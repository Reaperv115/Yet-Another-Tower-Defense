using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurretT1 : WeaponBase
{
    GameManager gm;

    Vector3 offSet;
    new Collider2D collider;
    RaycastHit2D hit;
    // Start is called before the first frame update
    void Start()
    {
        visionDistance = 15;
        price = 1;
        damage = 65;
        firerateinSeconds = .2f;
        mask = LayerMask.GetMask("enemy");
        gm = GameObject.Find("Main Camera").GetComponent<GameManager>();
        Debug.Log("fire rate in seconds");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
       if (firerateinSeconds <= 0)
       {
           if (collider)
           {
               hit = Physics2D.Raycast(transform.position, transform.up, visionDistance, mask);
               if (hit)
               {
                    Debug.Log("enemy hit");
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
        switch (collider.tag)
        {
            case "ET1":
                {
                    Debug.Log("enemy health: " + collider.GetComponent<Enemy1>().Health);
                    if (collider.GetComponent<Enemy1>().Health <= 0)
                    {
                        Debug.Log(collider);
                        UpdateScore(collider);
                        Destroy(collider.gameObject);
                    }
                    else
                    {
                        collider.GetComponent<Enemy1>().Health -= damage;
                    }
                    break;
                }
            case "ET2":
                {
                    if (collider.GetComponent<Enemy2>().Health <= 0)
                    {
                        UpdateScore(collider);
                        Destroy(collider.gameObject);
                    }
                    else
                    {
                        collider.GetComponent<Enemy2>().Health -= damage;
                    }
                    break;
                }
            case "ET3":
                {
                    if (collider.GetComponent<Enemy3>().Health <= 0)
                    {
                        UpdateScore(collider);
                        Destroy(collider.gameObject);
                    }
                    else
                    {
                        collider.GetComponent<Enemy3>().Health -= damage;
                    }
                    break;
                }
            default:
                break;
        }
    }

    void UpdateScore(Collider2D collider2D)
    {
        Debug.Log(gm);
        int tmp = gm.GetScore();
        switch (collider2D.tag)
        {
            case "ET1":
                {
                    gm.SetScore(tmp + 1);
                    break;
                }
            case "ET2":
                {
                    gm.SetScore(tmp + 2);
                    break;
                }
            case "ET3":
                {
                    gm.SetScore(tmp + 3);
                    break;
                }
            default:
                break;
        }
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

        gm.GetScoreBoard().text = "score: " + gm.GetScore().ToString();
    }

}
