using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;
    GameObject basicTurret, advancedTurret, ultimateTurret;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        basicTurret = Resources.Load<GameObject>("Basic Turret");
        advancedTurret = Resources.Load<GameObject>("Advanced Turret");
        ultimateTurret = Resources.Load<GameObject>("Ultimate Turret");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetBasicTurret() { return basicTurret; }
    public GameObject GetAdvancedTurret() { return advancedTurret; }
    public GameObject GetUltimateTurret() { return ultimateTurret; }
}
