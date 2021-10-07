using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    GameObject enemy;
    bool stopSpawning;

    GameObject tower;

    float spawnTimer = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        enemy = Resources.Load<GameObject>("enemy");
        Instantiate(enemy, transform.position, enemy.transform.rotation);
        stopSpawning = false;
        tower = GameObject.Find("tower");
    }

    // Update is called once per frame
    void Update()
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
                spawnTimer -= .025f;
                //Debug.Log(spawnTimer);
            }
        }

    }
}
