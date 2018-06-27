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


	private void Start()
	{
		eliminationsSlider.value = PlayerPrefsManager.GetEliminations();
		powerupToggle.isOn = PlayerPrefsManager.GetPowerupOO() ? true : false;
		frequencySlider.value = PlayerPrefsManager.GetPowerupFrequency();
		musicVolumeSlider.value = PlayerPrefsManager.GetMusicVolume();
		effectsVolumeSlider.value = PlayerPrefsManager.GetEffectsVolume();

	}

	private void Update()
	{
		MusicPlayer.ChangeMusicValue(musicVolumeSlider.value);
       
	}
    
    
	public void SaveAndExit() {
		
		PlayerPrefsManager.SetEliminations((int) eliminationsSlider.value);

		if (powerupToggle.isOn) PlayerPrefsManager.SetPowerupOO(1);
		else PlayerPrefsManager.SetPowerupOO(0);

		PlayerPrefsManager.SetPowerupFrequency(frequencySlider.value);
		PlayerPrefsManager.SetMusicVolume(musicVolumeSlider.value / 100f);
		PlayerPrefsManager.SetEffectsVolume(effectsVolumeSlider.value / 100f);

	}

}
