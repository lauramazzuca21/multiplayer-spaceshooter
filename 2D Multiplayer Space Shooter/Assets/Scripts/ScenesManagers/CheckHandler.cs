using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHandler : MonoBehaviour {
   
	// Use this for initialization
	public void CheckOnLoad (string scene) {
		if (FindObjectOfType<NetworkServerUI>().ActivePlayers >= 2) FindObjectOfType<LevelManager>().LoadScene(scene);
	}
}
