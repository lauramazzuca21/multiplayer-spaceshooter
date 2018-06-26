using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour
{
	static MusicPlayer _instance = null;

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

    public static void ChangeMusicValue(float value)
	{
		AudioSource audio = _instance.GetComponent<AudioSource>();
        audio.volume = value;
	}
}
