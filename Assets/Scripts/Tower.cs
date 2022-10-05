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
    GameObject enemystartingPoint;
    bool isgameOver;
    bool allenemsGone;
    bool spawn;

    // Start is called before the first frame update
    void Awake()
    {
        fHealth = 1f;
        //Debug.Log(fHealth);
    }
    void Start()
    {
        //fHealth = 1f;
        gameOver = GameObject.Find("game over").GetComponent<TextMeshProUGUI>();
        gm = GameObject.Find("Main Camera").GetComponent<GameManager>();
        enemystartingPoint = GameObject.Find("enemy starting tile");
        isgameOver = false;
        spawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(fHealth);
        //gm.GetHealthBar().localScale = new Vector3(fHealth, 1f);
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
            //gm.GetEnemies().Remove(collision.gameObject);
            Destroy(collision.gameObject);
            fHealth -= .1f;
            Debug.Log(fHealth);
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
