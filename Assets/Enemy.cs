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

    bool attackTower;

    Rigidbody2D enemyRb;

    

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
            transform.position = Vector3.MoveTowards(transform.position, tower.transform.position, .02f);
        else
        {
            if (transform.position.Equals(mapInfo.pathwayMarkers[pathIndex].position))
            {
                //Debug.Log("At the next marker." + mapInfo.pathwayMarkers.Length);
                if (pathIndex.Equals(mapInfo.pathwayMarkers.Length - 1))
                {
                    attackTower = true;
                    //transform.position = Vector3.MoveTowards(transform.position, tower.transform.position, .02f);
                }
                else
                {
                    ++pathIndex;
                    //Debug.Log("updating path index. " + mapInfo.pathwayMarkers.Length);
                    transform.position = Vector3.MoveTowards(transform.position, mapInfo.pathwayMarkers[pathIndex].position, .02f);
                }

            }
            else
            {
                //Debug.Log("simply moving the enemy forward. " + mapInfo.pathwayMarkers.Length);
                transform.position = Vector3.MoveTowards(transform.position, mapInfo.pathwayMarkers[pathIndex].position, .02f);
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
