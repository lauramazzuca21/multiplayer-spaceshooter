using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkClientUI : MonoBehaviour
{

	static NetworkClientUI _instance = null;

    //this is best to implement the Singleton pattern for MusicPlayer instance
    private void Awake()
    {
        if (_instance != null) { Destroy(gameObject); }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    static NetworkClient client;
	//static NetworkConnection connection;
    
	static public Ships ShipChosen { get; set; }
	public static int playerID;
	private bool _isFirstPlayer = false;
	[SerializeField]
	private Text _codeText;
	[SerializeField]
    private Text _statusText;
	[SerializeField]
    private Text _playerText;

	private LevelManager _levelManager;
	private string _currentScene;
	private bool _foundTextField;

	private void OnGUI()
    {
        string IPAddress = Network.player.ipAddress;
        GUI.Box(new Rect(10, Screen.height - 50, 100, 50), IPAddress);
        GUI.Label(new Rect(20, Screen.height - 30, 100, 20), "Status: " + client.isConnected);
       
    }   

    // Use this for initialization
    void Start()
    {
		
		client = new NetworkClient();

		_levelManager = FindObjectOfType<LevelManager>();

		//connection = new NetworkConnection();      
		client.RegisterHandler(888, ClientReceiveMessage);
    }

    //this function gets called once the player hits the "done" button on screen and automatically sends the
    //choice of ship and passwork. Does not connect until 
	public void Connect()
    {
		//Network.Connect("192.168.43.45", 25000, _codeText.text);
		client.Connect(_codeText.text, 25000);

		StartCoroutine(this.Wait());

		//Debug.Log("CLIENT IS CONNECTED!");
	      
    }

	private IEnumerator Wait()
	{
		yield return new WaitForSeconds(1f);

		if (client.isConnected)
		//if(Network.isClient)
		{
            StringMessage msg = new StringMessage();
			msg.value = ((int)ShipChosen).ToString();

            client.Send(888, msg);
            _levelManager.LoadScene(Constants.CONTROLLER_SCENE);
        }
      
	}

	//private void OnConnectedToServer()
	//{
	//	StringMessage msg = new StringMessage();
 //       msg.value = ((int)ShipChosen).ToString();
        
	//	connection.Send(888, msg);
 //       _levelManager.LoadScene(Constants.CONTROLLER_SCENE);
	//}


	private void ClientReceiveMessage(NetworkMessage networkMessage)
	{
		StringMessage msg = new StringMessage();
		msg.value = networkMessage.ReadMessage<StringMessage>().value;
        string[] deltas = msg.value.Split('|');

		if (deltas[0] == Players.FIRST.ToString())
		{
			_isFirstPlayer = true;
			playerID = 0;
		}
		else if (deltas[0] == "ID")
		{
			playerID = Convert.ToInt32(deltas[1]);
		}      
		else if (deltas[0] == "health") 
		{
			float health = Convert.ToSingle(deltas[1]);
			FindObjectOfType<ProgressBar>().UpdateHealthBar(health);
		}
		else if (deltas[0] == "MAXhealth")
        {
            float health = Convert.ToSingle(deltas[1]);
            FindObjectOfType<ProgressBar>().SetMaxHealth(health);
        }

	}



	static public void SendControllerInfo(float hDelta, float vDelta)
    {
		StringMessage msg = new StringMessage();
		msg.value = playerID + "|" + hDelta + "|" +  vDelta;

		client.Send(888, msg);
        Debug.Log("Message Sent!");
       
    }

	public void FireButton(bool fireStatus)
    {
        StringMessage msg = new StringMessage();
		msg.value = playerID + "|" + fireStatus.ToString();
		client.Send(888, msg);
        Debug.Log("Message Sent!");

    }

    // Update is called once per frame
    void Update()
    {
		//_statusText.text = Network.isClient.ToString();
		//_peerTypeText.text = Network.peerType.ToString();

		//_statusText.text = client.isConnected.ToString();

		_currentScene = SceneManager.GetActiveScene().name;

		if (_currentScene == Constants.CONTROLLER_SCENE)
		{
			if((_playerText = FindObjectOfType<Text>()).text == "" && !_foundTextField)
			{
				_foundTextField = true;
				_playerText.text = playerID.ToString();
			}

		}
    }
}