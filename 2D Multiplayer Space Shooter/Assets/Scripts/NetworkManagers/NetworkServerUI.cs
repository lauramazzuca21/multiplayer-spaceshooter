using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class NetworkServerUI : MonoBehaviour {

	static NetworkServerUI _instance = null;

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



	public int ActivePlayers { get; private set; }
	private LinkedList<Choice> playersChoices;

	private ShipsManager _shipsManager;

	private string _currentScene;

	[SerializeField]
    private Text _activePlayersText;
	[SerializeField]
    private Text _IPText;


    CrossPlatformInputManager.VirtualAxis m_HVAxis;
    CrossPlatformInputManager.VirtualAxis m_VVAxis;

	NetworkServer server;
    int IDFirstPlayer;
    float hDelta, vDelta;
    string horizontalAxis = "Horizontal1";
    string verticalAxis = "Vertical1";
	private bool _justLoaded = true;

	// Use this for initialization
	void Start()
	{
        playersChoices = new LinkedList<Choice>();

		_currentScene = SceneManager.GetActiveScene().name;

        m_HVAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxis);
        m_VVAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxis);
        CrossPlatformInputManager.RegisterVirtualAxis(m_HVAxis);
		CrossPlatformInputManager.RegisterVirtualAxis(m_VVAxis);
       
		Network.InitializeSecurity();

		//_codeText.text = Utilities.RandomCode(Constants.CODE_LENGTH); //displays the rand code
		//Network.incomingPassword = _codeText.text; //sets the password for connection as the rand generated code      
		//Network.InitializeServer(4, 25000, false);

		NetworkServer.Listen(25000);
        
		NetworkServer.RegisterHandler(888, ServerReceiveMessage);

    }


	// Update is called once per frame
    void Update()
    {
        _currentScene = SceneManager.GetActiveScene().name;


        if (_currentScene == Constants.CONNECTION_SCENE)
        {
            ActivePlayers = NetworkServer.connections.Count <= 0 ? 0 : NetworkServer.connections.Count - 1;
			//ActivePlayers = Network.connections.Length;

			_activePlayersText.text = ActivePlayers.ToString();

            _IPText.text = Network.player.ipAddress;

			//_statusText.text = Network.isServer.ToString();

        }

        if (_currentScene == Constants.GAMEPLAY_SCENE)
        {
            if (_justLoaded)
            {
                _shipsManager = FindObjectOfType<ShipsManager>();

                _shipsManager.InstantiateShips(ActivePlayers, playersChoices.ToArray<Choice>());

                _justLoaded = false;
            }
        }
    }


    private void ServerReceiveMessage(NetworkMessage message)
    {
        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;
        
		//Debug.Log("Message received " + msg.value);

		if (_currentScene == Constants.CONNECTION_SCENE)
		{
			ConnectionSceneParsingHandler(msg);         
		}

		else if (_currentScene == Constants.SETUP_SCENE || _currentScene == Constants.GAMEOVER_SCENE)
		{
			SetupAndGameoverSceneParsingHandler(msg);
		}
		else if (_currentScene == Constants.GAMEPLAY_SCENE && !_justLoaded)
		{
			GamePlaySceneParsingHandler(msg);         
		}
        
    }
   
 
	private void GamePlaySceneParsingHandler(StringMessage msg)
    {
        string[] deltas = msg.value.Split('|');
        
		if (deltas.Length == 2)
		{
			_shipsManager.PlayerFire(Convert.ToInt32(deltas[0]));
		}
		else if ( deltas.Length == 3)
		{
			m_HVAxis.Update(Convert.ToSingle(deltas[1]));
            m_VVAxis.Update(Convert.ToSingle(deltas[2]));
            hDelta = Convert.ToSingle(deltas[1]);
            vDelta = Convert.ToSingle(deltas[2]);

            _shipsManager.MovePlayer(Convert.ToInt32(deltas[0]), hDelta, vDelta);
		}      

    }

	private void SetupAndGameoverSceneParsingHandler(StringMessage msg)
    {
        string[] deltas = msg.value.Split('|');

		if (deltas.Length == 2 && Convert.ToInt32(deltas[0]) == IDFirstPlayer)
        {
            FindObjectOfType<MoveThroughUI>().Click(Convert.ToBoolean(deltas[1]));
        }
        else if (deltas.Length == 3 && Convert.ToInt32(deltas[0]) == IDFirstPlayer)
        {
            m_HVAxis.Update(Convert.ToSingle(deltas[1]));
            m_VVAxis.Update(Convert.ToSingle(deltas[2]));
            hDelta = Convert.ToSingle(deltas[1]);
            vDelta = Convert.ToSingle(deltas[2]);

			FindObjectOfType<MoveThroughUI>().UpdateUIOnSetup(hDelta, vDelta);

        }

    }

	private void ConnectionSceneParsingHandler(StringMessage msg)
    {
        string[] deltas = msg.value.Split('|');

        if (deltas.Length == 1)
        {
            Choice currentChoice;
			currentChoice.IDPlayer = playersChoices.ToArray<Choice>().Length + 1;
            currentChoice.shipChosen = Convert.ToInt32(deltas[0]);

			Debug.Log("currentChoice: ID " + currentChoice.IDPlayer + " Ship " + currentChoice.shipChosen);

            if (!playersChoices.Contains(currentChoice))
            {
                playersChoices.AddLast(currentChoice);

				Debug.Log("added choice. playerChoices.Count = " + playersChoices.Count);

                if (playersChoices.Count == 1)
                {
                    msg.value = Players.FIRST.ToString();
                    IDFirstPlayer = currentChoice.IDPlayer - 1;
					Debug.Log("IDFirstPlayer " + IDFirstPlayer);

				} 
				else if (playersChoices.Count > 1)
				{
					int id = playersChoices.Count - 1;
					msg.value = "ID|" + id;
                    
				}

				NetworkServer.SendToClient(currentChoice.IDPlayer, 888, msg);
            }
        }
		else if (deltas.Length == 2 && Convert.ToInt32(deltas[0]) == IDFirstPlayer)
		{
			FindObjectOfType<MoveThroughUI>().Click(Convert.ToBoolean(deltas[1]));
		}
		else if (deltas.Length == 3 && Convert.ToInt32(deltas[0]) == IDFirstPlayer)
        {
            m_HVAxis.Update(Convert.ToSingle(deltas[1]));
            m_VVAxis.Update(Convert.ToSingle(deltas[2]));
            hDelta = Convert.ToSingle(deltas[1]);
            vDelta = Convert.ToSingle(deltas[2]);

            FindObjectOfType<MoveThroughUI>().UpdateUIOnConnection(hDelta);

        }
    }


	public static void SendHealthInfo(int IDPlayer, float health, bool isMax)
    {
		StringMessage msg = new StringMessage();

		if (isMax)
		{
			msg.value = "MAXhealth|" + health;
		} else {
			msg.value = "health|" + health;
		}



		NetworkServer.SendToClient(IDPlayer, 888, msg);
    }
}
