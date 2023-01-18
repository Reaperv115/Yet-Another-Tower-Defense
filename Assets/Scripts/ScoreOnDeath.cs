using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreOnDeath : MonoBehaviour
{
    [SerializeField]
    int amount;

    private void OnDestroy()
    {
        Debug.Log("incrementing score using score manager instance");
        ScoreManager.instance.amount += amount;
        EnemyManager.instance.enemies.Remove(this.gameObject);
    }
}
