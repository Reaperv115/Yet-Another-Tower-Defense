using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMap : MonoBehaviour
{

    GameObject path;
    GameObject barricade;

    float spawnY;
    Vector2 spawnPosition;

   
    public GameObject[] pathwayMarkers;
    // Start is called before the first frame update
    void Start()
    {

        path = Resources.Load<GameObject>("Path/pathway");
        barricade = Resources.Load<GameObject>("Path/Path Accessories/barricade");
        pathwayMarkers = GameObject.FindGameObjectsWithTag("pathway");

        for (int i = 0; i < pathwayMarkers.Length; ++i)
        {
            Debug.Log(pathwayMarkers[i].name, pathwayMarkers[i]);
            if (i.Equals(3) || i.Equals(2))
                continue;
            else
                Instantiate(barricade, pathwayMarkers[i].transform.GetChild(0).transform.position, pathwayMarkers[i].transform.rotation);
                Instantiate(barricade, pathwayMarkers[i].transform.GetChild(1).transform.position, pathwayMarkers[i].transform.rotation);
        }   

        for (int i = 0; i < pathwayMarkers.Length; ++i)
        {
            if (i.Equals(3))
            {
                pathwayMarkers[i] = GameObject.Find("corner 0");
            }
            if (i.Equals(4))
            {
                pathwayMarkers[i] = GameObject.Find("corner 1");
            }
            if (i != 3 && i != 4)
                pathwayMarkers[i] = GameObject.Find("pathway " + i);
        }
    }
}
