using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;
    GameObject basicTurret, advancedTurret, ultimateTurret;
    [SerializeField]
    GameObject availableturretsCounter;
    GameObject[] numofturretsinplay;

    float numturretstoPlace;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        basicTurret = Resources.Load<GameObject>("Basic Turret");
        advancedTurret = Resources.Load<GameObject>("Advanced Turret");
        ultimateTurret = Resources.Load<GameObject>("Ultimate Turret");
        numturretstoPlace = 2;
    }

    // Update is called once per frame
    void Update()
    {
        availableturretsCounter.GetComponent<TextMeshProUGUI>().text =  "Available Turrets: " + numturretstoPlace.ToString();
    }

    public GameObject GetBasicTurret() { return basicTurret; }
    public GameObject GetAdvancedTurret() { return advancedTurret; }
    public GameObject GetUltimateTurret() { return ultimateTurret; }
    public float GetNumTurretsToPlace() { return numturretstoPlace; }
    public void TurretPlaced() { numturretstoPlace -= 1; }
    public void SetNumTurretsToPlace() { numturretstoPlace = (WaveManager.instance.GetRound() * 2) - WaveManager.instance.GetNumEnemiesToSpawn(); }
}
