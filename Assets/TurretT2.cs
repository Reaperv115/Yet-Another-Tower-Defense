using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretT2 : WeaponBase
{
    Collider2D collider;
    Vector3 offSet;
    // Start is called before the first frame update
    void Start()
    {
        price = 10;
        firerateinSeconds = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("enemy"))
        {
            collider = collision;
            offSet = collision.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, offSet);
            transform.rotation = rotation * Quaternion.Euler(0, 0, 90);
            Invoke("Fire", firerateinSeconds);
        }

    }

    void Fire()
    {
        //Destroy(collider.gameObject);
        collider.GetComponent<Enemy>().Health -= 25f;
    }
}
