using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tower : MonoBehaviour
{
    
    private float fHealth;

    GameManager gm;

    GameObject[] enemy;
    List<GameObject> enemyList;

    TextMeshProUGUI gameOver;
    GameObject enemystartingPoint;
    bool isgameOver;
    bool allenemsGone;
    bool spawn;

    // Start is called before the first frame update
    void Start()
    {
        fHealth = 1f;
        gameOver = GameObject.Find("game over").GetComponent<TextMeshProUGUI>();
        gm = GameObject.Find("Main Camera").GetComponent<GameManager>();
        enemystartingPoint = GameObject.Find("enemy starting tile");
        isgameOver = false;
        spawn = false;
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
            if (getHealth() <= 0f)
            {
                gm.SetHasBegun(false);
                gameOver.text = "Game Over";
                GameObject[] enemy = GameObject.FindGameObjectsWithTag("enemy");
                for (int i = 0; i < enemy.Length; ++i)
                {
                    Destroy(enemy[i]);
                }
                isgameOver = true;
                enemystartingPoint.GetComponent<SpawnEnemy>().SetSpawn(false);
            }

        }
    }


    public bool getisgameOver()
    {
        return isgameOver;
    }
    public void SetSpawn(bool canSpawn)
    {
        spawn = canSpawn;
    }
    public bool GetCanSpawn()
    {
        return spawn;
    }
}
