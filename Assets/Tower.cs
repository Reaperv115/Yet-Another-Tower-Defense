using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    private float fHealth;

    GameObject towerhealthDisplay;
    GameObject[] enemy;

    TextMeshProUGUI gameOver;
    bool isgameOver;

    // Start is called before the first frame update
    void Start()
    {
        fHealth = 5f;
        towerhealthDisplay = GameObject.Find("tower health");
        
        gameOver = GameObject.Find("game over").GetComponent<TextMeshProUGUI>();
        isgameOver = false;
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

    public GameObject gettowerhealthDisplay()
    {
        return towerhealthDisplay;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("enemy"))
        {
            Destroy(collision.gameObject);
            
            fHealth -= 1;
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
            Debug.Log(isgameOver);

        }
    }

    public bool getisgameOver()
    {
        return isgameOver;
    }
}
