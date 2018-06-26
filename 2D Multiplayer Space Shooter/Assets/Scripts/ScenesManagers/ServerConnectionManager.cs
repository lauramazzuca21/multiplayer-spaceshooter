using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerConnectionManager : MonoBehaviour {

    public int ActivePlayers = 3;

	private Ships[] playersChoices;

	private ShipsManager _shipsManager;

    // Use this for initialization
    void Start () {
		_shipsManager = FindObjectOfType<ShipsManager>();
		playersChoices = new Ships[ActivePlayers];

		for (int i = 0; i < playersChoices.Length; i++)
		{
			playersChoices[i] = Ships.FAST;
		}

		_shipsManager.InstantiateShips(ActivePlayers, this.playersChoices);
	}

	private void SetPlayerChoice(int playerID, Ships ship) {
		playersChoices[playerID] = ship;
        
	}
}
