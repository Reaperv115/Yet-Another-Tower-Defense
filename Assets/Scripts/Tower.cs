using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Tower : MonoBehaviour
{
    
    private float fHealth;
    bool isgameOver;

    public UnityEvent onDeath;

    // Start is called before the first frame update
    void Start()
    {
        fHealth = 1f;
        isgameOver = false;
    }

    public float GetHealth() { return fHealth; }

    public void SetHealth(float health) { fHealth = health; }
    public bool GetIsGameOver() { return isgameOver; }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("enemy"))
        {
            Color c = GetComponent<SpriteRenderer>().color;
            Destroy(collision.gameObject);
            fHealth -= .1f;
            switch (collision.transform.name)
            {
                case "Basic Enemy(Clone)":    ScoreManager.instance.amount -= 1; c.g -= 1; break;
                case "Advanced Enemy(Clone)": ScoreManager.instance.amount -= 2; c.g -= 2; break;
                case "Ultimate Enemy(Clone)": ScoreManager.instance.amount -= 3; c.g -= 3; break;
            }
            
            GetComponent<SpriteRenderer>().color = c;
            if (GetHealth() <= 0f) onDeath.Invoke();

        }
    }

    public void EndGame()
    {
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("enemy");
        for (int i = 0; i < enemy.Length; ++i)
        {
            Destroy(enemy[i]);
        }
        isgameOver = true;
        WaveManager.instance.SetSpawn(false);
        SceneManager.LoadScene("Defeat");
    }


}
