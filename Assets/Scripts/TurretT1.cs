using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurretT1 : WeaponBase
{
    GameManager gm;

    [SerializeField]
    GameObject cam;

    Vector3 offSet;
    new Collider2D collider;
    RaycastHit2D hit;
    // Start is called before the first frame update
    void Start()
    {
        price = 1;
        damage = 5;
        firerateinSeconds = .5f;
        mask = LayerMask.GetMask("enemy");
        gm = cam.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
       if (firerateinSeconds <= 0)
       {
           if (collider)
           {
               hit = Physics2D.Raycast(transform.position, transform.up, 40, mask);
               if (hit)
               {
                   firerateinSeconds = 1.0f;
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
                    if (collider.GetComponent<Enemy1>().Health <= 0)
                    {
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
        int tmp = gm.GetScore();
        if (collider2D.transform.tag.Equals("ET1"))
        {
            gm.SetScore(tmp += 1);
            Debug.Log(gm.GetScore());
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

    public int GetPrice()
    {
        return price;
    }

}
