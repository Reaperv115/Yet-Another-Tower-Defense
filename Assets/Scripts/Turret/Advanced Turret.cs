using UnityEngine;
using UnityEngine.UI;

public class AdvancedTurret : TurretBase
{
    RaycastHit2D hit;
    Vector3 offSet;


    float range = 15f;
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
        _damage = 5;
        //switch (WaveManager.instance.GetLevel())
        //{
        //    case 2: _damage += (_damage / 2); break;
        //    case 3: _damage += (_damage / 3); break;
        //    case 4: _damage += (_damage / 4); break;
        //    case 5: _damage += (_damage / 5); break;
        //}
        print("advanced turret damage: " + _damage);
        visionDistance = 10;
        firerateinSeconds = .000005f;
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
                audioSource.Stop();
                gameObject.transform.GetChild(1).gameObject.SetActive(false);
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
            switch (hit.transform.name)
            {
                case "Basic Enemy(Clone)":    hit.transform.GetComponent<BasicEnemy>().Health -= _damage; break;
                case "Advanced Enemy(Clone)": hit.transform.GetComponent<AdvancedEnemy>().Health -= _damage; break;
                case "Ultimate Enemy(Clone)": hit.transform.GetComponent<UltimateEnemy>().Health -= _damage; break;

                case "Basic Turret(Clone)":   hit.transform.GetComponent<BasicTurret>().TakeDamage(); break;
                case "Advanced Turret(Clone)":hit.transform.GetComponent<AdvancedTurret>().TakeDamage(); break;
                case "Ultimate Turret(Clone)":hit.transform.GetComponent<UltimateTurret>().TakeDamage(); break;
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
    public float GetHealth() { return health; }
    public void SetHealth(float newHealth) { health = newHealth; }
    public void TakeDamage() { SetHealth(health -= _damage); }
    //public void TakeDamage() 
    //{ 
    //    Destroy(gameObject);
    //}
}
