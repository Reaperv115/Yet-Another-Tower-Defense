using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    float lackoffundsdisplayTimer = 2f;
    SelectWeapon swRef;
    bool placingWeapon;
    
    private void Awake()
    {
        swRef = GetComponent<SelectWeapon>();
    }
    private void Update()
    {

        
    }

    public float GetDisplayTimer() { return lackoffundsdisplayTimer; }
    public void SetDisplayTimer(float time) { lackoffundsdisplayTimer = time; }
    public void LoadWeapon(GameObject weapon)
    {
        mainWeapon = weapon;
        GameManager.instance.GetWeaponToPlaceDisplay().GetComponent<TextMeshProUGUI>().text = mainWeapon.name;
        placingWeapon = true;
        GameManager.instance.GetWeaponsButton().SetActive(false);
        instantiatedmainWeapon = Instantiate(mainWeapon, swRef.getMWP(), mainWeapon.transform.rotation);
        WeaponManager.instance.TurretPlaced();
        GameManager.instance.GetWeaponsPanel().SetActive(false);
    }
    public GameObject instantiatedmainWeapon { get;  set; }
    public GameObject mainWeapon { get; set; }
    public bool IsPlacingWeapon() { return placingWeapon; }
    
    public void SetIsPlacing(bool isPlacing) { placingWeapon = isPlacing; }
}
