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

    GameObject round1Pack;
    GameObject[] round2Pack = new GameObject[2];
    GameObject[] round3Pack = new GameObject[3];
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogError("trying to create a duplicate of the enemy manager");
        round1Car = Resources.Load<GameObject>("enemy car (Tier 1)");
        round2Car = Resources.Load<GameObject>("enemy car (Tier 2)");
        round3Car = Resources.Load<GameObject>("enemy car (tier 3)");

        round1Pack = round1Car;

        round2Pack[0] = round1Car;
        round2Pack[1] = round2Car;

        round3Pack[0] = round1Car;
        round3Pack[1] = round2Car;
        round3Pack[2] = round3Car;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject GetRound1Pack()
    {
        return round1Pack;
    }

    public GameObject[] GetRound2Pack()
    {
        return round2Pack;
    }
    public GameObject[] GetRound3Pack() { return round3Pack; }
}
