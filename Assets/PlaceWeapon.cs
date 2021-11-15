using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceWeapon : MonoBehaviour
{
    Vector3 weaponSpot;
    GameObject mainCam;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlantWeapon()
    {
        weaponSpot = GameObject.Find("Spot").transform.position;
        mainCam.GetComponent<selectWeapon>().GetMainWeapon().transform.position = weaponSpot;
        mainCam.GetComponent<selectWeapon>().isselectingWeapon(false);
    }
}
