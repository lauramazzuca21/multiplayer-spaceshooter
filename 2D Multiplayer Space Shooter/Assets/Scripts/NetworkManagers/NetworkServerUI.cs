using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class NetworkServerUI : MonoBehaviour {

	public int ActivePlayers { get; private set; }
	private List<Choice> playersChoices;
	private ShipsManager _shipsManager;
	private string _currentScene;
	[SerializeField]
	private Text _codeText;
	[SerializeField]
    private Text _activePlayersText;
	private NetworkSettingsAttribute _server;


    CrossPlatformInputManager.VirtualAxis m_HVAxis;
    CrossPlatformInputManager.VirtualAxis m_VVAxis;

    int ID;
    float hDelta, vDelta;
    string horizontalAxis = "Horizontal1";
    string verticalAxis = "Vertical1";

    private void OnGUI()
    {
        string IPAddress = Network.player.ipAddress;
        GUI.Box(new Rect(10, Screen.height - 50, 100, 50), IPAddress);
        GUI.Label(new Rect(20, Screen.height - 35, 100, 20), "Status: " + NetworkServer.active);
        GUI.Label(new Rect(20, Screen.height - 20, 100, 20), "Connected: " + NetworkServer.connections.Count);
    }

    // Use this for initialization
    void Start()
    {
		DontDestroyOnLoad(gameObject);

		ActivePlayers = 0;
		_activePlayersText.text = ActivePlayers.ToString();

		_currentScene = SceneManager.GetActiveScene().name;

        m_HVAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxis);
        m_VVAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxis);
        CrossPlatformInputManager.RegisterVirtualAxis(m_HVAxis);
		CrossPlatformInputManager.RegisterVirtualAxis(m_VVAxis);

		_codeText.text = Utilities.RandomCode(Constants.CODE_LENGTH); //displays the rand code
		Network.incomingPassword = _codeText.text; //sets the password for connection as the rand generated code

		bool useNat = !Network.HavePublicAddress(); //checks on what connection we are to decide if nat is to be used
		Network.InitializeServer(4, 25000, useNat); //initializes the server

        NetworkServer.RegisterHandler(888, ServerReceiveMessage);

    }

    private void ServerReceiveMessage(NetworkMessage message)
    {
        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;
        string[] deltas = msg.value.Split('|');

		if (_currentScene == Constants.CONNECTION_SCENE && deltas.Length == 2)
        {
			Choice currentChoice;
			currentChoice.IDPlayer = deltas[0];
			currentChoice.shipChosen = deltas[1];

			if (!playersChoices.Contains(currentChoice))
			{
				playersChoices.Add(currentChoice);

				if (playersChoices.ToArray().Length == 1)
				{
					msg.value = Players.FIRST.ToString();
					NetworkServer.SendToClient();
				}
			}
        }
		else if (_currentScene == Constants.SETUP_SCENE || _currentScene == Constants.GAMEOVER_SCENE)
        {
			m_HVAxis.Update(Convert.ToSingle(deltas[0]));
            m_VVAxis.Update(Convert.ToSingle(deltas[1]));
        }
		else if (_currentScene == Constants.GAMEPLAY_SCENE)
        {

        }

        
        hDelta = Convert.ToSingle(deltas[0]);
        vDelta = Convert.ToSingle(deltas[1]);
        ID = (int) Convert.ToSingle(deltas[2]);

        //foreach(Ship s in ships)
        //{
        //    if(s.getID() == ID)
        //    {
        //        s.Movement(hDelta, vDelta);
        //    }
        //    Debug.Log("Message Received!");
        //}
        
    }

    // Update is called once per frame
    void Update()
    {
		if (_currentScene == Constants.CONNECTION_SCENE)
        {
            ActivePlayers = Network.connections.Length;
            _activePlayersText.text = ActivePlayers.ToString();
        }
	}
   
 

}
