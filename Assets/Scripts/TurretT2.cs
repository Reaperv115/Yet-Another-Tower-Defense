using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurretT2 : WeaponBase
{
    GameManager gm;
    new Collider2D collider;
    RaycastHit2D hit;
    Vector3 offSet;
    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        price = 2;
        damage = 55;
        firerateinSeconds = .2f;
        mask = LayerMask.GetMask("enemy");
        gm = GameObject.Find("Main Camera").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // firing mechanics
        if (firerateinSeconds <= 0)
        {
            if (collider)
            {
                hit = Physics2D.Raycast(transform.position, transform.up, 40, mask);
                if (hit)
                {
                    firerateinSeconds = .2f;
                    Fire();
                }
            }
        }
        else
        {
            firerateinSeconds -= Time.deltaTime;
        }

        
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag.Contains("E"))
        {
            collider = collision;
            offSet = collision.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, offSet);
            transform.rotation = rotation * Quaternion.Euler(0, 0, 90);
        }

    }

    void Fire()
    {
        int tmpCol = Random.Range(0, colors.Length);
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = colors[tmpCol];
        if (collider.GetComponent<EnemyBase>().Health <= 0)
        {
            UpdateScore(collider);
            Destroy(collider.gameObject);
        }
        else
            collider.GetComponent<EnemyBase>().Health -= damage;
    }
    void UpdateScore(Collider2D collider2D)
    {
        int tmp = gm.GetScore();
        if (collider2D.transform.tag.Equals("ET1"))
        {
            gm.SetScore(tmp += 1);
        }
        if (collider2D.transform.Equals("ET2"))
        {
            gm.SetScore(tmp += 2);
        }
        if (collider2D.transform.Equals("ET3"))
        {
            gm.SetScore(tmp += 3);
        }
    }
}
