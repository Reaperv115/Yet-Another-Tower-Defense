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

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = 1f;
        startingPos = GameManager.instance.GetEnemyStartingPosition().transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (WaveManager.instance.GetRound().Equals(6) && WaveManager.instance.GetLevel().Equals(6))
        {
            SceneManager.LoadScene("Victory");
        }
        else
        {
            if (WaveManager.instance.GetNumEnemiesToSpawn() > 0)
            {
                if (WaveManager.instance.CanSpawn())
                {
                    if (spawnTimer <= 0f)
                    {
                        SpawnEnemy(WaveManager.instance.GetRound());
                        spawnTimer = 1f;
                    }
                    else
                        spawnTimer -= Time.deltaTime;
                }
            }
            else
            {
                WaveManager.instance.enemyhealthCheck = GameObject.FindGameObjectsWithTag("enemy");
                Debug.Log(WaveManager.instance.enemyhealthCheck.Length);
                if (WaveManager.instance.enemyhealthCheck.Length <= 0)
                {
                    WaveManager.instance.SetLevel(WaveManager.instance.GetLevel() + 1);
                    WaveManager.instance.SetRound(WaveManager.instance.GetRound() + 1);
                }
            }
        }
    }
    void SpawnEnemy(int currRound)
    {
        switch (currRound)
        {
            case 5:
                GameObject enemInst = Instantiate(WaveManager.instance.GetRound1Pack(), startingPos.position, WaveManager.instance.GetRound1Pack().transform.rotation);
                EnemyManager.instance.enemies.Add(enemInst);
                WaveManager.instance.SetNumEnemiesToSpawn();
                break;
            //case 2:
            //    randomEnem = Random.Range(0, WaveManager.instance.GetRound2Pack().Length);
            //    Instantiate(WaveManager.instance.GetRound2Pack()[randomEnem], startingPos.position, WaveManager.instance.GetRound2Pack()[randomEnem].transform.rotation);
            //    break;
            //case 3:
            //    randomEnem = Random.Range(0, WaveManager.instance.GetRound3Pack().Length);
            //    Instantiate(WaveManager.instance.GetRound3Pack()[randomEnem], startingPos.position, WaveManager.instance.GetRound3Pack()[randomEnem].transform.rotation);
            //    break;
            //case 4:
            //    randomEnem = Random.Range(0, WaveManager.instance.GetRound3Pack().Length);
            //    Instantiate(WaveManager.instance.GetRound3Pack()[randomEnem], startingPos.position, WaveManager.instance.GetRound3Pack()[randomEnem].transform.rotation);
            //    break;
            //case 5:
            //    randomEnem = Random.Range(0, WaveManager.instance.GetRound3Pack().Length);
            //    Instantiate(WaveManager.instance.GetRound3Pack()[randomEnem], startingPos.position, WaveManager.instance.GetRound3Pack()[randomEnem].transform.rotation);
            //    break;
        }
    }
}
