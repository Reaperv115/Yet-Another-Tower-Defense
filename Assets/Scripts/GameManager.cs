using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject playButton, weaponsButton, startButton, rotate90, rotateneg90, restart;
    [SerializeField]
    GameObject scoreBoard, round, victoryDisplay;

    // enemies to load and spawn-in
    GameObject enemy, enemy2, enemy3;

    [SerializeField]
    GameObject weaponsPanel;

    List<Color>victorydisplayColors;

    [SerializeField]
    GameObject enemystartingPos;
    SpawnEnemy spawnEnemy;

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
        weaponsPanel.SetActive(false);
        startButton.SetActive(false);
        victorydisplayColors = new List<Color>() { Color.blue, Color.green, Color.black, Color.cyan };
        spawnEnemy = enemystartingPos.GetComponent<SpawnEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        // displaying score and current round
        scoreBoard.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
        round.GetComponent<TextMeshProUGUI>().text = "Round: " + currentRound.ToString();
    }

    // getters
    public TextMeshProUGUI GetScoreBoard() { return scoreBoard.GetComponent<TextMeshProUGUI>(); }
    public TextMeshProUGUI GetRound() { return round.GetComponent<TextMeshProUGUI>(); }
    public TextMeshProUGUI GetVictoryDisplay() { return victoryDisplay.GetComponent<TextMeshProUGUI>(); }
    public GameObject GetTier1Enemy() { return enemy; }
    public GameObject GetTier2Enemy() { return enemy2; }
    public GameObject GetTier3Enemy() { return enemy3; }
    public GameObject GetPlayButton() { return playButton; }
    public GameObject GetWeaponsButton() { return weaponsButton; }
    public GameObject GetStartButton() { return startButton; }
    public GameObject GetRotate90() { return rotate90; }
    public GameObject GetRotateNeg90() { return rotateneg90; }
    public GameObject GetWeaponsPanel() { return weaponsPanel; }
    public GameObject GetRestartButton() { return restart; }
    public SpawnEnemy GetSpawnEnemyRef() { return spawnEnemy; }
    public bool GetNextRound() { return nextRound; }
    public int GetCurrentRound() { return currentRound; }


    // setters
    public void SetScore(int newScore) { score = newScore; }
    public int GetScore() { return score; }
    public void SetCurrentRound(int newRound) { currentRound = newRound; }
    public void SetNextRound(bool nextround) { nextRound = nextround; }

    public void RestartGame() { SceneManager.LoadScene("Game"); }

    public void YouWON()
    {
        victoryDisplay.GetComponent<TextMeshProUGUI>().text = "YOU WIN! Get Ready For The Next Round!";
        if (changeColor <= 0)
        {
            colorIndex = Random.Range(0, victorydisplayColors.Count);
            colorIndex2 = Random.Range(0, victorydisplayColors.Count);
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

    public int GetT1Price()
    {
        return 3;
    }

    public int GetT2Price()
    {
        return 6;
    }
    public int GetT3Price()
    {
        return 9;
    }

}
