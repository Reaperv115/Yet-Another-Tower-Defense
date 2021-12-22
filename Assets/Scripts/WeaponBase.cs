using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    protected float firerateinSeconds;
    protected int price;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //virtual void Fire()

    public void SetFireRate(float firerate)
    {
        firerateinSeconds = firerate;
    }
    public float GetFireRate()
    {
        return firerateinSeconds;
    }
}
