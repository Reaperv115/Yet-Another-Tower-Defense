using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMap : MonoBehaviour
{

   
    public GameObject[] pathwayMarkers;
    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log(pathwayMarkers.Length);
        for (int i = 0; i < pathwayMarkers.Length; i++)
        {
            Debug.Log(pathwayMarkers[i].transform.name);
        }
    }
}
