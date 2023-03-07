using UnityEngine;
using System.Collections.Generic;

public class TurretT1 : WeaponBase
{

    Vector3 offSet;
    RaycastHit2D hit;

    float range = 15f;
    Transform target;
    GameObject muzzleFlash;
    Transform bulletSpawn;
    // Start is called before the first frame update
    void Start()
    {
        mask = LayerMask.GetMask("enemy");
        visionDistance = 9;
        damage = 2;
        firerateinSeconds = .0025f;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.volume = .3f;
        audioSource.pitch = .25f;
    }

    // Update is called once per frame
    void Update()
    {
        // do nothing if no target is close enough
        if (target == null)
        {
            if (audioSource.isPlaying) audioSource.Stop();
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            return;
        }

        // making the turret track the enemy when it's close enough
        offSet = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, offSet);
        hit = Physics2D.Raycast(transform.position, transform.up * visionDistance, visionDistance, mask);
        if (hit)
            Fire();
        else
        {
            if (audioSource.isPlaying) audioSource.Stop();
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
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
        // literally no idea why this if-check is required all of a sudden
        // game still functions properly without it,
        // it just throws a bunch of errors in Unity though
        if (target)
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            audioSource.Play();
            switch (target.transform.name)
            {
                case "enemy car (Tier 1)(Clone)": target.GetComponent<Enemy1>().Health -= damage;
                    break;
                case "enemy car (Tier 2)(Clone)": target.GetComponent<Enemy2>().Health -= damage;
                    break;
                case "enemy car (Tier 3)(Clone)": target.GetComponent<Enemy3>().Health -= damage;
                    break;
                default:
                    break;
            }
            
        }

    }

}
