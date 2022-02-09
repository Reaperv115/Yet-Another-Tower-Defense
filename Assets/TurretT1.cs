using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurretT1 : WeaponBase
{
    TextMeshProUGUI scoreBoard;

    Vector3 offSet;
    Collider2D collider;
    RaycastHit2D hit;
    // Start is called before the first frame update
    void Start()
    {
        price = 1;
        damage = 5;
        firerateinSeconds = 0f;
        mask = LayerMask.GetMask("enemy");
        scoreBoard = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (collider)
        {
            
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
            UpdateScore(collider);
            Destroy(collider.gameObject);
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("ET1");
            GameObject[] enemies2 = GameObject.FindGameObjectsWithTag("ET2");
            GameObject[] enemies3 = GameObject.FindGameObjectsWithTag("ET3");
            if (enemies.Length.Equals(1) && enemies2.Length.Equals(1) && enemies3.Length.Equals(1))
            {
                Debug.Log("YOU WIN");
            }
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
