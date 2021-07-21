using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = Resources.Load<GameObject>("enemy");
        Instantiate(enemy, GameObject.Find("enemy starting pos").transform.position, enemy.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
