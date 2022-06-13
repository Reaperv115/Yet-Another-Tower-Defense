using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject playButton, weaponsButton;
    [SerializeField]
    GameObject scoreBoard, round, victoryDisplay;
    GameObject enemy, enemy2, enemy3;

    List<Color>victorydisplayColors;

    bool nextRound;

    private int score = 3;
    int currentRound;
    float changeColor = 3;
    int colorIndex = 0, colorIndex2 = 1;

    // Start is called before the first frame update
    void Start()
    {
        currentRound = 1;
        nextRound = false;
        enemy = Resources.Load<GameObject>("enemy car (Tier 1)");
        enemy2 = Resources.Load<GameObject>("enemy car (Tier 2)");
        enemy3 = Resources.Load<GameObject>("enemy car (Tier 3)");
        victorydisplayColors = new List<Color>() { Color.blue, Color.green, Color.white, Color.black, Color.cyan };
    }

    // Update is called once per frame
    void Update()
    {
        scoreBoard.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
        round.GetComponent<TextMeshProUGUI>().text = "Round: " + currentRound.ToString();
    }

    public TextMeshProUGUI GetScoreBoard() { return scoreBoard.GetComponent<TextMeshProUGUI>(); }

    public int GetScore() { return score; }
    public void SetScore(int newScore) { score = newScore; }

    public TextMeshProUGUI GetRound() { return round.GetComponent<TextMeshProUGUI>(); }

    public int GetCurrentRound() { return currentRound; }
    public void SetCurrentRound(int newRound) { currentRound = newRound; }

    public TextMeshProUGUI GetVictoryDisplay() { return victoryDisplay.GetComponent<TextMeshProUGUI>(); }

    public GameObject GetTier1Enemy() { return enemy; }

    public GameObject GetTier2Enemy() { return enemy2; }

    public GameObject GetTier3Enemy() { return enemy3; }

    public bool GetNextRound() { return nextRound; }

    public void SetNextRound(bool nextround) { nextRound = nextround; }

    public GameObject GetPlayButton() { return playButton; }

    public GameObject GetWeaponsButton() { return weaponsButton; }

    public void YouWON()
    {
        victoryDisplay.GetComponent<TextMeshProUGUI>().text = "YOU WIN! Get Ready For The Next Round!";
        if (changeColor <= 0)
        {
            colorIndex = Random.Range(0, victorydisplayColors.Count);
            colorIndex2 = Random.Range(0, victorydisplayColors.Count);
            if (colorIndex.Equals(colorIndex2))
                colorIndex2 += 1;
            changeColor = 3;
        }
        changeColor -= Time.deltaTime;
        victoryDisplay.GetComponent<TextMeshProUGUI>().color = Color.Lerp(victorydisplayColors[colorIndex], victorydisplayColors[colorIndex2], Mathf.PingPong(Time.time, 1));
    }

    public void MoveOn()
    {
        victoryDisplay.GetComponent<TextMeshProUGUI>().text = "";
        victoryDisplay.GetComponent<TextMeshProUGUI>().color = Color.green;
    }
}
