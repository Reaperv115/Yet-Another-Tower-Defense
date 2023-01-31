using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SubsystemsImplementation;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner instance;
    float spawnTimer;
    [HideInInspector]
    public Transform startingPos;
    GameObject tower;
    
    int randomEnem;

    GameObject enemInst;
    private float intermission;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = 1f;
        startingPos = GameManager.instance.GetEnemyStartingPosition().transform;
        intermission = 5f;
        tower = GameManager.instance.FindTower(GameManager.instance.GetTrack());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.GetNextRound())
        {
            VictoryIntermission();
        }
        if (WaveManager.instance.CanSpawn())
        {
            if (WaveManager.instance.GetNumEnemiesToSpawn() > 0)
            {
                if (spawnTimer <= 0f)
                {
                    SpawnEnemy(WaveManager.instance.GetRound());
                    spawnTimer = 1f;
                }
                else
                    spawnTimer -= Time.deltaTime;

            }
            else
            {
                WaveManager.instance.enemyhealthCheck = GameObject.FindGameObjectsWithTag("enemy");
                if (WaveManager.instance.enemyhealthCheck.Length <= 0)
                {
                    if (tower.GetComponent<Tower>().GetHealth() <= 0f)
                    {
                        SceneManager.LoadScene("Defeat");
                    }
                    else
                    {
                        WaveManager.instance.SetSpawn(false);
                        GameManager.instance.GetWeaponsButton().SetActive(true);
                        GameManager.instance.SetNextRound(true);
                        if (WaveManager.instance.GetLevel().Equals(5) && WaveManager.instance.GetRound().Equals(5))
                        {
                            SceneManager.LoadScene("Victory");
                        }
                        else
                        {
                            if (!WaveManager.instance.GetRound().Equals(5))
                            {
                                WaveManager.instance.SetRound(WaveManager.instance.GetRound() + 1);

                            }
                            else
                            {
                                if (!WaveManager.instance.GetLevel().Equals(5))
                                {
                                    GameManager.instance.GetNextLevelButton().SetActive(true);
                                    WaveManager.instance.SetLevel(WaveManager.instance.GetLevel() + 1);
                                }
                                else
                                {
                                    SceneManager.LoadScene("Victory");
                                }
                            }
                        }
                    }
                }
            }
        }
        
    }
    void SpawnEnemy(int currRound)
    {
        switch (currRound)
        {
            case 1:
                enemInst = Instantiate(WaveManager.instance.GetRound1Pack(), startingPos.position, WaveManager.instance.GetRound1Pack().transform.rotation);
                EnemyManager.instance.enemies.Add(enemInst);
                WaveManager.instance.SetNumEnemiesToSpawn();
                break;
            case 2:
                randomEnem = Random.Range(0, WaveManager.instance.GetRound2Pack().Length);
                enemInst = Instantiate(WaveManager.instance.GetRound2Pack()[randomEnem], startingPos.position, WaveManager.instance.GetRound2Pack()[randomEnem].transform.rotation);
                EnemyManager.instance.enemies.Add(enemInst);
                WaveManager.instance.SetNumEnemiesToSpawn();
                break;
            case 3:
                randomEnem = Random.Range(0, WaveManager.instance.GetRound3Pack().Length);
                enemInst = Instantiate(WaveManager.instance.GetRound3Pack()[randomEnem], startingPos.position, WaveManager.instance.GetRound3Pack()[randomEnem].transform.rotation);
                EnemyManager.instance.enemies.Add(enemInst);
                WaveManager.instance.SetNumEnemiesToSpawn();
                break;
            case 4:
                randomEnem = Random.Range(0, WaveManager.instance.GetRound3Pack().Length);
                enemInst = Instantiate(WaveManager.instance.GetRound3Pack()[randomEnem], startingPos.position, WaveManager.instance.GetRound3Pack()[randomEnem].transform.rotation);
                EnemyManager.instance.enemies.Add(enemInst);
                WaveManager.instance.SetNumEnemiesToSpawn();
                break;
            case 5:
                randomEnem = Random.Range(0, WaveManager.instance.GetRound3Pack().Length);
                enemInst = Instantiate(WaveManager.instance.GetRound3Pack()[randomEnem], startingPos.position, WaveManager.instance.GetRound3Pack()[randomEnem].transform.rotation);
                EnemyManager.instance.enemies.Add(enemInst);
                WaveManager.instance.SetNumEnemiesToSpawn();
                break;
        }
    }

    void VictoryIntermission()
    {
        if (intermission <= 0.0f)
        {
            GameManager.instance.GetBeginRoundButton().SetActive(true);
            intermission = 5.0f;
            GameManager.instance.SetNextRound(false);
            GameManager.instance.MoveOn();
            WaveManager.instance.SetMaxNumEnemiesToSpawn();
        }
        else
        {
            if (intermission < 3f) GameManager.instance.MoveOn();
            intermission -= .05f;
            GameManager.instance.YouWON();
        }
    }
}
