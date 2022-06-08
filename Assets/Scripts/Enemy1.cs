using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : EnemyBase
{
    [SerializeField]
    GameObject cam;
    SetMap mapInfo;
    GameObject tower;

    [SerializeField]
    GameObject track;
    List<Transform> pathwayMarkers;
    // Start is called before the first frame update
    void Start()
    {
        mapInfo = GameObject.Find("battlefield").GetComponent<SetMap>();
        speed = 20f * Time.deltaTime;
        attackTower = false;
        Health = 100;
        pathIndex = 0;
        pathindexPoint = 0;
        pathwayMarkers = new List<Transform>();
        GetPathMarkers();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position);
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
        
        if (transform.position.Equals(pathwayMarkers[pathIndex].transform.position))
        {
            if (pathIndex.Equals(pathwayMarkers.Count - 1))
            {
                attackTower = true;
            }
            else
            {
                ++pathIndex;
                transform.position = Vector3.MoveTowards(transform.position, pathwayMarkers[pathIndex].transform.position, _speed);
            }

        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pathwayMarkers[pathIndex].transform.position, _speed);
        }
    }

    void GetPathMarkers()
    {

        for (int i = 1; i < track.transform.childCount; ++i)
        {
            pathwayMarkers.Add(track.transform.GetChild(i));
        }
    }
}
