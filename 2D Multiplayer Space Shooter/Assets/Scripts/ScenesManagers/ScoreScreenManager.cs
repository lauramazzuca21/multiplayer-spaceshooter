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

    public ScoreManager scoreManager;


    // Use this for initialization
    void Start()
    {
        ResetTexts();
        scoreManager = FindObjectOfType<ScoreManager>();
        SetTexts(scoreManager._textPlayerScore);
    }

    // Update is called once per frame
    public void ResetTexts()
    {

        for (int i = 0; i < 4; i++)
        {
            _textPlayerID[i].text = "";
            _textFinalScore[i].text = "";
        }

    }

    public void SetTexts(Text[] _textPlayersScore)
    {
        int j = 0;
        int i = 0;
        string scoreMax = _textPlayersScore[i].text;
        int intMax = 0;
        int[] pos = new int[4];
        pos[0] = 5;
        pos[1] = 5;
        pos[2] = 5;
        pos[3] = 5;

        for (j = 0; j < 4; j++)
        {

            for (i = 0; i < 4; i++) //FIX I<4
            {
                if ((_textPlayersScore[i].text.CompareTo(scoreMax) > 0) && i != pos[0] && i != pos[1] && i != pos[2] && i != pos[3])
                {
                    scoreMax = _textPlayersScore[i].text;
                    intMax = i;
                }
            }

            pos[j] = intMax;
            _textPlayerID[j].text = "Player " + (intMax + 1).ToString();
            _textPlayersScore[j].text = scoreMax;

        }
    }
}
