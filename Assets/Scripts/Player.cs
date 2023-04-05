using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    TextMeshProUGUI lackoffundsDisplay;
    float lackoffundsdisplayTimer = 2f;
    SelectWeapon swRef;
    bool placingWeapon;
    
    private void Awake()
    {
        swRef = GetComponent<SelectWeapon>();
        lackoffundsDisplay = GameObject.Find("LackofFunds").GetComponent<TextMeshProUGUI>();
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
        Debug.Log(instantiatedmainWeapon);
        GameManager.instance.GetWeaponsPanel().SetActive(false);
        if (mainWeapon.transform.name.Contains("Basic")) GameManager.instance.SetScore(ScoreManager.instance.amount - GameManager.instance.GetT1Price());
        if (mainWeapon.transform.name.Contains("Advanced")) GameManager.instance.SetScore(ScoreManager.instance.amount - GameManager.instance.GetT2Price());
        if (mainWeapon.transform.name.Contains("Ultimate")) GameManager.instance.SetScore(ScoreManager.instance.amount - GameManager.instance.GetT3Price());
    }
    public GameObject instantiatedmainWeapon { get;  set; }
    public GameObject mainWeapon { get; set; }
    public bool IsPlacingWeapon() { return placingWeapon; }
    
    public void SetIsPlacing(bool isPlacing) { placingWeapon = isPlacing; }
}
