using UnityEngine;

public class TurretBase : MonoBehaviour
{
    protected int price;
    protected static int damage;
    protected float firerateinSeconds;
    protected float visionDistance;
    protected LayerMask enemyMask, weaponMask;
    protected Color[] colors = {Color.black, Color.blue, Color.red, Color.cyan, Color.grey};
    protected Player player;
    protected AudioSource audioSource;
    protected bool tooClose;
    protected float health;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>();
    }
}
