using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    protected int pathIndex;
    protected int pathindexPoint;

    protected bool attackTower;


    protected float speed;
        

    public int Health { get; set; }

    public virtual void Go(float _speed)
    {

    }
}
