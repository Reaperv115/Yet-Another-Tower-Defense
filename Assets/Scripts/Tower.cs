using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    
    private float fHealth;

    GameManager gm;

    GameObject[] enemy;
    List<GameObject> enemyList;

    TextMeshProUGUI gameOver;
    bool isgameOver;
    bool allenemsGone;

    // Start is called before the first frame update
    void Start()
    {
        fHealth = 1f;
        gameOver = GameObject.Find("game over").GetComponent<TextMeshProUGUI>();
        gm = GameObject.Find("Main Camera").GetComponent<GameManager>();
        isgameOver = false;
        allenemsGone = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float getHealth()
    {
        return fHealth;
    }

    public void setHealth(float health)
    {
        fHealth = health;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("enemy"))
        {
            
            Destroy(collision.gameObject);
            fHealth -= .1f;
            gm.GetHealthBar().localScale = new Vector3(fHealth, 1f);
            if (fHealth <= 0.0f)
            {
                gameOver.text = "Game Over";
                enemy = GameObject.FindGameObjectsWithTag("enemy");
                for (int i = 0; i < enemy.Length; ++i)
                {
                    Destroy(enemy[i]);
                }
                isgameOver = true;
            }

        }
    }


    public bool getisgameOver()
    {
        return isgameOver;
    }
}
