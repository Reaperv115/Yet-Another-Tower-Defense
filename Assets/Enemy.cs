using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject cam;
    SetMap mapInfo;

    [SerializeField]
    GameObject tower;

    GameObject bullet;
    GameObject tmpBullet;
    Transform originalbulletPos;

    // Start is called before the first frame update
    void Start()
    {
        mapInfo = cam.GetComponent<SetMap>();
        bullet = transform.GetChild(0).gameObject;
        originalbulletPos = bullet.transform;
        Debug.Log(tower.transform.position);
    }

    // Update is called once per frame
    void Update()
    {

        if (mapInfo.path.Length.Equals(0))
        {
            mapInfo.path = Resources.LoadAll<GameObject>("Path");
            transform.position = Vector3.MoveTowards(transform.position, mapInfo.path[mapInfo.pathIndex].transform.position, 0.02f);
        }
        else
        {

            if (transform.position.Equals(mapInfo.path[mapInfo.pathIndex].transform.position))
            {
                if (mapInfo.pathIndex.Equals(mapInfo.path.Length - 1))
                {
                    if (bullet.transform.position.Equals(tower.transform.position))
                    {
                        bullet.transform.position = Vector3.zero;
                        Destroy(bullet);
                    }
                    else
                    {
                        bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, tower.transform.position, .02f);

                    }
                }
                else
                {
                    ++mapInfo.pathIndex;
                    transform.position = Vector3.MoveTowards(transform.position, mapInfo.path[mapInfo.pathIndex].transform.position, .02f);
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, mapInfo.path[mapInfo.pathIndex].transform.position, .02f);
            }
        }
    }
}
