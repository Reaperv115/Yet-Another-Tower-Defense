using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    GameManager gm;

    GameObject tower;
    GameObject enemyInst;

    int numenemiestoAdd, maxnumnEnemies;

    float intermission = 5.0f;
    float timebetweenSpawn = 1.0f;
    bool spawn;
    // Start is called before the first frame update
    void Start()
    {
        spawn = false;
        tower = gm.GetTower();
        
        numenemiestoAdd = 10;
        maxnumnEnemies = numenemiestoAdd;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyManager.instance.enemies.Count > 0)
            EnemyHealthCheck();

        // giving the player a slight intermission between rounds
        if (gm.GetNextRound())
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
                if (intermission < 3f) gm.MoveOn(); 
                intermission -= .05f;
                GameManager.instance.YouWON();
            }
        }
        // giving the go-ahead to start spawning the appropriate enemies
        //if (gm.CanBeginRound())
        //{
        //    spawn = true;
        //    gm.GetWeaponsButton().SetActive(false);
        //}

        // spawning in enemies
        if (spawn)
        {
            if (timebetweenSpawn <= 0.0f)
            {
                
                if (numenemiestoAdd <= 0)
                {
                    if (EnemyManager.instance.enemies.Count.Equals(0))
                    {
                        if (GameManager.instance.GetCurrentRound().Equals(5))
                        {
                            if (GameManager.instance.GetTrackIndex().Equals(5))
                            {
                                GameManager.instance.GetVictoryDisplay().text = "YOU'VE BEATEN THE GAME! CONGRATS! Be sure to be on the lookout for more title by GLITC Gaming!";
                                GameManager.instance.GetRestartButton().gameObject.SetActive(true);
                            }
                            else
                            {
                                GameManager.instance.GetVictoryDisplay().text = "You Win! Continue to the next round when you're ready!";
                                GameManager.instance.GetNextLevelButton().gameObject.SetActive(true);
                            }
                        }
                        else
                        {
                            spawn = false;
                            GameManager.instance.SetHasBegun(false);
                            GameManager.instance.SetCurrentRound(gm.GetCurrentRound() + 1);
                            GameManager.instance.SetNextRound(true);
                            maxnumnEnemies += 5;
                            numenemiestoAdd = maxnumnEnemies;
                            GameManager.instance.GetWeaponsButton().SetActive(true);
                        }
                    }

                }
                else Spawn(GameManager.instance.GetCurrentRound());
            }
            else timebetweenSpawn -= .05f;

        }
    }


    // function to decide which group of enemies to choose
    // from when spawning enemies
    void Spawn(int currentround)
    {
        switch (currentround)
        {
            case 1:
                {
                    enemyInst = Instantiate(WaveManager.instance.GetRound1Pack(), transform.position, transform.rotation);
                    timebetweenSpawn = 1.0f;
                    --numenemiestoAdd;
                    break;
                }
            case 2:
                {
                    int randomEnem = Random.Range(0, WaveManager.instance.GetRound2Pack().Length);
                    enemyInst = Instantiate(WaveManager.instance.GetRound2Pack()[randomEnem], transform.position, WaveManager.instance.GetRound2Pack()[randomEnem].transform.rotation);
                    timebetweenSpawn = 1.0f;
                    --numenemiestoAdd;
                    break;
                }
            case 3:
                {
                    int randomEnem = Random.Range(0, WaveManager.instance.GetRound3Pack().Length);
                    enemyInst = Instantiate(WaveManager.instance.GetRound3Pack()[randomEnem], transform.position, WaveManager.instance.GetRound3Pack()[randomEnem].transform.rotation);
                    timebetweenSpawn = 1.0f;
                    --numenemiestoAdd;
                    break;
                }
            case 4:
                {
                    int randomEnem = Random.Range(0, WaveManager.instance.GetRound3Pack().Length);
                    enemyInst = Instantiate(WaveManager.instance.GetRound3Pack()[randomEnem], transform.position, WaveManager.instance.GetRound3Pack()[randomEnem].transform.rotation);
                    timebetweenSpawn = 1.0f;
                    --numenemiestoAdd;
                    break;
                }
            case 5:
                {
                    int randomEnem = Random.Range(0, WaveManager.instance.GetRound3Pack().Length);
                    enemyInst = Instantiate(WaveManager.instance.GetRound3Pack()[randomEnem], transform.position, WaveManager.instance.GetRound3Pack()[randomEnem].transform.rotation);
                    timebetweenSpawn = 1.0f;
                    --numenemiestoAdd;
                    break;
                }
            default:
                break;
        }
    }

    void EnemyHealthCheck()
    {
        foreach (GameObject enemy in EnemyManager.instance.enemies)
        {
            switch (enemy.transform.name)
            {
                case "enemy car (Tier 1)(Clone)":
                    if (enemy.GetComponent<Enemy1>().Health <= 0) Destroy(enemy);
                    break;
                case "enemy car (Tier 2)(Clone)":
                    if (enemy.GetComponent<Enemy2>().Health <= 0) Destroy(enemy);
                    break;
                case "enemy car (Tier 3)(Clone)":
                    if (enemy.GetComponent<Enemy3>().Health <= 0) Destroy(enemy);
                    break;
            }
        }
    }

    public void SetSpawn(bool canSpawn)
    {
        spawn = canSpawn;
    }
}
