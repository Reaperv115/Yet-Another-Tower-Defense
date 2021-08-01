using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    GameObject enemy;

    float spawnTimer = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        enemy = Resources.Load<GameObject>("enemy");
        Instantiate(enemy, GameObject.Find("enemy starting pos").transform.position, enemy.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer <= 0.0f)
        {
            Instantiate(enemy, GameObject.Find("enemy starting pos").transform.position, enemy.transform.rotation);
            spawnTimer = 5.0f;
        }
        else
        {
            spawnTimer -= .025f;
            //Debug.Log(spawnTimer);
        }
    }
}
