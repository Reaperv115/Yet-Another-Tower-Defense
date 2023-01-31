using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    [HideInInspector]
    public List<GameObject> enemies;

    GameObject round1Car;
    GameObject round2Car;
    GameObject round3Car;

    
    // Start is called before the first frame update
    void Start()
    {
        // setting EnemyManager instance
        if (instance == null)
            instance = this;
        else
            Debug.LogError("trying to create a duplicate of the enemy manager");

        // loading enemy car resources
        round1Car = Resources.Load<GameObject>("enemy car (Tier 1)");
        round2Car = Resources.Load<GameObject>("enemy car (Tier 2)");
        round3Car = Resources.Load<GameObject>("enemy car (tier 3)");
    }

    // getters for enemy car objects
    public GameObject GetRound1Car()
    {
        return round1Car;
    }
    public GameObject GetRound2Car()
    {
        return round2Car;
    }
    public GameObject GetRound3Car() 
    {
        return round3Car;
    }
    //
}
