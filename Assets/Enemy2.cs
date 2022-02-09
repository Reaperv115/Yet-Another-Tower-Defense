using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : EnemyBase
{
    [SerializeField]
    GameObject cam;
    SetMap mapInfo;
    GameObject tower;
    // Start is called before the first frame update
    void Start()
    {
        mapInfo = GameObject.Find("battlefield").GetComponent<SetMap>();
        speed = 10f * Time.deltaTime;
        attackTower = false;
        Health = 100;
        pathIndex = 0;
        pathindexPoint = 2;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(speed);
        if (attackTower)
        {
            tower = GameObject.FindGameObjectWithTag("tower");
            transform.position = Vector3.MoveTowards(transform.position, tower.transform.position, speed);
        }
        else
        {
            Go(speed);


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
}
