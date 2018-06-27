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
        SetTexts(scoreManager._textPlayerScore);
		ResetTexts();
    }

    // Update is called once per frame
    public void ResetTexts()
    {
		int i;

		for ( i = 0; i < scoreManager._textPlayerScore.Length; i++)
        {
            _textPlayerID[i].text = "";
            _textFinalScore[i].text = "";
        }
        
		for (int j = i; j < 4; j++)
        {
			_textPlayerID[i].gameObject.SetActive(false);
			_textFinalScore[i].gameObject.SetActive(false);
        }

    }

    public void SetTexts(Text[] _textPlayersScore)
    {
        
		int[] pos = new int[_textPlayersScore.Length];

		for (int i = 0; i < _textPlayersScore.Length; i++)
        {
			pos[i] = Convert.ToInt32(_textPlayersScore[i].text);
        }

		Array.Sort(pos);


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
            for (int i = 0; i < _textPlayersScore.Length; i++) //FIX I<4
            {
				if (Convert.ToInt32(_textPlayersScore[i].text) == pos[j])
                {
					_textPlayerID[j].text = "Player " + (i+1).ToString();
					_textPlayersScore[j].text = _textPlayersScore[i].text;
                }
            }

   //         pos[j] = intMax;
            

			//scoreMax = "";
			//intMax = 0;
        }
    }
}
