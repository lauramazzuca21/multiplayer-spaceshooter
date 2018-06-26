using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SetupManager : MonoBehaviour {

	[SerializeField]
	private Slider eliminationsSlider;
	[SerializeField]
	private Toggle powerupToggle;
	[SerializeField]
	private Slider frequencySlider;
	[SerializeField]
	private Slider musicVolumeSlider;
	[SerializeField]
	private Slider effectsVolumeSlider;
    
   

	private void Update()
	{
		MusicPlayer.ChangeMusicValue(musicVolumeSlider.value);
	}
    

	public void SaveAndExit() {
		
		PlayerPrefsManager.SetEliminations((int) eliminationsSlider.value);

		if (powerupToggle.enabled) PlayerPrefsManager.SetPowerupOO(1);
		else PlayerPrefsManager.SetPowerupOO(0);

		PlayerPrefsManager.SetPowerupFrequency(frequencySlider.value);
		PlayerPrefsManager.SetMusicVolume(musicVolumeSlider.value / 100f);
		PlayerPrefsManager.SetEffectsVolume(effectsVolumeSlider.value / 100f);

	}

}
