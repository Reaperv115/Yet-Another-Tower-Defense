using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    Vector3 offSet;
    Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
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
            Invoke("Fire", .25f);
        }
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("enemy"))
        {
            collider = collision;
            offSet = collision.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, offSet);
            transform.rotation = rotation * Quaternion.Euler(0, 0, 90);
            Invoke("Fire", .25f);
        }
    }
}
