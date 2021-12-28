using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretT2 : WeaponBase
{
    Collider2D collider;
    RaycastHit2D hit;
    Vector3 offSet;
    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        price = 10;
        damage = 10;
        firerateinSeconds = .5f;
        mask = LayerMask.GetMask("enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (collider)
        {
            dir = transform.position - collider.transform.position;
            hit = Physics2D.Raycast(transform.position, transform.up, 40, mask);
            if (firerateinSeconds >= 0)
                if (hit)
                    Fire();
            else
            {
                    firerateinSeconds -= Time.deltaTime;
            }

        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("enemy"))
        {
            collider = collision;
            offSet = collision.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, offSet);
            transform.rotation = rotation * Quaternion.Euler(0, 0, 90);
        }

    }

    void Fire()
    {
        //Destroy(collider.gameObject);
        collider.GetComponent<Enemy>().Health -= damage;
    }
}
