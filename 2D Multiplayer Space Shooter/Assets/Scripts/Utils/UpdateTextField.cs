using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateTextField : MonoBehaviour {

	[SerializeField]
	private Text textToUpdate;
	[SerializeField]
	private Slider slider;
	[SerializeField]
	private bool isPercent;
	
	// Update is called once per frame
	void Update () {
		if (!isPercent) textToUpdate.text = slider.value.ToString();
		else textToUpdate.text = slider.value.ToString() + "%";
	}
}
