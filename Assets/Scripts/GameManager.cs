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
        Debug.Log(canvas);
        //LoadUI();
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
        //spawnEnemy = enemystartingPos.GetComponent<SpawnEnemy>();
        player = GetComponent<Player>();
        trackIndex = 1;
        LoadTrackInst();
        //nextLevel.gameObject.SetActive(false);
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
    //public SpawnEnemy GetSpawnEnemyRef() { return spawnEnemy; }
    public bool GetNextRound() { return nextRound; }
    public int GetCurrentRound() { return currentRound; }
    public int GetTrackIndex() { return trackIndex; }


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
        victoryDisplay.GetComponent<TextMeshProUGUI>().color = Color.Lerp(victorydisplayColors[colorIndex], victorydisplayColors[colorIndex2], Mathf.PingPong(Time.time, 1));
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

    // private void LoadUI()
    // {
    //     restart = Resources.Load<GameObject>("UI/Restart");
    //     restartInst = Instantiate(restart, restart.GetComponent<RectTransform>().anchoredPosition, restart.transform.rotation);
    //     restartInst.transform.SetParent(canvas.transform, false);

    //     beginroundButton = Resources.Load<GameObject>("UI/BeginRound");
    //     beginroundbuttonInst = Instantiate(beginroundButton, beginroundButton.GetComponent<RectTransform>().anchoredPosition, beginroundButton.transform.rotation);
    //     beginroundbuttonInst.transform.SetParent(canvas.transform, false);

    //     startButton = Resources.Load<GameObject>("UI/PlayGame");
    //     startbuttonInst = Instantiate(startButton, startButton.GetComponent<RectTransform>().anchoredPosition, startButton.transform.rotation);
    //     startbuttonInst.transform.SetParent(canvas.transform, false);
        
    //     nextLevel = Resources.Load<GameObject>("UI/Next Level");
    //     nextlevelInst = Instantiate(nextLevel, nextLevel.GetComponent<RectTransform>().anchoredPosition, nextLevel.transform.rotation);
    //     nextlevelInst.transform.SetParent(canvas.transform, false);
        
        
    //     weaponsButton = Resources.Load<GameObject>("UI/weaponsBtn");
    //     weaponsbtnInst = Instantiate(weaponsButton, weaponsButton.GetComponent<RectTransform>().anchoredPosition, weaponsButton.transform.rotation);
    //     weaponsbtnInst.transform.SetParent(canvas.transform, false);
        
    //     victoryDisplay = Resources.Load<GameObject>("UI/Victory");
    //     victorydisplayInst = Instantiate(victoryDisplay, victoryDisplay.GetComponent<RectTransform>().anchoredPosition, victoryDisplay.transform.rotation);
    //     victorydisplayInst.transform.SetParent(canvas.transform, false);
        
    //     round = Resources.Load<GameObject>("UI/Round");
    //     roundInst = Instantiate(round, round.GetComponent<RectTransform>().anchoredPosition, round.transform.rotation);
    //     roundInst.transform.SetParent(canvas.transform, false);
        
    //     scoreBoard = Resources.Load<GameObject>("UI/Score");
    //     scoreboardInst = Instantiate(scoreBoard, scoreBoard.GetComponent<RectTransform>().anchoredPosition, scoreBoard.transform.rotation);   
    //     scoreboardInst.transform.SetParent(canvas.transform, false);

    //     weapontoPlace = Resources.Load<TextMeshProUGUI>("UI/Weapon to Place");
    //     weapontoplaceInst = Instantiate(weapontoPlace, weapontoPlace.GetComponent<RectTransform>().anchoredPosition, weapontoPlace.transform.rotation);
    //     weapontoplaceInst.transform.SetParent(canvas.transform, false);
        
    //     weaponsPanel = Resources.Load<GameObject>("UI/Weapons Panel");
    //     weaponspanelInst = Instantiate(weaponsPanel, weaponsPanel.GetComponent<RectTransform>().anchoredPosition, weaponsPanel.transform.rotation);
    //     weaponspanelInst.transform.SetParent(canvas.transform, false);
    // }
}
