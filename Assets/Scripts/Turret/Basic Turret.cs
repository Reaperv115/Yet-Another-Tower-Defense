using UnityEngine;


public class BasicTurret : TurretBase
{
    RaycastHit2D hit;
    Vector3 offSet;


    float range = 20f;
    Transform target;

    int enem;
    int turret;
    int mask;
    // Start is called before the first frame update
    void Start()
    {
        enem = 1 << LayerMask.NameToLayer("enemy");
        turret = 1 << LayerMask.NameToLayer("weapon");
        mask = enem | turret;
        damage = 2;
        switch (WaveManager.instance.GetLevel())
        {
            case 2: damage += (damage / 2); break;
            case 3: damage += (damage / 3); break;
            case 4: damage += (damage / 4); break;
            case 5: damage += (damage / 5); break;
        }
        enemyMask = LayerMask.GetMask("enemy");
        weaponMask = LayerMask.GetMask("weapon");
        visionDistance = 8;

        firerateinSeconds = .00005f;
        InvokeRepeating("UpdateTarget", 0f, 0.1f);
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.volume = .6f;
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
        hit = Physics2D.Raycast(transform.position, transform.up * visionDistance, visionDistance, enemyMask);
        if (firerateinSeconds <= 0f)
        {
            if (hit)
            {
                Fire();
                firerateinSeconds = .00005f;
            }
            else
            {
                gameObject.transform.GetChild(1).gameObject.SetActive(false);
                audioSource.Stop();
            }
        }
        else
        {
            firerateinSeconds -= Time.deltaTime;
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            audioSource.Stop();
        }
    }

    // check for any targets within range
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
    void Fire()
    {
        if (target)
        {
            audioSource.Play();
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            switch (hit.transform.name)
            {
                case "Basic Enemy(Clone)": hit.transform.GetComponent<BasicEnemy>().Health -= damage; break;
                case "Advanced Enemy(Clone)": hit.transform.GetComponent<AdvancedEnemy>().Health -= damage; break;
                case "Ultimate Enemy(Clone)": hit.transform.GetComponent<UltimateEnemy>().Health -= damage; break;
            }
        }
    }

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
}
