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
}
