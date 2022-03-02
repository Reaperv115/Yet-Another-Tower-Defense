using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour
{

    protected int pathIndex;
    protected int pathindexPoint;

    protected bool attackTower;


    protected float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { }
        

    public int Health { get; set; }
}
