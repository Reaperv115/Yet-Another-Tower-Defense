using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject[] path;
    int pathIndex;

    [SerializeField]
    Transform[] pathway;
    int pathwayIndex;

    // Start is called before the first frame update
    void Start()
    {

        path = Resources.LoadAll<GameObject>("Path");
        for (int i = 0; i < pathway.Length; ++i)
        {
            Debug.Log("pathway" + i + ": " + pathway[i].position);
            Instantiate(path[i], pathway[i].position, path[i].transform.rotation);
            path[i].transform.position = pathway[i].position;
            Debug.Log("path" + i + ": " + path[i].transform.position);
        }
        pathwayIndex = 0;
        pathIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.Equals(path[pathIndex].transform.position))
        {
            if (pathIndex.Equals(path.Length - 1))
            {
                Destroy(this);
            }
            else
            {
                ++pathIndex;
                transform.position = Vector3.MoveTowards(transform.position, path[pathIndex].transform.position, .02f);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, path[pathIndex].transform.position, .02f);
        }
    }
}
