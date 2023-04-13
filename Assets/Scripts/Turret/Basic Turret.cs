using UnityEngine;


public class BasicTurret : TurretBase
{
    RaycastHit2D hit;
    Vector3 offSet;


    float range = 15f;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        damage = 2;
        print(GameManager.instance.GetLevel());
        switch (WaveManager.instance.GetLevel())
        {
            case 2: damage += (damage / 2); break;
            case 3: damage += (damage / 2); break;
            case 4: damage += (damage / 2); break;
            case 5: damage += (damage / 2); break;
        }
        print("Basic Turret Damage: " + damage);
        mask = LayerMask.GetMask("enemy");
        visionDistance = 8;
        
        firerateinSeconds = .00005f;
        InvokeRepeating("UpdateTarget", 0f, 0.1f);
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.volume = .6f;
    }

    // Update is called once per frame
    void Update()
    {
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
        hit = Physics2D.Raycast(transform.position, transform.up * visionDistance, visionDistance, mask);
        if (hit)
        {
            if (firerateinSeconds <= 0f)
            {
                Fire();
                firerateinSeconds = .00005f;
            }
            else
            {
                firerateinSeconds -= Time.deltaTime;
                audioSource.Stop();
            }
        }
        else
        {
            audioSource.Stop();
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
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
            switch (target.name)
            {
                case "Basic Enemy(Clone)": target.GetComponent<BasicEnemy>().Health -= damage; break;
                case "Advanced Enemy(Clone)": target.GetComponent<AdvancedEnemy>().Health -= damage; break;
                case "Ultimate Enemy(Clone)": target.GetComponent<UltimateEnemy>().Health -= damage; break;
            }
        }
    }
}
