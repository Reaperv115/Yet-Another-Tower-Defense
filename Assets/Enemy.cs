using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject cam;
    SetMap mapInfo;

    [SerializeField]
    GameObject tower;

    GameObject towerhealthBar;

    // Start is called before the first frame update
    void Start()
    {
        mapInfo = cam.GetComponent<SetMap>();
        towerhealthBar = GameObject.Find("tower health");
        towerhealthBar.GetComponent<Slider>().onValueChanged.AddListener(onValueChanged);
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
                    Destroy(this.gameObject);
                    //towerHealth.transform.GetChild(0).GetComponent<Slider>().value -= 1;
                    Debug.Log(tower.GetComponent<Tower>().getHealth());
                    float tmpvar = tower.GetComponent<Tower>().getHealth();
                    onValueChanged(tmpvar -= 1);
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

    public void onValueChanged(float towerHealth)
    {
        tower.GetComponent<Tower>().setHealth(towerHealth);
    }
}
