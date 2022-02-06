using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    
    GameObject enemy;
    GameObject tower;
    [SerializeField]
    GameObject playButton;
    int enemyIndex;
    int numenemiestoAdd = 6;

    float timebetweenSpawn = 1.0f;
    bool spawn;
    // Start is called before the first frame update
    void Start()
    {
        enemyIndex = 0;
        spawn = false;
        enemy = Resources.Load<GameObject>("enemy car (Tier 1)");
        tower = GameObject.Find("tower");
        enemyIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (playButton.GetComponent<PlayGame>().hasStarted())
        {
            spawn = true;
        }
        //if (tower.GetComponent<Tower>().getHealth() <= 0f)
        //{
        //    spawn = false;
        //}

        if (spawn)
        {
            if (timebetweenSpawn <= 0.0f)
            {
                if (numenemiestoAdd > 0)
                {
                    Instantiate(enemy, transform.position, enemy.transform.rotation);
                    
                    timebetweenSpawn = 1.0f;
                    --numenemiestoAdd;
                }
                else
                {
                    spawn = false;
                    playButton.GetComponent<PlayGame>().SetHasBegun(false);
                }
            }
            else
            {
                timebetweenSpawn -= .05f;
            }
            //if (timebetweenSpawn <= 0.0f)
            //{
                
            //}
            //else
            //{
            //    timebetweenSpawn -= .05f;
            //    Debug.Log(timebetweenSpawn);
            //}

        }
    }

    public int GetNumEnemies()
    {
        return numenemiestoAdd;
    }
}
