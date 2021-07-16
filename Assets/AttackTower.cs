using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTower : MonoBehaviour
{
    Sprite[] path;

    [SerializeField]
    GameObject enemy;

    [SerializeField]
    Transform enemystartPos;

    Quaternion rot;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemy, enemystartPos.position, enemy.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
