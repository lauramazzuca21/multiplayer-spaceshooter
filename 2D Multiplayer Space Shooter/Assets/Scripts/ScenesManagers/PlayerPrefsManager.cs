using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{

    const string ELIMINATIONS_KEY = "eliminations_value";
    const string POWERUP_OO_KEY = "powerup_oo_value";
    const string POWERUP_FREQUENCY_KEY = "powerup_frequency_value";
    const string MUSIC_KEY = "music_value";
    const string EFFECTS_KEY = "effects_value";

    public static void SetEliminations(int eliminations)
    {
        PlayerPrefs.SetInt(ELIMINATIONS_KEY, eliminations);
    }

    public static void SetPowerupOO(int setting)
    {
        PlayerPrefs.SetInt(MUSIC_KEY, setting);
    }

    public static void SetPowerupFrequency(float frequency)
    {
        PlayerPrefs.SetFloat(POWERUP_FREQUENCY_KEY, frequency);
    }

    public static void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat(MUSIC_KEY, volume);
    }

    public static void SetEffectsVolume(float volume)
    {
        PlayerPrefs.SetFloat(EFFECTS_KEY, volume);
    }

	public static int GetEliminations()
    {
        return PlayerPrefs.GetInt(ELIMINATIONS_KEY);
    }

	public static bool GetPowerupOO()
    {
		return PlayerPrefs.GetInt(MUSIC_KEY) == 0 ? false : true;
    }

	public static float GetPowerupFrequency()
    {
		return PlayerPrefs.GetFloat(POWERUP_FREQUENCY_KEY);
    }

	public static float GetMusicVolume()
    {
		return PlayerPrefs.GetFloat(MUSIC_KEY);
    }

	public static float GetEffectsVolume()
    {
		return PlayerPrefs.GetFloat(EFFECTS_KEY);
    }
}
