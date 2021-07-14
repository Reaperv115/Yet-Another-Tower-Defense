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
        path = Resources.LoadAll<Sprite>("Path");
        Debug.Log(path.Length);
        for (int i = 0; i < path.Length; ++i)
            Debug.Log("square " + i + ":" + path[i].name);

        rot = enemy.transform.rotation * Quaternion.AngleAxis(90, Vector3.forward);
        Instantiate(enemy, enemystartPos.position, enemy.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
