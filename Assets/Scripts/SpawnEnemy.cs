using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    GameObject round1;
    List<GameObject> round2;
    List<GameObject> round3;

    GameObject enemy, enemy2, enemy3;
    GameObject tower;
    [SerializeField]
    GameObject playButton;
    int currentRound;
    int numenemiestoAdd = 6;

    float timebetweenSpawn = 1.0f;
    bool spawn;
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
    }

    // Update is called once per frame
    void Update()
    {
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
                Debug.Log(numenemiestoAdd);
                
                if (numenemiestoAdd > 0)
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
                        default:
                            break;
                    }
                }
                else
                {
                    spawn = false;
                    playButton.GetComponent<PlayGame>().SetHasBegun(false);
                    ++currentRound;
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
}
