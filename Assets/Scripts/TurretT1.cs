using UnityEngine;

public class TurretT1 : WeaponBase
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
        visionDistance = 15;
        damage = 2;
        price = 3;
        firerateinSeconds = .5f;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {   // do nothing if no target is close enough
        if (target == null) return;

        // making the turret track the enemy when it's close enough
        offSet = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, offSet);
        hit = Physics2D.Raycast(transform.position, transform.up * 10, visionDistance, mask);
        if (hit) Invoke("Fire", firerateinSeconds);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        float shortestDistance = 15;
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
        // literally no idea why this if-check is required all of a sudden
        // game still functions properly without it,
        // it just throws a bunch of errors in Unity though
        if (target)
        {
            switch (target.name)
            {
                case "enemy car (Tier 1)(Clone)":
                    {
                        if (target.GetComponent<Enemy1>().Health <= 0)
                        {
                            UpdateScore(target);
                            Destroy(target.gameObject);
                        }
                        else target.GetComponent<Enemy1>().Health -= damage;
                        break;
                    }
                case "enemy car (Tier 2)(Clone)":
                    {
                        if (target.GetComponent<Enemy2>().Health <= 0)
                        {
                            UpdateScore(target);
                            Destroy(target.gameObject);
                        }
                        else target.GetComponent<Enemy2>().Health -= damage;
                        break;
                    }
                case "enemy car (Tier 3)(Clone)":
                    {
                        if (target.GetComponent<Enemy3>().Health <= 0)
                        {
                            UpdateScore(target);
                            Destroy(target.gameObject);
                        }
                        else target.GetComponent<Enemy3>().Health -= damage;
                        break;
                    }
                default:
                    break;
            }
        }
    }

    // updating the score if an enemy is killed
    // depending on which type of enemy is killed
    void UpdateScore(Transform enem)
    {
        if (enem.name.Equals("enemy car (Tier 1)(Clone)")) gm.SetScore(gm.GetScore() + (gm.GetT1PriceF() / 2));
        if (enem.name.Equals("enemy car (Tier 2)(Clone)")) gm.SetScore(gm.GetScore() + (gm.GetT2PriceF() / 2));
        if (enem.name.Equals("enemy car (Tier 3)(Clone)")) gm.SetScore(gm.GetScore() + (gm.GetT3PriceF() / 2));
    }

}
