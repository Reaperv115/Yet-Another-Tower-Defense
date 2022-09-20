using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameManager gm;
    
    TextMeshProUGUI lackoffundsDisplay;
    float lackoffundsdisplayTimer = 2f;
    selectWeapon swRef;
    bool placingWeapon;
    
    private void Awake()
    {
        gm = GetComponent<GameManager>();
        swRef = GetComponent<selectWeapon>();
        lackoffundsDisplay = GameObject.Find("LackofFunds").GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {

        // checking to see if the player
        // has enough money to buy tier 1 weapon
        if (swRef.checkT1Funds())
        {

            // if you have enough then make the purchase
            if (gm.GetScore() >= gm.GetT1Price())
            {
                LoadWeapon("turret (Tier 1)");
                swRef.setT1Check(false);
            }
            else
            {
                // if not then tell them
                if (lackoffundsdisplayTimer <= 0f)
                {
                    lackoffundsDisplay.text = "";
                    swRef.setT1Check(false);
                }
                else
                {
                    gm.GetWeaponsPanel().SetActive(false);
                    lackoffundsDisplay.text = "NOT ENOUGH MONEY";
                    lackoffundsdisplayTimer -= .05f;
                }
            }
        }

        // checking to see if the player
        //has enough money to buy tier 2 weapon
        if (swRef.checkT2Funds())
        {

            // if you have enough then make the purchase
            if (gm.GetScore() >= gm.GetT2Price())
            {
                LoadWeapon("turret (Tier 2)");
                swRef.setT2Check(false);
            }
            else
            {

                // if not then tell them
                if (lackoffundsdisplayTimer <= 0f)
                {
                    lackoffundsDisplay.text = "";
                    swRef.setT2Check(false);
                }
                else
                {
                    gm.GetWeaponsPanel().SetActive(false);
                    lackoffundsDisplay.text = "NOT ENOUGH MONEY";
                    lackoffundsdisplayTimer -= .05f;
                }
            }
        }

        // checking to see if the player
        //has enough money to buy tier 3 weapon
        if (swRef.checkT3Funds())
        {
            // if you have enough then make the purchase
            if (gm.GetScore() >= gm.GetT3Price())
            {
                LoadWeapon("turret (Tier 3)");
                swRef.setT3Check(false);
            }
            else
            {
                // if not then tell them
                if (lackoffundsdisplayTimer <= 0f)
                {
                    lackoffundsDisplay.text = "";
                    swRef.setT3Check(false);
                }
                else
                {
                    gm.GetWeaponsPanel().SetActive(false);
                    lackoffundsDisplay.text = "NOT ENOUGH MONEY";
                    lackoffundsdisplayTimer -= .05f;
                }
            }
        }
    }

    public float GetDisplayTimer() { return lackoffundsdisplayTimer; }
    public void SetDisplayTimer(float time) { lackoffundsdisplayTimer = time; }
    void LoadWeapon(string weapon)
    {
        mainWeapon = Resources.Load<GameObject>(weapon);
        gm.GetWeaponToPlaceDisplay().GetComponent<TextMeshProUGUI>().text = mainWeapon.name;
        placingWeapon = true;
        gm.GetWeaponsButton().SetActive(false);
        instantiatedmainWeapon = Instantiate(mainWeapon, swRef.getMWP(), mainWeapon.transform.rotation);
        gm.GetWeaponsPanel().SetActive(false);
        switch (mainWeapon.transform.tag)
        {
            case "WT1":
                {
                    gm.SetScore(gm.GetScore() - gm.GetT1Price());
                    break;
                }
            case "WT2":
                {
                    gm.SetScore(gm.GetScore() - gm.GetT2Price());
                    break;
                }
            case "WT3":
                {
                    gm.SetScore(gm.GetScore() - gm.GetT3Price());
                    break;
                }
            default:
                break;
        }
        
        
        
    }
    public GameObject instantiatedmainWeapon { get;  set; }
    public GameObject mainWeapon { get; set; }
    public bool IsPlacingWeapon()
    {
        return placingWeapon;
    }
    public void SetIsPlacing(bool isPlacing)
    {
        placingWeapon = isPlacing;
    }
}
