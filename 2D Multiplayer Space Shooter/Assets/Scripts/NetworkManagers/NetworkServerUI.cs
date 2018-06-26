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
	private Text _codeText;
	[SerializeField]
    private Text _activePlayersText;
	[SerializeField]
    private Text _IPText;
	[SerializeField]
    private Text _statusText;


    CrossPlatformInputManager.VirtualAxis m_HVAxis;
    CrossPlatformInputManager.VirtualAxis m_VVAxis;

    int IDFirstPlayer;
    float hDelta, vDelta;
    string horizontalAxis = "Horizontal1";
    string verticalAxis = "Vertical1";
	private bool _justLoaded = true;


	//private void OnGUI()
	//{
	//    string IPAddress = Network.player.ipAddress;
	//    GUI.Box(new Rect(10, Screen.height - 50, 100, 50), IPAddress);
	//    GUI.Label(new Rect(20, Screen.height - 35, 100, 20), "Status: " + NetworkServer.active);
	//    GUI.Label(new Rect(20, Screen.height - 20, 100, 20), "Connected: " + NetworkServer.connections.Count);
	//}

	// Use this for initialization
	void Start()
    {
		DontDestroyOnLoad(gameObject);

		ActivePlayers = 0;
		_activePlayersText.text = ActivePlayers.ToString();

		playersChoices = new LinkedList<Choice>();

		_currentScene = SceneManager.GetActiveScene().name;

        m_HVAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxis);
        m_VVAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxis);
        CrossPlatformInputManager.RegisterVirtualAxis(m_HVAxis);
		CrossPlatformInputManager.RegisterVirtualAxis(m_VVAxis);

		_codeText.text = Utilities.RandomCode(Constants.CODE_LENGTH); //displays the rand code
		Network.incomingPassword = _codeText.text; //sets the password for connection as the rand generated code

		//Network.InitializeServer(4, 25000, false);

		NetworkServer.Listen(25000);
        
        NetworkServer.RegisterHandler(888, ServerReceiveMessage);

    }

    private void ServerReceiveMessage(NetworkMessage message)
    {
        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;
        string[] deltas = msg.value.Split('|');
		Debug.Log("currentChoice " + msg.value);

		if (_currentScene == Constants.CONNECTION_SCENE)
		{
			if (deltas.Length == 2)
			{
				Choice currentChoice;
				currentChoice.IDPlayer = deltas[0];
				currentChoice.shipChosen = Convert.ToInt32(deltas[1]);


				if (!playersChoices.Contains(currentChoice))
				{
					playersChoices.AddLast(currentChoice);

					if (playersChoices.Count == 1)
					{
						msg.value = Players.FIRST.ToString();
						IDFirstPlayer = Convert.ToInt32(currentChoice.IDPlayer);
						NetworkServer.SendToClient(IDFirstPlayer, 888, msg);
					}
				}
			}
			else if (deltas.Length == 3 && Convert.ToInt32(deltas[0]) == IDFirstPlayer)
			{
				m_HVAxis.Update(Convert.ToSingle(deltas[1]));
				m_VVAxis.Update(Convert.ToSingle(deltas[2]));
				hDelta = Convert.ToSingle(deltas[1]);
				vDelta = Convert.ToSingle(deltas[2]);

				FindObjectOfType<MoveThroughUI>().UpdateUIOnConnection(hDelta, false);

			}

		}

		else if (_currentScene == Constants.SETUP_SCENE || _currentScene == Constants.GAMEOVER_SCENE)
		{
			if (deltas[0] == playersChoices.First<Choice>().IDPlayer)
			{

			}
		}
		else if (_currentScene == Constants.GAMEPLAY_SCENE && !_justLoaded)
		{
			
			m_HVAxis.Update(Convert.ToSingle(deltas[1]));
			m_VVAxis.Update(Convert.ToSingle(deltas[2]));
			hDelta = Convert.ToSingle(deltas[1]);
			vDelta = Convert.ToSingle(deltas[2]);


		}
        
    }

    // Update is called once per frame
    void Update()
    {
		_currentScene = SceneManager.GetActiveScene().name;


		if (_currentScene == Constants.CONNECTION_SCENE)
        {
			ActivePlayers = NetworkServer.connections.Count - 1;
            _activePlayersText.text = ActivePlayers.ToString();

			_IPText.text = Network.player.ipAddress;

			_statusText.text = NetworkServer.active.ToString();
            
            
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
   
 

}
