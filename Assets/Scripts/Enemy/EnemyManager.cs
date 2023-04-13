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
        round1Car = Resources.Load<GameObject>("Basic Enemy");
        round2Car = Resources.Load<GameObject>("Advanced Enemy");
        round3Car = Resources.Load<GameObject>("Ultimate Enemy");
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

    public void IncreaseEnemyHealth()
    {
        round1Car.GetComponent<BasicEnemy>().Health *= 2;
        round2Car.GetComponent<AdvancedEnemy>().Health *= 2;
        round3Car.GetComponent<UltimateEnemy>().Health *= 2;
        print(round1Car.GetComponent<BasicEnemy>().Health);
        print(round2Car.GetComponent<AdvancedEnemy>().Health);
        print(round3Car.GetComponent<UltimateEnemy>().Health);
    }
}
