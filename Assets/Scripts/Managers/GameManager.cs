using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    GameObject beginroundButton, weaponsButton, startButton, restart;
    [SerializeField]
    GameObject scoreBoard, round, victoryDisplay;
    [SerializeField]
    TextMeshProUGUI weapontoPlace;
    [SerializeField]
    TextMeshProUGUI lackofFunds;

    GameObject nextLevel;

    GameObject canvas;
    [SerializeField]
    private GameObject nextlevelPrep;
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

    string apologies = "Sorry, no turrets available to place";

    bool nextRound;

    private float score = 6f;
    float changeColor = 1;
    float lackoffundsdisplayTimer;
    int currentRound, currentLevel;
    int colorIndex = 0;
    int trackIndex;

    public bool endofLevel = false;
    float nextlevelTimer = 3f;
    string victoryMessage = "Congrats! You've beaten the level! Get ready for the next one!";

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogError("trying to create a duplicate of the game manager");
        healthBar = GameObject.Find("HealthBar").transform.GetChild(1).GetComponent<RectTransform>();
        nextLevel = GameObject.Find("Next Level");
        enemyManager = GameObject.Find("EnemyManager");
        nextRound = false;
        weaponsPanel.gameObject.SetActive(false);
        beginroundButton.gameObject.SetActive(false);
        restart.gameObject.SetActive(false);
        victorydisplayColors = new List<Color>() { Color.blue, Color.cyan, Color.gray, Color.magenta, Color.grey };
        player = GetComponent<Player>();
        trackIndex = 1;
        LoadTrackInst();
        currentRound = currentLevel = 1;
        tower = FindTower(GetTrack());
        canvas = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        if (endofLevel)
        {
            if (nextlevelTimer <= 0f)
            {
                victoryMessage = "";
                nextlevelPrep.GetComponent<TextMeshProUGUI>().text = victoryMessage;
                nextlevelTimer = 3f;
                nextLevel.SetActive(true);
                SetEndofLevel(false);
            }
            else
            {
                nextlevelTimer -= Time.deltaTime;
                victoryMessage = "Congrats! You've beaten the level! Get ready for the next one!";
                nextlevelPrep.GetComponent<TextMeshProUGUI>().text = victoryMessage;
            }
        }

        if (lackoffundsdisplayTimer > 0)
        {
            lackofFunds.text = apologies;
            lackoffundsdisplayTimer -= Time.deltaTime;
        }
        else
            lackofFunds.text = "";
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
    public bool GetEndofLevel() { return endofLevel; }
    public int GetCurrentRound() { return currentRound; }
    public int GetTrackIndex() { return trackIndex; }
    public GameObject GetTrackInst() { return trackInst; }
    public GameObject GetNextLevelButton() { return nextLevel; }
    public RectTransform GetHealthBar() { return healthBar; }
    public TextMeshProUGUI GetLackOfFundsDisplay() { return lackofFunds; }
    public GameObject GetCanvas() { return canvas; }
    public GameObject GetNextLevelPrep() { return nextlevelPrep; }
    public int GetLevel() { return currentLevel; }


    // setters
    public void SetScore(int newScore) { ScoreManager.instance.amount = newScore; }
    public float GetScore() { return score; }
    public void SetCurrentRound(int newRound) { currentRound = newRound; }
    public void SetNextRound(bool nextround) { nextRound = nextround; }
    public void SetNextLevel(bool nextlevel) { endofLevel = nextlevel; }
    public void SetEndofLevel(bool endlevel) { endofLevel = endlevel; }
    public void SetTrackIndex(int newIndex) { trackIndex = newIndex; }
    public void SetDisplayTimer(float displaytimer) { lackoffundsdisplayTimer = displaytimer; }

    public void RestartGame() { SceneManager.LoadScene("Game"); }

    


    public void MoveOn() { victoryDisplay.GetComponent<TextMeshProUGUI>().text = ""; }
    
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
        track = Resources.Load<GameObject>("Levels/" + trackIndex + "/track " + trackIndex);
        trackInst = Instantiate(track, track.transform.position, track.transform.rotation);
        print(track);
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
    void LevelVictory()
    {


        if (nextlevelTimer <= 0f)
        {
            nextlevelTimer = 3f;
            GameManager.instance.GetNextLevelPrep().GetComponent<TextMeshProUGUI>().text = "";
            GameManager.instance.SetNextLevel(false);
            //nextlevelBtn.gameObject.SetActive(true);
        }
        else
        {
            nextlevelTimer -= Time.deltaTime;
            GameManager.instance.GetNextLevelPrep().transform.SetParent(GameManager.instance.GetCanvas().transform);
            GameManager.instance.GetNextLevelPrep().GetComponent<TextMeshProUGUI>().text = "Congrats! You beat the level! Get ready for the next one!";
        }
    }
}
