using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    protected int price;
    protected static int damage;
    protected float firerateinSeconds;
    protected float visionDistance;
    protected LayerMask mask;
    protected Color[] colors = {Color.black, Color.blue, Color.red, Color.cyan, Color.grey};
    protected Player player;
    protected AudioSource audioSource;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>();
    }
}
