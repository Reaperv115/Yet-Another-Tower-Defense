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
    }

    // Update is called once per frame
    void Update()
    {
        
        if (collider)
        {
            Debug.Log("turret 3 firerate: " + firerateinSeconds);
            dir = transform.position - collider.transform.position;
            hit = Physics2D.Raycast(transform.position, transform.up, 40, mask);
            if (firerateinSeconds <= 0)
            {
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
        if (collision.transform.tag.Contains("T"))
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
        if (collider.GetComponent<Enemy>().Health <= 0)
        {
            Destroy(collider.gameObject);
            int tmp = player.GetScore();
            player.SetScore(tmp += 1);
            scoreBoard.text = "Score: " + player.GetScore().ToString();
        }
        else
            collider.GetComponent<Enemy>().Health -= damage;
    }
}
