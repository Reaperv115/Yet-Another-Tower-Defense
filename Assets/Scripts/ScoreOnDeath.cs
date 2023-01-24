using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreOnDeath : MonoBehaviour
{
    [SerializeField]
    int amount;

    private void Update()
    {
        if (GetComponent<Enemy1>().Health <= 0f)
        {
            ScoreManager.instance.amount += amount;
            EnemyManager.instance.enemies.Remove(this.gameObject);
        }
    }
}
