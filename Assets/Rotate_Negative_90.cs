using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Negative_90 : MonoBehaviour
{
    GameObject weaponDirection;
    GameObject mainweapon;
    [SerializeField]
    GameObject cam;
    Transform pointer;

    // Start is called before the first frame update
    void Start()
    {
        weaponDirection = GameObject.Find("Weapon Direction");
        pointer = weaponDirection.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void rotate_negative_90()
    {
        Vector3 rotation = new Vector3(0, 0, -90);
        cam.GetComponent<selectWeapon>().GetMainWeapon().transform.Rotate(rotation);
    }
}
