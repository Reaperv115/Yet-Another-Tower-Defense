using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI victoryDisplay;

    GameObject round1;
    List<GameObject> round2;
    List<GameObject> round3;

    GameObject enemy, enemy2, enemy3;
    GameObject tower;
    [SerializeField]
    GameObject playButton;
    int currentRound;
    int numenemiestoAdd = 6;

    float intermission = 5.0f;
    float timebetweenSpawn = 1.0f;
    bool spawn;
    bool nextRound;
    // Start is called before the first frame update
    void Start()
    {
        currentRound = 1;
        spawn = false;
        enemy = Resources.Load<GameObject>("enemy car (Tier 1)");
        enemy2 = Resources.Load<GameObject>("enemy car (Tier 2)");
        enemy3 = Resources.Load<GameObject>("enemy car (Tier 3)");
        tower = GameObject.Find("tower");
        round1 = enemy;

        round2 = new List<GameObject>();
        round2.Add(enemy);
        round2.Add(enemy2);

        round3 = new List<GameObject>();
        round3.Add(enemy);
        round3.Add(enemy2);
        round3.Add(enemy3);

        nextRound = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (nextRound)
        {
            if (intermission <= 0.0f)
            {
                numenemiestoAdd = 9;
                playButton.SetActive(true);
                intermission = 5.0f;
                nextRound = false;
            }
            else
            {
                if (intermission < 3f)
                {
                    victoryDisplay.text = "";
                }
                intermission -= .05f;
            }
        }

        if (playButton.GetComponent<PlayGame>().hasStarted())
        {
            spawn = true;
        }
        if (tower.GetComponent<Tower>().getHealth() <= 0f)
        {
            spawn = false;
        }

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
                        if (currentRound.Equals(5))
                        {
                            victoryDisplay.text = "YOU BEAT THE GAME";
                        }
                        else
                        {
                            victoryDisplay.text = "YOU WIN!";
                            spawn = false;
                            playButton.GetComponent<PlayGame>().SetHasBegun(false);
                            ++currentRound;
                            nextRound = true;
                        }
                    }
                    
                }
                else
                {
                    switch (currentRound)
                    {
                        case 1:
                            {
                                Instantiate(enemy, transform.position, enemy.transform.rotation);
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
            }
            else
            {
                timebetweenSpawn -= .05f;
            }

        }
    }

    public int GetNumEnemies()
    {
        return numenemiestoAdd;
    }

    public int GetCurrentRound()
    {
        return currentRound;
    }
}
