using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    GameObject[] enemies;
    GameObject enemy;
    GameObject tower;
    [SerializeField]
    GameObject playButton;
    int enemyIndex;

    float  spawnTimer = 5.0f;
    bool spawn;
    // Start is called before the first frame update
    void Start()
    {
        spawn = false;
        enemies = new GameObject[6];
        enemy = Resources.Load<GameObject>("enemy car (Tier 1)");
        tower = GameObject.Find("tower");
        enemyIndex = 0;

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i] = enemy;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playButton.GetComponent<PlayGame>().hasStarted())
        {
            spawn = true;
        }

        if (spawn)
        {
            if (tower.GetComponent<Tower>().getHealth() <= 0f)
            {
                spawn = false;
            }
            else
            {
                if (enemyIndex < enemies.Length)
                {
                    if (spawnTimer <= 0.0f)
                    {
                        Debug.Log("spawning enemy: " + enemyIndex);
                        Instantiate(enemies[enemyIndex], transform.position, enemies[enemyIndex].transform.rotation);
                        spawnTimer = 1.0f;
                        ++enemyIndex;
                    }
                    else
                    {
                        spawnTimer -= .05f;
                    }
                }
                else
                    Debug.Log("YOU WIN!");
                
            }
        }

    }
}
