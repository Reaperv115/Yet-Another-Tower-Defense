using UnityEngine;

public class TurretT3 : WeaponBase
{
    GameManager gm;

    Vector3 offSet;
    RaycastHit2D hit;

    float range = 15f;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("Main Camera").GetComponent<GameManager>();
        mask = LayerMask.GetMask("enemy");
        visionDistance = 10;
        damage = 75;
        price = 9;
        firerateinSeconds = 4f;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {   // do nothing if no target is close enough
        if (target == null) return;

        // making the turret track the enemy when it's close enough
        offSet = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, offSet);
        hit = Physics2D.Raycast(transform.position, transform.up * visionDistance, visionDistance, mask);
        if (hit)
        {
            if (firerateinSeconds <= 0f)
            {
                Fire();
                firerateinSeconds = 4f;
            }
            else
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
    // depeneding on which type of enemy it is.
    void Fire()
    {
        int tmpCol = Random.Range(0, colors.Length);
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = colors[tmpCol];
        if (target)
        {
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
