using System;
using UnityEngine;

public class UltimateTurret : TurretBase
{
    int enem;
    int turret;
    int mask;

    Vector3 offSet;
    RaycastHit2D hit;

    float range = 15f;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        enem = 1 << LayerMask.NameToLayer("enemy");
        turret = 1 << LayerMask.NameToLayer("weapon");
        mask = enem | turret;
       _damage = 5;
        //switch (WaveManager.instance.GetLevel())
        //{
        //    case 2: _damage += (_damage / 2); break;
        //    case 3: _damage += (_damage / 3); break;
        //    case 4: _damage += (_damage / 4); break;
        //    case 5: _damage += (_damage / 5); break;
        //}
        print("ultimate turret damage: " + _damage);
        visionDistance = 15;
        firerateinSeconds = .0000005f;
        InvokeRepeating("UpdateTarget", 0f, 0.1f);
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        tooClose = false;
        health = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0f)
            Destroy(gameObject);
        // do nothing if no target is close enough
        if (target == null)
        {
            audioSource.Stop();
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            return;
        }
        // making the turret track the enemy when it's close enough
        offSet = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, offSet);
        hit = Physics2D.Raycast(transform.GetChild(0).position, transform.up * visionDistance, visionDistance, mask);
        if (firerateinSeconds <= 0f)
        {
            if (hit)
            {
                Fire();
                firerateinSeconds = .005f;
            }
            else
            {
                gameObject.transform.GetChild(1).gameObject.SetActive(false);
                audioSource.Stop();
            }
        }
        else
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            audioSource.Stop();
            firerateinSeconds -= Time.deltaTime;
        }
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        float shortestDistance = visionDistance;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distancetoEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distancetoEnemy <= shortestDistance)
            {
                shortestDistance = distancetoEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= range) target = nearestEnemy.transform;
        else nearestEnemy = null;
    }

    // checking to see what needs to happen if an enemy is shot
    // depending on which type of enemy it is.
    void Fire()
    {
        if (target)
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            audioSource.Play();
            switch (hit.transform.name)
            {
                case "Basic Enemy(Clone)":     hit.transform.GetComponent<BasicEnemy>().Health -= _damage; break;
                case "Advanced Enemy(Clone)":  hit.transform.GetComponent<AdvancedEnemy>().Health -= _damage; break;
                case "Ultimate Enemy(Clone)":  hit.transform.GetComponent<UltimateEnemy>().Health -= _damage; break;

                case "Basic Turret(Clone)":    hit.transform.GetComponent<BasicTurret>().TakeDamage(hit); break;
                case "Advanced Turret(Clone)": hit.transform.GetComponent<AdvancedTurret>().TakeDamage(hit); break;
                case "Ultimate Turret(Clone)": hit.transform.GetComponent<UltimateTurret>().TakeDamage(hit); break;
            }
        }
    }
    public float GetHealth() { return health; }
    public void SetHealth(float newHealth) { health = newHealth; }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.name.Contains("Turret"))
            GameManager.instance.GetCam().GetComponent<Player>().SetTooClose(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name.Contains("Turret"))
            GameManager.instance.GetCam().GetComponent<Player>().SetTooClose(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.name.Contains("Turret"))
            GameManager.instance.GetCam().GetComponent<Player>().SetTooClose(false);
    }
    public void TakeDamage(RaycastHit2D hit2D) 
    { 
        health -= _damage;
        Color c = hit2D.transform.gameObject.GetComponent<SpriteRenderer>().color;
        c.r += _damage;
        hit2D.transform.gameObject.GetComponent<SpriteRenderer>().color = c; 
    }
}
