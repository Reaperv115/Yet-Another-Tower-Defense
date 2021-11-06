using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    GameObject enemy;
    GameObject tower;
    GameObject playButton;

    float spawnTimer = 5.0f;
    bool spawn;
    // Start is called before the first frame update
    void Start()
    {
        playButton = GameObject.Find("BeginGame");
        Debug.Log(playButton);
        spawn = false;

        enemy = Resources.Load<GameObject>("enemy");
        tower = GameObject.Find("tower");
    }

    // Update is called once per frame
    void Update()
    {
        if (playButton.GetComponent<PlayGame>().hasStarted())
        {
            //Debug.Log("start spawning");
            spawn = true;
        }

        //Debug.Log(spawn);
        if (spawn)
        {
            if (tower.GetComponent<Tower>().getHealth() <= 0f)
            {
                return;
            }
            else
            {
                if (spawnTimer <= 0.0f)
                {
                    Instantiate(enemy, transform.position, enemy.transform.rotation);
                    spawnTimer = 5.0f;
                }
                else
                {
                    spawnTimer -= .05f;
                    Debug.Log(spawnTimer);
                }
            }
        }

    }
}
