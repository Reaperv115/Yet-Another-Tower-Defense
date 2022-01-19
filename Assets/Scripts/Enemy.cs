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

    float speed;

    // Start is called before the first frame update
    void Start()
    {
        mapInfo = GameObject.Find("battlefield").GetComponent<SetMap>();
        enemyRb = GetComponent<Rigidbody2D>();
        attackTower = false;
        Health = 100;
        speed = 10f * Time.deltaTime;
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
            Go(speed);
            //if (transform.position.Equals(mapInfo.pathwayMarkers[pathIndex].transform.GetChild(pathindexPoint).position))
            //{
            //    if (pathindexPoint.Equals(3))
            //    {
            //        if (pathIndex.Equals(mapInfo.pathwayMarkers.Length - 1))
            //        {
            //            attackTower = true;

            //        }
            //        else
            //        {
            //            ++pathIndex;
            //            pathindexPoint = 2;
            //            transform.position = Vector3.MoveTowards(transform.position, mapInfo.pathwayMarkers[pathIndex].transform.GetChild(pathindexPoint).position, speed);
            //        }
            //    }
            //    else
            //    {
            //        ++pathindexPoint;
            //        transform.position = Vector3.MoveTowards(transform.position, mapInfo.pathwayMarkers[pathIndex].transform.GetChild(pathindexPoint).position, speed);
            //    }

            //}
            //else
            //{
            //    transform.position = Vector3.MoveTowards(transform.position, mapInfo.pathwayMarkers[pathIndex].transform.GetChild(pathindexPoint).position, speed);
            //}


        }

    }

    void Go(float _speed)
    {
        if (transform.position.Equals(mapInfo.pathwayMarkers[pathIndex].transform.GetChild(pathindexPoint).position))
        {
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
                    transform.position = Vector3.MoveTowards(transform.position, mapInfo.pathwayMarkers[pathIndex].transform.GetChild(pathindexPoint).position, _speed);
                }
            }
            else
            {
                ++pathindexPoint;
                transform.position = Vector3.MoveTowards(transform.position, mapInfo.pathwayMarkers[pathIndex].transform.GetChild(pathindexPoint).position, _speed);
            }

        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, mapInfo.pathwayMarkers[pathIndex].transform.GetChild(pathindexPoint).position, _speed);
        }
    }

    public GameObject getTower()
    {
        return tower;
    }

    public int Health { get; set; }
}
