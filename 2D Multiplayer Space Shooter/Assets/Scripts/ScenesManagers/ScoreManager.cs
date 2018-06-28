using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	static ScoreManager _instance = null;


    private void Awake()
    {
        if (_instance != null) { Destroy(gameObject); }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }   


	public int[] _scorePlayer;

    private int _maxKills;

    [SerializeField]
	private Text[] _textPlayerScore;

    private LevelManager levelManager;
	private string _currentScene;

	void Start()
    {
		_maxKills = PlayerPrefsManager.GetEliminations();
		_currentScene = SceneManager.GetActiveScene().ToString();

    }

	private void Update()
	{
		_currentScene = SceneManager.GetActiveScene().name;
		Debug.Log("currentScene " + _currentScene);


		if (_currentScene == Constants.GAMEPLAY_SCENE)
		{
			Debug.Log("DENTRO IF. OnOff " + PlayerPrefsManager.GetPowerupOO()
			          + " frequency " + PlayerPrefsManager.GetPowerupFrequency());
			
			int ActivePlayer = FindObjectOfType<ShipsManager>().ActivePlayers;

			if (ActivePlayer != 0 && _scorePlayer.Length < ActivePlayer)
            {
				_scorePlayer = new int[ActivePlayer];
				Debug.Log("created _scorePlayer with " + ActivePlayer);

                Reset();
            }
            if (levelManager == null)
            {
                levelManager = FindObjectOfType<LevelManager>();
            }
		}

	}

	public void UpdateScore(int playerID) //player 0-3
    {
		Debug.Log("Scored points: playerLayer " + playerID);
        _scorePlayer[playerID - Constants.LAYER_OFFSET]++;
        _textPlayerScore[playerID-Constants.LAYER_OFFSET].text = _scorePlayer[playerID - Constants.LAYER_OFFSET].ToString();

        if (_scorePlayer[playerID - Constants.LAYER_OFFSET] >= _maxKills) {
            levelManager.LoadScene(Constants.GAMEOVER_SCENE);
        }
    }

    public void Reset()
    {
		int i;
        for(i = 0; i< _scorePlayer.Length; i++)
        {
            _scorePlayer[i] = 0;
            _textPlayerScore[i].text = _scorePlayer[i].ToString();
        }
        
		for (int j = i; j < 4; j++)
        {
			_textPlayerScore[j].gameObject.SetActive(false);

        }
    }
}
