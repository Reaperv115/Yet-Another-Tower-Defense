using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject beginroundButton, weaponsButton, startButton, restart, nextLevel;
    [SerializeField]
    GameObject scoreBoard, round, victoryDisplay;

    [SerializeField]
    TextMeshProUGUI weapontoPlace;

    // enemies to load and spawn-in
    GameObject enemy, enemy2, enemy3;

    [SerializeField]
    GameObject weaponsPanel;

    List<Color>victorydisplayColors;

    [SerializeField]
    GameObject enemystartingPos;
    SpawnEnemy spawnEnemy;

    Scene activeScene;
    Player player;
    bool hasBegun;

    bool nextRound;

    private float score = 6f;
    int currentRound;
    float changeColor = 3;
    int colorIndex = 0, colorIndex2 = 1;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(beginroundButton);
        hasBegun = false;
        currentRound = 1;
        nextRound = false;
        enemy = Resources.Load<GameObject>("enemy car (Tier 1)");
        enemy2 = Resources.Load<GameObject>("enemy car (Tier 2)");
        enemy3 = Resources.Load<GameObject>("enemy car (Tier 3)");
        weaponsPanel.gameObject.SetActive(false);
        beginroundButton.gameObject.SetActive(false);
        restart.gameObject.SetActive(false);
        victorydisplayColors = new List<Color>() { Color.blue, Color.green, Color.black, Color.cyan };
        spawnEnemy = enemystartingPos.GetComponent<SpawnEnemy>();
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        // displaying score and current round
        scoreBoard.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
        round.GetComponent<TextMeshProUGUI>().text = "Round: " + currentRound.ToString();
        activeScene = SceneManager.GetActiveScene();
        // if (!SceneManager.GetActiveScene().Equals(activeScene))
        // {
        //     ResetUI();
        //     player.SetIsPlacing(false);
        // }
    }

    // getters
    public TextMeshProUGUI GetScoreBoard() { return scoreBoard.GetComponent<TextMeshProUGUI>(); }
    public TextMeshProUGUI GetRound() { return round.GetComponent<TextMeshProUGUI>(); }
    public TextMeshProUGUI GetVictoryDisplay() { return victoryDisplay.GetComponent<TextMeshProUGUI>(); }
    public TextMeshProUGUI GetWeaponToPlaceDisplay() { return weapontoPlace; }
    public GameObject GetTier1Enemy() { return enemy; }
    public GameObject GetTier2Enemy() { return enemy2; }
    public GameObject GetTier3Enemy() { return enemy3; }
    public GameObject GetBeginRoundButton() { return beginroundButton; }
    public GameObject GetWeaponsButton() { return weaponsButton; }
    public GameObject GetStartButton() { return startButton; }
    public GameObject GetWeaponsPanel() { return weaponsPanel; }
    public GameObject GetRestartButton() { return restart; }
    public SpawnEnemy GetSpawnEnemyRef() { return spawnEnemy; }
    public bool GetNextRound() { return nextRound; }
    public int GetCurrentRound() { return currentRound; }


    // setters
    public void SetScore(float newScore) { score = newScore; }
    public float GetScore() { return score; }
    public void SetCurrentRound(int newRound) { currentRound = newRound; }
    public void SetNextRound(bool nextround) { nextRound = nextround; }

    public void RestartGame() { SceneManager.LoadScene("Game"); }


    public void MoveOn() { victoryDisplay.GetComponent<TextMeshProUGUI>().text = ""; }

    // returns the price of the tier 1 weapon
    public int GetT1Price() { return 2;  }
    public float GetT1PriceF() { return 2.0f; }

    // returns the price of the tier 2 weapon
    public int GetT2Price() { return 4; }
    public float GetT2PriceF() { return 4.0f; }

    // returns the rpice of the tier 3 weapon
    public int GetT3Price() { return 6; }
    public float GetT3PriceF() { return 6.0f; }
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

    public void ResetUI()
    {
        beginroundButton.SetActive(false);
        restart.gameObject.SetActive(false);
        //weaponsButton.gameObject.SetActive(false);
    }

    public void BeginRound()
    {
        hasBegun = true;
        beginroundButton.gameObject.SetActive(false);
    }

    public bool CanBeginRound()
    {
        return hasBegun;
    }
}
