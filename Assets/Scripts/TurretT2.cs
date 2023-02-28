using Unity.VisualScripting;
using UnityEngine;

public class TurretT2 : WeaponBase
{
    RaycastHit2D hit;
    Vector3 offSet;
    Vector3 dir;


    float range = 15f;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        mask = LayerMask.GetMask("enemy");
        visionDistance = 10;
        damage = 25;
        firerateinSeconds = .5f;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.volume = .6f;
    }

    // Update is called once per frame
    void Update()
    {
        // do nothing if no target is close enough
        if(target == null) return;

        // making the turret track the enemy when it's close enough
        offSet = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, offSet);
        hit = Physics2D.Raycast(transform.position, transform.up * visionDistance, visionDistance, mask);
        if (hit)
        {
            if (firerateinSeconds <= 0f)
            {
                Fire();
                firerateinSeconds = .5f;
            }
            else
            {
                audioSource.Stop();
                gameObject.transform.GetChild(1).gameObject.SetActive(false);
                firerateinSeconds -= Time.deltaTime;
            }
        }
        else
        {
            audioSource.Stop();
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            firerateinSeconds -= Time.deltaTime;
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
                case "enemy car (Tier 1)(Clone)":
                    {
                        
                        target.GetComponent<Enemy1>().Health -= damage;
                        break;
                    }
                case "enemy car (Tier 2)(Clone)":
                    {
                        target.GetComponent<Enemy2>().Health -= damage;
                        break;
                    }
                case "enemy car (Tier 3)(Clone)":
                    {
                        target.GetComponent<Enemy3>().Health -= damage;
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
