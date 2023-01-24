using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    GameObject beginroundButton, weaponsButton, startButton, restart, nextLevel;
    [SerializeField]
    GameObject scoreBoard, round, victoryDisplay;
    [SerializeField]
    TextMeshProUGUI weapontoPlace;


    [SerializeField]
    GameObject weaponsPanel;
    GameObject enemyManager;

    RectTransform healthBar;

    List<Color>victorydisplayColors;

    GameObject enemystartingPos;
    GameObject track, trackInst;

    GameObject tower;
    TextMeshProUGUI gameOver;
    Player player;

    bool hasBegun;

    bool nextRound;

    private float score = 6f;
    int currentRound;
    float changeColor = 1;
    int colorIndex = 0;
    int trackIndex;
    int t1Price = 2, t2Price = 6, t3Price = 10;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogError("trying to create a duplicate of the game manager");
        healthBar = GameObject.Find("HealthBar").transform.GetChild(1).GetComponent<RectTransform>();
        enemyManager = GameObject.Find("EnemyManager");
        hasBegun = false;
        nextRound = false;
        weaponsPanel.gameObject.SetActive(false);
        beginroundButton.gameObject.SetActive(false);
        restart.gameObject.SetActive(false);
        nextLevel.gameObject.SetActive(false);
        victorydisplayColors = new List<Color>() { Color.blue, Color.cyan, Color.gray, Color.magenta, Color.grey };
        player = GetComponent<Player>();
        trackIndex = 1;
        LoadTrackInst();
        weaponsPanel.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = t1Price.ToString();
        weaponsPanel.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = t2Price.ToString();
        weaponsPanel.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = t3Price.ToString();
        currentRound = 1;
        tower = FindTower(GetTrack());
    }

    // Update is called once per frame
    void Update()
    {
        // displaying score and current round
        scoreBoard.GetComponent<TextMeshProUGUI>().text = "Score: " + ScoreManager.instance.amount;
        round.GetComponent<TextMeshProUGUI>().text = "Round: " + WaveManager.instance.GetRound();
    }

    // getters
    public TextMeshProUGUI GetScoreBoard() { return scoreBoard.GetComponent<TextMeshProUGUI>(); }
    public TextMeshProUGUI GetRound() { return round.GetComponent<TextMeshProUGUI>(); }
    public TextMeshProUGUI GetVictoryDisplay() { return victoryDisplay.GetComponent<TextMeshProUGUI>(); }
    public TextMeshProUGUI GetWeaponToPlaceDisplay() { return weapontoPlace; }
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
    public void SetScore(int newScore) { ScoreManager.instance.amount = newScore; }
    public float GetScore() { return score; }
    public void SetCurrentRound(int newRound) { currentRound = newRound; }
    public void SetNextRound(bool nextround) { nextRound = nextround; }
    public void SetTrackIndex(int newIndex) { trackIndex = newIndex; }

    public void RestartGame() { SceneManager.LoadScene("Game"); }

    


    public void MoveOn() { victoryDisplay.GetComponent<TextMeshProUGUI>().text = ""; }

    // returns the price of the tier 1 weapon
    public int GetT1Price() { return t1Price;  }
    public float GetT1PriceF() { return 4.0f; }

    // returns the price of the tier 2 weapon
    public int GetT2Price() { return t2Price; }
    public float GetT2PriceF() { return 6.0f; }

    // returns the price of the tier 3 weapon
    public int GetT3Price() { return t3Price; }
    public float GetT3PriceF() { return 8.0f; }
    
    public GameObject GetTower() { return tower; }
    public void YouWON()
    {
        victoryDisplay.GetComponent<TextMeshProUGUI>().text = "YOU WIN! Get Ready For The Next Round!";
        if (changeColor <= 0)
        {
            colorIndex = Random.Range(0, victorydisplayColors.Count);
            changeColor = 1f;
        }
        changeColor -= Time.deltaTime;
        victoryDisplay.GetComponent<TextMeshProUGUI>().color = victorydisplayColors[colorIndex];
    }

    public void LoadTrackInst()
    {
        track = Resources.Load<GameObject>("Levels/" + GetTrackIndex() + "/track " + GetTrackIndex());
        trackInst = Instantiate(track, track.transform.position, track.transform.rotation);
        tower = FindTower(track);
        enemystartingPos = FindEnemySpawn(track);
    }

    public void BeginRound()
    {
        //hasBegun = true;
        WaveManager.instance.SetSpawn(true);
        weaponsButton.gameObject.SetActive(false);
        beginroundButton.gameObject.SetActive(false);
    }

    public bool CanBeginRound() { return hasBegun; }

    public void SetHasBegun(bool hasbegun) { hasBegun = hasbegun; }

    public GameObject GetTrack() { return track; }

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
        return track.transform.GetChild(i).gameObject;
    }

    public GameObject FindEnemySpawn(GameObject track)
    {
        int i = 0;
        for (;i < track.transform.childCount;)
        {
            if (track.transform.GetChild(i).name.Equals("enemy starting tile"))
                break;
            else
                ++i;
        }
        return track.transform.GetChild(i).gameObject;
    }

    public GameObject GetEnemyStartingPosition()
    {
        return enemystartingPos;
    }

}
