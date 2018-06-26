using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int[] _scorePlayer;

    private int _maxKills;

    [SerializeField]
	private Text[] _textPlayerScore;

    private LevelManager levelManager;

	void Start()
    {
        _scorePlayer = new int[4];
		_maxKills = 10;
        Reset();
        levelManager = FindObjectOfType<LevelManager>();
    }
			
	public void UpdateScore(int playerID) //player 0-3
    {
        Debug.Log("Scored points");
        _scorePlayer[playerID - Constants.LAYER_OFFSET]++;
        _textPlayerScore[playerID-Constants.LAYER_OFFSET].text = _scorePlayer[playerID - Constants.LAYER_OFFSET].ToString();

        if (_scorePlayer[playerID - Constants.LAYER_OFFSET] == _maxKills) {
            levelManager.LoadScene(Constants.GAMEOVER_SCENE);
        }
    }

    public void Reset()
    {
        for(int i = 0; i<_scorePlayer.Length; i++)
        {
            _scorePlayer[i] = 0;
            _textPlayerScore[i].text = _scorePlayer[i].ToString();
        }
    }
}
