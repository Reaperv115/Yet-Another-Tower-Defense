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
    Player player;
    
    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        price = 5;
        damage = 5;
        firerateinSeconds = 1f;
        mask = LayerMask.GetMask("enemy");
        scoreBoard = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        player = GameObject.Find("Main Camera").GetComponent<Player>();
        score = 0;
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
                    firerateinSeconds = 1f;
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
        Debug.Log(transform.GetChild(0).name);
        int tmpCol = Random.Range(0, colors.Length);
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = colors[tmpCol];
        if (collider.GetComponent<Enemy>().Health <= 0)
        {
            Destroy(collider.gameObject);
            int tmp = player.getScore();
            player.setScore(tmp += 1);
            scoreBoard.text = "Score: " + player.getScore().ToString();
        }
        else
            collider.GetComponent<Enemy>().Health -= damage;
    }

}
