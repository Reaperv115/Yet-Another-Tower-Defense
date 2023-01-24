using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SubsystemsImplementation;

public class WaveSpawner : MonoBehaviour
{
    float spawnTimer;
    Transform startingPos;
    
    int randomEnem;

    GameObject enemInst;
    private float intermission;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = 1f;
        startingPos = GameManager.instance.GetEnemyStartingPosition().transform;
        intermission = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.GetNextRound())
        {
            VictoryIntermission();
        }
        
        if (WaveManager.instance.GetRound().Equals(6) && WaveManager.instance.GetLevel().Equals(6))
        {
            SceneManager.LoadScene("Victory");
        }
        else
        {
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
                    Debug.Log(WaveManager.instance.enemyhealthCheck.Length);
                    if (WaveManager.instance.enemyhealthCheck.Length <= 0)
                    {
                        WaveManager.instance.SetSpawn(false);
                        GameManager.instance.GetWeaponsButton().SetActive(true);
                        GameManager.instance.SetNextRound(true);
                        WaveManager.instance.SetRound(WaveManager.instance.GetRound() + 1);
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
                break;
            case 3:
                randomEnem = Random.Range(0, WaveManager.instance.GetRound3Pack().Length);
                enemInst = Instantiate(WaveManager.instance.GetRound3Pack()[randomEnem], startingPos.position, WaveManager.instance.GetRound3Pack()[randomEnem].transform.rotation);
                EnemyManager.instance.enemies.Add(enemInst);
                break;
            case 4:
                randomEnem = Random.Range(0, WaveManager.instance.GetRound3Pack().Length);
                enemInst = Instantiate(WaveManager.instance.GetRound3Pack()[randomEnem], startingPos.position, WaveManager.instance.GetRound3Pack()[randomEnem].transform.rotation);
                EnemyManager.instance.enemies.Add(enemInst);
                break;
            case 5:
                randomEnem = Random.Range(0, WaveManager.instance.GetRound3Pack().Length);
                enemInst = Instantiate(WaveManager.instance.GetRound3Pack()[randomEnem], startingPos.position, WaveManager.instance.GetRound3Pack()[randomEnem].transform.rotation);
                EnemyManager.instance.enemies.Add(enemInst);
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
        }
        else
        {
            if (intermission < 3f) GameManager.instance.MoveOn();
            intermission -= .05f;
            GameManager.instance.YouWON();
        }
    }
}
