using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject cam;
    SetMap mapInfo;

    int pathIndex = 0;

    [SerializeField]
    GameObject tower;
    Tower towerS;

    bool attackTower;

    Rigidbody2D enemyRb;

    // Start is called before the first frame update
    void Start()
    {
        mapInfo = cam.GetComponent<SetMap>();
        enemyRb = GetComponent<Rigidbody2D>();
        towerS = tower.GetComponent<Tower>();
        attackTower = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTower)
            transform.position = Vector3.MoveTowards(transform.position, tower.transform.position, .02f);
        else
        {


            if (mapInfo.path.Length.Equals(0))
            {
                mapInfo.path = Resources.LoadAll<GameObject>("Path");
                transform.position = Vector3.MoveTowards(transform.position, mapInfo.path[pathIndex].transform.position, 0.02f);
            }
            else
            {
                if (transform.position.Equals(mapInfo.path[pathIndex].transform.position))
                {
                    Debug.Log("path index: " + pathIndex);
                    //Debug.Log("path length: " + mapInfo.path.Length);
                    if (pathIndex.Equals(mapInfo.path.Length - 1))
                    {
                        attackTower = true;
                        //transform.position = Vector3.MoveTowards(transform.position, tower.transform.position, .02f);
                    }
                    else
                    {
                        ++pathIndex;
                        transform.position = Vector3.MoveTowards(transform.position, mapInfo.path[pathIndex].transform.position, .02f);
                    }

                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, mapInfo.path[pathIndex].transform.position, .02f);
                }
            }
        }

    }

    public GameObject getTower()
    {
        return tower;
    }
}
