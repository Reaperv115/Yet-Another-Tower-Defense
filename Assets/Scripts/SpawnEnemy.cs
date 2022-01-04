using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    GameObject enemy;
    GameObject tower;
    [SerializeField]
    GameObject playButton;

    float  spawnTimer = 5.0f;
    bool spawn;
    // Start is called before the first frame update
    void Start()
    {
        spawn = false;

        enemy = Resources.Load<GameObject>("enemy");
        tower = GameObject.Find("tower");
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
                if (spawnTimer <= 0.0f)
                {
                    Instantiate(enemy, transform.position, enemy.transform.rotation);
                    spawnTimer = 1.0f;
                }
                else
                {
                    spawnTimer -= .05f;
                    //Debug.Log(spawnTimer);
                }
            }
        }

    }
}
