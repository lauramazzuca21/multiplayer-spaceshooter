using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ProgressBar : MonoBehaviour {

	[Header("OBJECTS")]
	public Transform loadingBar;
	public Transform textPercent;

	[Header("VARIABLES (IN-GAME)")]
	public bool isOn;
	public bool restart;
	private bool _firstRecharge = true;
	[Range(0, 100)] public float currentPercent;
	[Range(0, 100)] public int speed;
	private float MAX_HEALTH;


	private void Start()
	{
		
	}


	void Update ()
	{
		if (currentPercent < 100 && isOn == true && _firstRecharge) 
		{
			currentPercent += speed * Time.deltaTime;

			if (currentPercent >= 100)
            {
				_firstRecharge = false;
				currentPercent = 100;

            }

		}

		loadingBar.GetComponent<Image>().fillAmount = currentPercent / 100;
        textPercent.GetComponent<Text>().text = ((int)currentPercent).ToString("F0") + "%";      

	}

    public void UpdateHealthBar(float health)
	{
		currentPercent = (health/MAX_HEALTH) * 100;
	}

    internal void SetMaxHealth(float health)
    {
		MAX_HEALTH = health;
    }
}