using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        EnemyManager.instance.enemies.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
