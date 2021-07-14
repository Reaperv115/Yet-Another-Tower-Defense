using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject[] path;

    [SerializeField]
    Transform[] pathway;

    // Start is called before the first frame update
    void Start()
    {

        path = Resources.LoadAll<GameObject>("Path");
        for (int i = 0; i < pathway.Length; ++i)
        {
            Instantiate(path[i], pathway[i].position, path[i].transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
