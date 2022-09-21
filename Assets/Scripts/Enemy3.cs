using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : EnemyBase
{
    GameObject tower;
    GameManager gm;
    GameObject track;
    List<Transform> pathwayMarkers;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("Main Camera").GetComponent<GameManager>();
        Health = 175;
        speed = 20f * Time.deltaTime;
        attackTower = false;
        pathIndex = 0;
        track = gm.GetTrack();
        pathwayMarkers = new List<Transform>();
        GetPathMarkers();
    }

    // Update is called once per frame
    void Update()
    {
        // attacking the tower if
        // at the end of the track
        if (attackTower)
        {
            tower = gm.FindTower(gm.GetTrack());
            transform.position = Vector3.MoveTowards(transform.position, tower.transform.position, speed);
        }
        // else make the enemy go
        else { Go(speed); }
    }

    // guiding the enemy through the track
    public override void Go(float _speed)
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

    // getting track info
    void GetPathMarkers()
    {
         for (int i = 0; i < track.transform.GetChild(0).transform.childCount; ++i)
        {   
            pathwayMarkers.Add(track.transform.GetChild(0).transform.GetChild(i));
        }
    }
}
