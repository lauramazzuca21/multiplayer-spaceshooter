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


	private int[] _scorePlayer;

    private int _maxKills;

    [SerializeField]
	public Text[] _textPlayerScore;

    private LevelManager levelManager;
	private string _currentScene;

	void Start()
    {
		_maxKills = PlayerPrefsManager.GetEliminations();
    }

	private void Update()
	{
		_currentScene = SceneManager.GetActiveScene().ToString();

		if (_currentScene == Constants.GAMEPLAY_SCENE)
		{
			if (FindObjectOfType<ShipsManager>().ActivePlayers != 0 && _scorePlayer == null)
            {
                _scorePlayer = new int[FindObjectOfType<ShipsManager>().ActivePlayers];
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
        for(i = 0; i<_scorePlayer.Length; i++)
        {
            _scorePlayer[i] = 0;
            _textPlayerScore[i].text = _scorePlayer[i].ToString();
        }
        
		for (int j = i; j < 4; j++)
        {
			_textPlayerScore[i].gameObject.SetActive(false);

        }
    }
}
