using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI victoryDisplay;
    private float fHealth;

    GameObject[] enemy;
    List<GameObject> enemyList;
    RectTransform bar;
    [SerializeField]
    GameObject seRef;

    TextMeshProUGUI gameOver;
    bool isgameOver;
    bool allenemsGone;

    // Start is called before the first frame update
    void Start()
    {
        fHealth = 1f;
        bar = GameObject.Find("HealthBar").transform.GetChild(1).GetComponent<RectTransform>();
        gameOver = GameObject.Find("game over").GetComponent<TextMeshProUGUI>();
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
        if (collision.transform.tag.Contains("E"))
        {

            Destroy(collision.gameObject);
            if (seRef.GetComponent<SpawnEnemy>().GetNumEnemies() <= 0)
            {
                enemy = GameObject.FindGameObjectsWithTag("ET1");
                Debug.Log(enemy.Length);
                if (enemy.Length.Equals(1))
                {
                    victoryDisplay.text = "YOU WIN!";
                }
            }
            fHealth -= .1f;
            bar.localScale = new Vector3(fHealth, 1f);
            if (fHealth <= 0.0f)
            {
                gameOver.text = "Game Over";
                enemy = GameObject.FindGameObjectsWithTag("ET1");
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
