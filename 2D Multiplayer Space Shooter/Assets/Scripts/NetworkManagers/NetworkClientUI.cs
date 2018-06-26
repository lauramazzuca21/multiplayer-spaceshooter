using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;

public class NetworkClientUI : MonoBehaviour
{
    static NetworkClient client;
	static public Ships ShipChosen { get; set; }
	static private string playerID;
	private Text _codeText;
	private LevelManager _levelManager;
	//private 

    private void OnGUI()
    {
        string IPAddress = Network.player.ipAddress;
        GUI.Box(new Rect(10, Screen.height - 50, 100, 50), IPAddress);
        GUI.Label(new Rect(20, Screen.height - 30, 100, 20), "Status: " + client.isConnected);
       
    }   

    // Use this for initialization
    void Start()
    {
		playerID = Network.player.ipAddress;
        client = new NetworkClient();
		_levelManager = FindObjectOfType<LevelManager>();
    }
    
    public void Connect()
    {
		Network.Connect("192.168.1.141", 25000, _codeText.text);

		if (client.isConnected)
		{
			StringMessage msg = new StringMessage();
			msg.value = playerID + "|" + (int)ShipChosen;
			_levelManager.LoadScene(Constants.CONTROLLER_SCENE);
		}
	}

	static public void SendControllerInfo(float hDelta, float vDelta)
    {
		if (Network.isClient)
        {
            StringMessage msg = new StringMessage();
			msg.value = hDelta + "|" + vDelta + "|" +  ShipChosen + "|";
            client.Send(888, msg);
            Debug.Log("Message Sent!");
        }
        else Debug.Log("Accipicchia!");
    }

    // Update is called once per frame
    void Update()
    {
    }
}