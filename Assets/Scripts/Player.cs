using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int score = 3;
    TextMeshProUGUI scoreBoard;
    TextMeshProUGUI roundDisplay;
    TextMeshProUGUI lackoffundsDisplay;
    float lackoffundsdisplayTimer = 2f;
    selectWeapon swRef;
    SpawnEnemy seRef;
    bool placingWeapon;
    TextMeshProUGUI weapontoPlace;
    private void Awake()
    {
        scoreBoard = GameObject.FindGameObjectWithTag("scoreboard").GetComponent<TextMeshProUGUI>();
        Debug.Log("scoreboard" + scoreBoard);
        swRef = GetComponent<selectWeapon>();
        roundDisplay = GameObject.Find("Round").GetComponent<TextMeshProUGUI>();
        seRef = GameObject.Find("enemy starting tile").transform.GetChild(0).GetComponent<SpawnEnemy>();
        weapontoPlace = GameObject.Find("Weapon to Place").GetComponent<TextMeshProUGUI>();
        lackoffundsDisplay = GameObject.Find("LackofFunds").GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        scoreBoard.text = "Score: " + score.ToString();
        roundDisplay.text = "Round: " + seRef.GetCurrentRound().ToString();
        if (swRef.checkT1Funds())
        {
            if (score >= 1)
            {
                LoadWeapon("turret (Tier 1)");
                swRef.setT1Check(false);
            }
            else
            {
                if (lackoffundsdisplayTimer <= 0f)
                {
                    lackoffundsDisplay.text = "";
                    swRef.setT1Check(false);
                }
                else
                {
                    swRef.getweaponsPanel().gameObject.SetActive(false);
                    lackoffundsDisplay.text = "NOT ENOUGH MONEY";
                    lackoffundsdisplayTimer -= .05f;
                }
            }
        }
        if (swRef.checkT2Funds())
        {
            if (score >= 2)
            {
                LoadWeapon("turret (Tier 2)");
                swRef.setT2Check(false);
            }
            else
            {
                if (lackoffundsdisplayTimer <= 0f)
                {
                    lackoffundsDisplay.text = "";
                    swRef.setT2Check(false);
                }
                else
                {
                    swRef.getweaponsPanel().gameObject.SetActive(false);
                    lackoffundsDisplay.text = "NOT ENOUGH MONEY";
                    lackoffundsdisplayTimer -= .05f;
                }
            }
        }
        if (swRef.checkT3Funds())
        {
            if (score >= 3)
            {
                LoadWeapon("turret (Tier 3)");
                swRef.setT3Check(false);
            }
            else
            {
                if (lackoffundsdisplayTimer <= 0f)
                {
                    lackoffundsDisplay.text = "";
                    swRef.setT3Check(false);
                }
                else
                {
                    swRef.getweaponsPanel().gameObject.SetActive(false);
                    lackoffundsDisplay.text = "NOT ENOUGH MONEY";
                    lackoffundsdisplayTimer -= .05f;
                }
            }
        }
    }
    public int GetScore()
    {
        return score;
    }
    public void SetScore(int nScore)
    {
        score = nScore;
    }

    public float GetDisplayTimer()
    {
        return lackoffundsdisplayTimer;
    }
    public void SetDisplayTimer(float time)
    {
        lackoffundsdisplayTimer = time;
    }
    void LoadWeapon(string weapon)
    {
        mainWeapon = Resources.Load<GameObject>(weapon);
        placingWeapon = true;
        weapontoPlace.text = weapon;
        swRef.ToggleWeaponAdjusting(true);
        instantiatedmainWeapon = Instantiate(mainWeapon, swRef.getMWP(), mainWeapon.transform.rotation);
        swRef.getweaponsPanel().gameObject.SetActive(false);
        switch (mainWeapon.transform.tag)
        {
            case "WT1":
                {
                    score -= 1;
                    // optional todo: why isnt this working
                    //score -= mainWeapon.GetComponent<TurretT1>().GetPrice();
                    break;
                }
            case "WT2":
                {
                    score -= 2;
                    // which means this one
                    //score -= instantiatedmainWeapon.GetComponent<TurretT2>().GetPrice();
                    break;
                }
            case "WT3":
                {
                    score -= 3;
                    // and this one are't working
                    //score -= instantiatedmainWeapon.GetComponent<TurretT3>().GetPrice();
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
