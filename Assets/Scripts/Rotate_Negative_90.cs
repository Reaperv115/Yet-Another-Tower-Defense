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
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        // weaponDirection = GameObject.Find("Weapon Direction");
        // pointer = weaponDirection.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (cam)
        {
            player = cam.GetComponent<Player>();
        }
    }

    public void rotate_negative_90()
    {
        Vector3 rotation = new Vector3(0, 0, -90);
        player.GetMainWeapon().transform.Rotate(rotation);
    }
}
