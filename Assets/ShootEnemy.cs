using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    Vector3 offSet;
    Ray2D ray;
    RaycastHit2D hit;
    float vision = 5f;
    LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        mask = LayerMask.GetMask("enemy");
        Debug.Log(mask.value);
    }

    // Update is called once per frame
    void Update()
    {
        //hit = Physics2D.Raycast(transform.position, transform.right * vision, mask);
        //Debug.DrawRay(transform.position, transform.right * vision);
        //if (hit.collider.tag.Equals("enemy"))
        //{
        //    hit.collider.GetComponent<Enemy>().Health -= 25f;
        //}
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        offSet = collision.transform.position - transform.position;
        Debug.Log(collision.transform.name);
        if (collision.transform.tag.Equals("enemy"))
        {
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, offSet);
            transform.rotation = rotation * Quaternion.Euler(0, 0, 90);
        }
        
    }
}
