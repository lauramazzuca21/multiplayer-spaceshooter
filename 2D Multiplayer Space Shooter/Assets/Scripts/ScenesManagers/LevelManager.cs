using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	//static LevelManager _instance = null;


	//private void Awake()
    //{
    //    if (_instance != null) { Destroy(gameObject); }
    //    else
    //    {
    //        _instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //}   

    public void LoadScene(string name)
	{
		SceneManager.LoadScene(name);
	}

	public void Quit()
	{
		#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
	}
}
