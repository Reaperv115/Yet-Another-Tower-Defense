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
    int pathindexPoint = 2;

    GameObject tower;

    bool attackTower;

    Rigidbody2D enemyRb;

    float speed = .02f;

    // Start is called before the first frame update
    void Start()
    {
        mapInfo = GameObject.Find("battlefield").GetComponent<SetMap>();
        enemyRb = GetComponent<Rigidbody2D>();
        attackTower = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTower)
        {
            tower = GameObject.FindGameObjectWithTag("tower");
            transform.position = Vector3.MoveTowards(transform.position, tower.transform.position, speed);
        }
        else
        {
            if (transform.position.Equals(mapInfo.pathwayMarkers[pathIndex].transform.GetChild(pathindexPoint).position))
            {
                //Debug.Log("At the next marker." + mapInfo.pathwayMarkers.Length);
                if (pathindexPoint.Equals(3))
                {
                    if (pathIndex.Equals(mapInfo.pathwayMarkers.Length - 1))
                    {
                        attackTower = true;

                    }
                    else
                    {
                        ++pathIndex;
                        pathindexPoint = 2;
                        transform.position = Vector3.MoveTowards(transform.position, mapInfo.pathwayMarkers[pathIndex].transform.GetChild(pathindexPoint).position, speed);
                    }
                }
                else
                {
                    ++pathindexPoint;
                    transform.position = Vector3.MoveTowards(transform.position, mapInfo.pathwayMarkers[pathIndex].transform.GetChild(pathindexPoint).position, speed);
                }

            }
            else
            {
                //Debug.Log("simply moving the enemy forward. " + mapInfo.pathwayMarkers.Length);
                transform.position = Vector3.MoveTowards(transform.position, mapInfo.pathwayMarkers[pathIndex].transform.GetChild(pathindexPoint).position, speed);
            }

            //if (mapInfo.pathwayMarkers.Length.Equals(0))
            //{
            //    mapInfo.pathwayMarkers = Resources.LoadAll<Transform>("Path Markers");
            //    //Debug.Log("Just loaded thre markers " + mapInfo.pathwayMarkers.Length);
            //    transform.position = Vector3.MoveTowards(transform.position, mapInfo.pathwayMarkers[pathIndex].transform.position, 0.02f);
            //}
            //else
            //{



            //}


        }

    }

    public GameObject getTower()
    {
        return tower;
    }
}
