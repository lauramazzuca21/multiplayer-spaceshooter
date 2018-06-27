using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreenManager : MonoBehaviour
{
    [SerializeField]
    private Text[] _textPlayerID;

    [SerializeField]
    private Text[] _textFinalScore;

	private ScoreManager scoreManager;


    // Use this for initialization
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
		ResetTexts();
		SetTexts(scoreManager._scorePlayer);
    }

    // Update is called once per frame
    public void ResetTexts()
    {
		int i;
		Debug.Log("ScorePlayer.Length " + scoreManager._scorePlayer.Length);
		for ( i = 0; i < scoreManager._scorePlayer.Length; i++)
        {
            _textPlayerID[i].text = "";
            _textFinalScore[i].text = "";
        }
        
		for (int j = i; j < 4; j++)
        {
			_textPlayerID[j].gameObject.SetActive(false);
			_textFinalScore[j].gameObject.SetActive(false);
        }

    }

	public void SetTexts(int[] _playersScore)
    {

		int[] pos = _playersScore;

		Array.Sort(pos);

		Array.Reverse(pos);


		//for (int i = 0; i < _textPlayersScore.Length; i++)
        //{
        //    pos[i] = Convert.ToInt32(_textPlayersScore[i].text);
        //}

        //int j = 0;
        //int i = 0;
        //string scoreMax = _textPlayersScore[i].text;
        //int intMax = 0;
        //int[] pos = new int[4];
        //pos[0] = 5;
        //pos[1] = 5;
        //pos[2] = 5;
        //pos[3] = 5;


		for (int j = 0; j < pos.Length; j++)
        {
			for (int i = 0; i < _playersScore.Length; i++) //FIX I<4
            {
				Debug.Log("j " + j + " value " + pos[j] + "i " + i + " value " + _playersScore[i]);
				if (_playersScore[i] == pos[j])
                {
					_textPlayerID[j].text = "Player " + (i+1).ToString();
					_textFinalScore[j].text = _playersScore[i].ToString();
					_playersScore[i] = -1;
                }
            }

   //         pos[j] = intMax;
            

			//scoreMax = "";
			//intMax = 0;
        }
    }
}
