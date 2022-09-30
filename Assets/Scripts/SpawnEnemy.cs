using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    GameManager gm;

    GameObject round1;
    List<GameObject> round2, round3;
    GameObject tower;
    List<GameObject> enemies;
    
    int numenemiestoAdd, maxnumnEnemies;

    float intermission = 5.0f;
    float timebetweenSpawn = 1.0f;
    bool spawn;
    bool begintrackingEnemies;
    // Start is called before the first frame update
    void Start()
    {
        // getting the Game Manager component
        gm = GameObject.Find("Main Camera").GetComponent<GameManager>();
        spawn = false;
        tower = gm.FindTower(gm.GetTrack());
        round1 = gm.GetTier1Enemy();

        round2 = new List<GameObject>();
        round2.Add(gm.GetTier2Enemy());
        round2.Add(gm.GetTier3Enemy());

        round3 = new List<GameObject>();
        round3.Add(gm.GetTier1Enemy());
        round3.Add(gm.GetTier2Enemy());
        round3.Add(gm.GetTier3Enemy());

        numenemiestoAdd = 10;
        maxnumnEnemies = numenemiestoAdd;
        begintrackingEnemies = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.GetEnemies().Count > 0)
            gm.SetBeginTrackingEnemies(true);
        else
            gm.SetBeginTrackingEnemies(false);

        // giving the player a slight intermission between rounds
        if (gm.BeginTrackingEnemies())
        {
            gm.SetEnemyIndex(0);
            foreach(GameObject enemy in gm.GetEnemies())
            {
                Debug.Log("enemy index: " + gm.GetEnemyIndex());
                switch(enemy.transform.name)
                {
                    case "enemy car (Tier 1)(Clone)":
                    {
                        if (enemy.GetComponent<Enemy1>().GetHealth() <= 0f)
                        {
                            GameObject clone = enemy;
                            gm.GetEnemies().RemoveAt(gm.GetEnemyIndex());
                            Destroy(clone);
                            gm.SetScore(gm.GetScore() + 1);
                        }
                        break;
                    }
                }
                    gm.SetEnemyIndex(gm.GetEnemyIndex() + 1);
            }
            
        }
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

        if (tower.GetComponent<Tower>().getHealth() <= 0f) spawn = false;

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

    public List<GameObject> GetAliveEnemies()
    {
        return enemies;
    }

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
                    //gm.AddEnemy(Instantiate(gm.GetTier1Enemy(), transform.position, transform.rotation));

                    GameObject tmpEnem = Instantiate(gm.GetTier1Enemy(), transform.position, gm.GetTier1Enemy().transform.rotation);
                    gm.AddEnemy(tmpEnem);
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
}
