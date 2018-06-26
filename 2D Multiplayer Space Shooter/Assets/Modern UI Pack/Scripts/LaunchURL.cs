using UnityEngine;
using System.Collections;

public class LaunchURL : MonoBehaviour {

	public string URL; 

	public void urlLinkOrWeb() 
	{
		#if !UNITY_EDITOR
            Application.OpenURL(URL);
        #endif

	}
}
