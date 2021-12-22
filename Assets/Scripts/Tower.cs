using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    private float fHealth;

    GameObject[] enemy;
    RectTransform bar;

    TextMeshProUGUI gameOver;
    bool isgameOver;

    // Start is called before the first frame update
    void Start()
    {
        fHealth = 1f;
        bar = GameObject.Find("HealthBar").transform.GetChild(1).GetComponent<RectTransform>();
        gameOver = GameObject.Find("game over").GetComponent<TextMeshProUGUI>();
        isgameOver = false;
        //bar.localScale = new Vector3(.4f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getHealth()
    {
        //Debug.Log(fHealth);
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
            bar.localScale = new Vector3(fHealth, 1f);
            if (fHealth <= 0.0f)
            {
                gameOver.text = "Game Over";
                enemy = GameObject.FindGameObjectsWithTag("enemy");
                Debug.Log(enemy.Length);
                foreach (GameObject enem in enemy)
                {
                    Destroy(enem);
                }
                isgameOver = true;
            }
            //Debug.Log(isgameOver);

        }
    }


    public bool getisgameOver()
    {
        return isgameOver;
    }
}
