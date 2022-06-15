using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    GameManager gm;

    GameObject round1;
    List<GameObject> round2, round3;
    GameObject tower;
    
    int numenemiestoAdd, maxnumnEnemies;

    float intermission = 5.0f;
    float timebetweenSpawn = 1.0f;
    bool spawn;
    // Start is called before the first frame update
    void Start()
    {
        // getting the Game Manager component
        gm = GameObject.Find("Main Camera").GetComponent<GameManager>();
        spawn = false;
        tower = GameObject.Find("tower");
        round1 = gm.GetTier1Enemy();

        round2 = new List<GameObject>();
        round2.Add(gm.GetTier2Enemy());
        round2.Add(gm.GetTier3Enemy());

        round3 = new List<GameObject>();
        round3.Add(gm.GetTier1Enemy());
        round3.Add(gm.GetTier2Enemy());
        round3.Add(gm.GetTier3Enemy());

        numenemiestoAdd = 6;
        maxnumnEnemies = numenemiestoAdd;
    }

    // Update is called once per frame
    void Update()
    {
        // giving the player a slight intermission between rounds
        if (gm.GetNextRound())
        {
            if (intermission <= 0.0f)
            {
                gm.GetPlayButton().SetActive(true);
                intermission = 5.0f;
                gm.SetNextRound(false);
            }
            else
            {
                if (intermission < 3f)
                {
                    gm.MoveOn();
                }
                intermission -= .0000005f;
                gm.YouWON();
            }
        }

        // giving the go-ahead to start spawning the appropriate enemies
        if (gm.GetPlayButton().GetComponent<PlayGame>().hasStarted())
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
                    GameObject[] enemies = GameObject.FindGameObjectsWithTag("ET1");
                    GameObject[] enemies2 = GameObject.FindGameObjectsWithTag("ET2");
                    GameObject[] enemies3 = GameObject.FindGameObjectsWithTag("ET3");
                    if (enemies.Length.Equals(0) && enemies2.Length.Equals(0) && enemies3.Length.Equals(0))
                    {
                        if (gm.GetCurrentRound().Equals(5))
                            gm.GetVictoryDisplay().text = "YOU BEAT THE GAME";
                        else
                        {
                            spawn = false;
                            gm.GetPlayButton().GetComponent<PlayGame>().SetHasBegun(false);
                            gm.SetCurrentRound(gm.GetCurrentRound() + 1);
                            gm.SetNextRound(true);
                            maxnumnEnemies += 5;
                            numenemiestoAdd = maxnumnEnemies;
                            gm.GetWeaponsButton().SetActive(true);
                        }
                    }
                    
                }
                else
                    Spawn(gm.GetCurrentRound());
            }
            else
                timebetweenSpawn -= .05f;

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
                    Instantiate(gm.GetTier1Enemy(), transform.position, gm.GetTier1Enemy().transform.rotation);
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

    public int GetNumEnemies()
    {
        return numenemiestoAdd;
    }
}
