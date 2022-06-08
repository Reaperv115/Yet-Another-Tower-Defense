using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject scoreBoard, round;

    private int score = 3;
    int currentRound;
    // Start is called before the first frame update
    void Start()
    {
        currentRound = 1;
    }

    // Update is called once per frame
    void Update()
    {
        scoreBoard.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
        round.GetComponent<TextMeshProUGUI>().text = "Round: " + currentRound.ToString();
    }

    public TextMeshProUGUI GetScoreBoard()
    {
        return scoreBoard.GetComponent<TextMeshProUGUI>();
    }

    public int GetScore()
    {
        return score;
    }
    public void SetScore(int newScore)
    {
        score = newScore;
    }

    public TextMeshProUGUI GetRound()
    {
        return round.GetComponent<TextMeshProUGUI>();
    }

    public int GetCurrentRound()
    {
        return currentRound;
    }

    public void SetCurrentRound(int newRound)
    {
        currentRound = newRound;
    }
}
