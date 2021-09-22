using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMap : MonoBehaviour
{

    GameObject path;
    GameObject barricade;

    float spawnY;
    Vector2 spawnPosition;

    [HideInInspector]
    public Transform[] pathwayMarkers;
    // Start is called before the first frame update
    void Start()
    {

        path = Resources.Load<GameObject>("Path/pathway");
        barricade = Resources.Load<GameObject>("Path/Path Accessories/barricade");
        pathwayMarkers = Resources.LoadAll<Transform>("Path Markers");

        for (int i = 0; i < pathwayMarkers.Length; ++i)
        {
            Instantiate(barricade, path.transform.GetChild(0).transform.position, barricade.transform.rotation);
            Instantiate(barricade, path.transform.GetChild(1).transform.position, barricade.transform.rotation);
            Instantiate(path, pathwayMarkers[i].position, path.transform.rotation);
            path.transform.position = pathwayMarkers[i].position;
        }
    }
}
