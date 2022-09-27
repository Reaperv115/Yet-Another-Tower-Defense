using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject beginroundButton, weaponsButton, startButton, restart, nextLevel;
    //GameObject beginroundbuttonInst, weaponsbtnInst, startbuttonInst, restartInst, nextlevelInst;
    [SerializeField]
    GameObject scoreBoard, round, victoryDisplay;
    //GameObject scoreboardInst, roundInst, victorydisplayInst;
    [SerializeField]
    TextMeshProUGUI weapontoPlace;
    //TextMeshProUGUI weapontoplaceInst;

    // enemies to load and spawn-in
    GameObject enemy, enemy2, enemy3;

    [SerializeField]
    GameObject weaponsPanel;
    //GameObject weaponspanelInst;
    GameObject canvas;

    RectTransform healthBar;

    List<Color>victorydisplayColors;

    GameObject enemystartingPos;
    GameObject track, trackInst;
    SpawnEnemy spawnEnemy;

    Scene activeScene;
    Player player;

    bool hasBegun;

    bool nextRound;
    bool activateweaponsPanel = false;

    private float score = 6f;
    int currentRound;
    float changeColor = 3;
    int colorIndex = 0, colorIndex2 = 1;
    int trackIndex;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
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
        enemystartingPos = GameObject.Find("enemy starting tile");
        player = GetComponent<Player>();
        trackIndex = 1;
        LoadTrackInst();
        nextLevel.gameObject.SetActive(false);
        healthBar = GameObject.Find("HealthBar").transform.GetChild(1).GetComponent<RectTransform>();
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
    public TextMeshProUGUI GetWeaponToPlaceDisplay() { return weapontoPlace; }
    public GameObject GetTier1Enemy() { return enemy; }
    public GameObject GetTier2Enemy() { return enemy2; }
    public GameObject GetTier3Enemy() { return enemy3; }
    public GameObject GetBeginRoundButton() { return beginroundButton; }
    public GameObject GetWeaponsButton() { return weaponsButton; }
    public GameObject GetStartButton() { return startButton; }
    public GameObject GetWeaponsPanel() { return weaponsPanel; }
    public GameObject GetRestartButton() { return restart; }
    public bool GetNextRound() { return nextRound; }
    public int GetCurrentRound() { return currentRound; }
    public int GetTrackIndex() { return trackIndex; }
    public GameObject GetTrackInst() { return trackInst; }
    public GameObject GetNextLevelButton() { return nextLevel; }
    public RectTransform GetHealthBar() { return healthBar; }


    // setters
    public void SetScore(float newScore) { score = newScore; }
    public float GetScore() { return score; }
    public void SetCurrentRound(int newRound) { currentRound = newRound; }
    public void SetNextRound(bool nextround) { nextRound = nextround; }
    public void SetTrackIndex(int newIndex) { trackIndex = newIndex; }

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
        victoryDisplay.GetComponent<TextMeshProUGUI>().color = Color.Lerp(victorydisplayColors[colorIndex], victorydisplayColors[colorIndex2], Mathf.PingPong(Time.time, 8));
    }

    public void ResetUI()
    {
        beginroundButton.SetActive(false);
        restart.gameObject.SetActive(false);
        //weaponsButton.gameObject.SetActive(false);
    }

    public void LoadTrackInst()
    {
        track = Resources.Load<GameObject>("Levels/" + GetTrackIndex() + "/track");
        Debug.Log(track);
        trackInst = Instantiate(track, track.transform.position, track.transform.rotation);
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

    public void SetHasBegun(bool hasbegun)
    {
        hasBegun = hasbegun;
    }

    public GameObject GetTrack()
    {
        return track;
    }

    public GameObject FindTower(GameObject track)
    {
        int i = 0;
        for (;i < track.transform.childCount;)
        {
            if (track.transform.GetChild(i).name.Equals("tower"))
                break;
            else
                ++i;
        }
        track.transform.GetChild(i).GetComponent<Tower>().setHealth(1f);
        return track.transform.GetChild(i).gameObject;
    }
}
