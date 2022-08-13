using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurretT2 : WeaponBase
{
    GameManager gm;
    Collider2D enemyCollider;
    RaycastHit2D hit;
    Vector3 offSet;
    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("Main Camera").GetComponent<GameManager>();
        mask = LayerMask.GetMask("enemy");
        firerateinSeconds = .2f;
        visionDistance = 15;
        damage = 55;
        price = 6;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // firing mechanics
        if (firerateinSeconds <= 0)
        {
            if (enemyCollider)
            {
                hit = Physics2D.Raycast(transform.position, transform.up, visionDistance, mask);
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
            enemyCollider = collision;
            offSet = collision.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, offSet);
            transform.rotation = rotation * Quaternion.Euler(0, 0, 90);
        }

    }

    void Fire()
    {
        int tmpCol = Random.Range(0, colors.Length);
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = colors[tmpCol];
        if (enemyCollider.GetComponent<EnemyBase>().Health <= 0)
        {
            UpdateScore(enemyCollider);
            Destroy(enemyCollider.gameObject);
        }
        else
            enemyCollider.GetComponent<EnemyBase>().Health -= damage;
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
