using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurretT3 : WeaponBase
{
    TextMeshProUGUI scoreBoard;
    int score;
    Collider2D collider;
    RaycastHit2D hit;
    Vector3 offSet;
    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        price = 3;
        damage = 40;
        firerateinSeconds = 0f;
        mask = LayerMask.GetMask("enemy");
        scoreBoard = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (collider)
        {
            dir = transform.position - collider.transform.position;
            
            if (firerateinSeconds <= 0)
            {
                hit = Physics2D.Raycast(transform.position, transform.up, 40, mask);
                if (hit)
                {
                    firerateinSeconds = 0f;
                    Fire();
                }
            }
            else
            {
                firerateinSeconds -= Time.deltaTime;
            }

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
            Destroy(collider.gameObject);
            UpdateScore(collider);
        }
        else
            collider.GetComponent<EnemyBase>().Health -= damage;
    }
    void UpdateScore(Collider2D collider2D)
    {
        int tmp = player.GetScore();
        if (collider2D.transform.tag.Equals("ET1"))
        {
            player.SetScore(tmp += 1);
        }
        if (collider2D.transform.Equals("ET2"))
        {
            player.SetScore(tmp += 2);
        }
        if (collider2D.transform.Equals("ET3"))
        {
            player.SetScore(tmp += 3);
        }


        scoreBoard.text = "Score: " + player.GetScore().ToString();
    }

    public int GetPrice()
    {
        return price;
    }
}
