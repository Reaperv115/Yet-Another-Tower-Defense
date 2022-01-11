using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurretT1 : WeaponBase
{
    TextMeshProUGUI scoreBoard;
    int score;

    Vector3 offSet;
    Collider2D collider;
    RaycastHit2D hit;
    
    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        price = 1;
        damage = 5;
        firerateinSeconds = 0f;
        mask = LayerMask.GetMask("enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (collider)
        {
            Debug.Log("turret 1 firerate: " + firerateinSeconds);
            dir = transform.position - collider.transform.position;
            hit = Physics2D.Raycast(transform.position, transform.up, 40, mask);
            if (firerateinSeconds <= 0)
            {
                if (hit)
                {
                    //Debug.Log("shooting enemy");
                    Fire();
                    firerateinSeconds = 0f;
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
        if (collision.transform.tag.Contains("T"))
        {
            Debug.Log(collision.name);
            collider = collision;
            offSet = collision.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, offSet);
            transform.rotation = rotation * Quaternion.Euler(0, 0, 90);
        }

    }

    void Fire()
    {
        Debug.Log(transform.GetChild(0).name);
        int tmpCol = Random.Range(0, colors.Length);
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = colors[tmpCol];
        if (collider.GetComponent<Enemy>().Health <= 0)
        {
            Destroy(collider.gameObject);
            UpdateScore(collider);
        }
        else
            collider.GetComponent<Enemy>().Health -= damage;
    }

    void UpdateScore(Collider2D collider2D)
    {
        int tmp = player.GetScore();
        if (collider.transform.tag.Equals("eT1"))
        {
            player.SetScore(tmp += 1);
        }
        if (collider.transform.Equals("et2"))
        {
            player.SetScore(tmp += 2);
        }
        if (collider.transform.Equals("et3"))
        {
            player.SetScore(tmp += 3);
        }
        
        
        scoreBoard.text = "Score: " + player.GetScore().ToString();
    }

}
