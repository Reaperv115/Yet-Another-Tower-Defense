using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Tower : MonoBehaviour
{
    
    private float fHealth;

    TextMeshProUGUI gameOver;
    bool isgameOver;

    public UnityEvent onDeath;

    // Start is called before the first frame update
    void Start()
    {
        fHealth = 1f;
        gameOver = GameObject.Find("game over").GetComponent<TextMeshProUGUI>();
        isgameOver = false;
    }

    public float GetHealth()
    {
        return fHealth;
    }

    public void SetHealth(float health)
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
            if (GetHealth() <= 0f)
            {
                onDeath.Invoke();
            }

        }
    }

    public void EndGame()
    {
        gameOver.text = "Game Over";
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("enemy");
        for (int i = 0; i < enemy.Length; ++i)
        {
            Destroy(enemy[i]);
        }
        isgameOver = true;
        WaveManager.instance.SetSpawn(false);
        SceneManager.LoadScene("Defeat");
    }


    public bool GetIsGameOver()
    {
        return isgameOver;
    }
}
