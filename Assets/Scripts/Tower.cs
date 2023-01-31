using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tower : MonoBehaviour
{
    
    private float fHealth;

    TextMeshProUGUI gameOver;
    bool isgameOver;

    // Start is called before the first frame update
    void Start()
    {
        fHealth = 1f;
        gameOver = GameObject.Find("game over").GetComponent<TextMeshProUGUI>();
        isgameOver = false;
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
            GameManager.instance.GetHealthBar().localScale = new Vector3(fHealth, 1f);
            if (getHealth() <= 0f)
            {
                gameOver.text = "Game Over";
                GameObject[] enemy = GameObject.FindGameObjectsWithTag("enemy");
                for (int i = 0; i < enemy.Length; ++i)
                {
                    Destroy(enemy[i]);
                }
                isgameOver = true;
                WaveManager.instance.SetSpawn(false);
            }

        }
    }


    public bool getisgameOver()
    {
        return isgameOver;
    }
}
