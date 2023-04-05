using UnityEngine;

public class UltimateTurret : TurretBase
{

    Vector3 offSet;
    RaycastHit2D hit;

    float range = 15f;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        mask = LayerMask.GetMask("enemy");
        visionDistance = 15;
        damage = 75;
        firerateinSeconds = 1f;
        InvokeRepeating("UpdateTarget", 0f, 0.1f);
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {   // do nothing if no target is close enough
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
                firerateinSeconds = 1f;
            }
            else
            {
                gameObject.transform.GetChild(1).gameObject.SetActive(false);
                audioSource.Stop();
                firerateinSeconds -= Time.deltaTime;
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
        }
    }

}
