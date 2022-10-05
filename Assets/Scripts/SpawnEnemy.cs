using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    GameManager gm;

    GameObject round1;
    List<GameObject> round2, round3;
    GameObject tower;
    GameObject[] enemies;
    GameObject enemyInst;
    
    int numenemiestoAdd, maxnumnEnemies;

    float intermission = 5.0f;
    float timebetweenSpawn = 1.0f;
    bool spawn;
    bool begintrackingEnemies;
    int enemyIndex;
    // Start is called before the first frame update
    void Start()
    {
        // getting the Game Manager component
        gm = GameObject.Find("Main Camera").GetComponent<GameManager>();
        spawn = false;
        round1 = gm.GetTier1Enemy();
        tower = gm.GetTower();

        round2 = new List<GameObject>();
        round2.Add(gm.GetTier2Enemy());
        round2.Add(gm.GetTier3Enemy());

        round3 = new List<GameObject>();
        round3.Add(gm.GetTier1Enemy());
        round3.Add(gm.GetTier2Enemy());
        round3.Add(gm.GetTier3Enemy());
        numenemiestoAdd = 10;
        maxnumnEnemies = numenemiestoAdd;

        enemies = new GameObject[numenemiestoAdd];
        begintrackingEnemies = false;
        enemyIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemyhealthCheck = GameObject.FindGameObjectsWithTag("enemy");
        foreach(GameObject enemy in enemyhealthCheck)
        {
            switch(enemy.transform.name)
            {
                case "enemy car (Tier 1)(Clone)":
                {
                    if (enemy.GetComponent<Enemy1>().Health <= 0)
                    {
                        Destroy(enemy);
                        gm.SetScore(gm.GetScore() + 1);
                    }
                    break;
                }
            }
        }

        // giving the player a slight intermission between rounds
        if (gm.GetNextRound())
        {
            if (intermission <= 0.0f)
            {
                gm.GetBeginRoundButton().SetActive(true);
                intermission = 5.0f;
                gm.SetNextRound(false);
                gm.MoveOn();
            }
            else
            {
                if (intermission < 3f)
                {
                    gm.MoveOn();
                }
                intermission -= .05f;
                gm.YouWON();
            }
        }
        // giving the go-ahead to start spawning the appropriate enemies
        if (gm.CanBeginRound())
        {
            spawn = true;
            gm.GetWeaponsButton().SetActive(false);
        }

        //if (tower.GetComponent<Tower>().getHealth() <= 0f) spawn = false;

        // spawning in enemies
        if (spawn)
        {
            if (timebetweenSpawn <= 0.0f)
            {
                
                if (numenemiestoAdd <= 0)
                {
                    GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
                    if (enemies.Length.Equals(0))
                    {
                        if (gm.GetCurrentRound().Equals(5))
                        {
                            gm.GetVictoryDisplay().text = "You Win! Continue to the next round when you're ready!";
                            gm.GetNextLevelButton().gameObject.SetActive(true);
                        }
                        else
                        {
                            spawn = false;
                            gm.SetHasBegun(false);
                            gm.SetCurrentRound(gm.GetCurrentRound() + 1);
                            gm.SetNextRound(true);
                            maxnumnEnemies += 5;
                            numenemiestoAdd = maxnumnEnemies;
                            gm.GetWeaponsButton().SetActive(true);
                        }
                    }

                }
                else
                {
                    Spawn(gm.GetCurrentRound());
                }
            }
            else
                timebetweenSpawn -= .05f;

        }
    }

    // public List<GameObject> GetAliveEnemies()
    // {
    //     return enemies;
    // }

    public bool CanBeginTracking()
    {
        return begintrackingEnemies;
    }

    // function to decide which group of enemies to choose
    // from when spawning enemies
    void Spawn(int currentround)
    {
        switch (currentround)
        {
            case 1:
                {
                    enemyInst = Instantiate(gm.GetTier1Enemy(), transform.position, transform.rotation);
                    timebetweenSpawn = 1.0f;
                    --numenemiestoAdd;
                    break;
                }
            case 2:
                {
                    int randomEnem = Random.Range(0, round2.Count);
                    Instantiate(round2[randomEnem], transform.position, round2[randomEnem].transform.rotation);
                    timebetweenSpawn = 1.0f;
                    --numenemiestoAdd;
                    break;
                }
            case 3:
                {
                    int randomEnem = Random.Range(0, round3.Count);
                    Instantiate(round3[randomEnem], transform.position, round3[randomEnem].transform.rotation);
                    timebetweenSpawn = 1.0f;
                    --numenemiestoAdd;
                    break;
                }
            case 4:
                {
                    int randomEnem = Random.Range(0, round3.Count);
                    Instantiate(round3[randomEnem], transform.position, round3[randomEnem].transform.rotation);
                    timebetweenSpawn = 1.0f;
                    --numenemiestoAdd;
                    break;
                }
            case 5:
                {
                    int randomEnem = Random.Range(0, round3.Count);
                    Instantiate(round3[randomEnem], transform.position, round3[randomEnem].transform.rotation);
                    timebetweenSpawn = 1.0f;
                    --numenemiestoAdd;
                    break;
                }
            default:
                break;
        }
    }

    public int GetNumEnemiesToAdd()
    {
        return numenemiestoAdd;
    }

    public GameObject[] GetEnemies()
    {
        return enemies;
    }

    public void SetSpawn(bool canSpawn)
    {
        spawn = canSpawn;
    }
}
