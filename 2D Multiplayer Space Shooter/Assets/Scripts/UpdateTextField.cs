using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateTextField : MonoBehaviour {

	[SerializeField]
	private Text textToUpdate;
	[SerializeField]
	private Slider slider;
	
	// Update is called once per frame
	void Update () {
		textToUpdate.text = slider.value.ToString();
	}
}
