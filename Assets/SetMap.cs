using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMap : MonoBehaviour
{
    [HideInInspector]
    public GameObject[] path;
    [HideInInspector]
    public int pathIndex;

    [SerializeField]
    Transform[] pathwayMarkers;

    // Start is called before the first frame update
    void Start()
    {
        path = Resources.LoadAll<GameObject>("Path");
        for (int i = 0; i < pathwayMarkers.Length; ++i)
        {
            Instantiate(path[i], pathwayMarkers[i].position, path[i].transform.rotation);
            path[i].transform.position = pathwayMarkers[i].position;
        }
        pathIndex = 0;
    }
}
