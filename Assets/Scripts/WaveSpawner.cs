using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SubsystemsImplementation;

public class WaveSpawner : MonoBehaviour
{
    float spawnTimer;
    Transform startingPos;
    
    int randomEnem;

    GameObject enemInst;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = 1f;
        startingPos = GameManager.instance.GetEnemyStartingPosition().transform;
        Debug.Log(WaveManager.instance.GetRound());
    }

    // Update is called once per frame
    void Update()
    {
        if (WaveManager.instance.DidWinRound())
        {
            WaveManager.instance.SetRound(WaveManager.instance.GetRound() + 1);
            WaveManager.instance.SetWinRound(false);
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
                        WaveManager.instance.RoundWon();
                        WaveManager.instance.SetSpawn(false);
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
}
