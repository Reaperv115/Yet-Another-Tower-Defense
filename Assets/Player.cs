using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int score = 10;
    TextMeshProUGUI scoreBoard;
    TextMeshProUGUI lackoffundsDisplay;
    float lackoffundsdisplayTimer = 2f;
    selectWeapon swRef;
    GameObject mainWeapon, instantiatedWeapon;
    bool placingWeapon;
    TextMeshProUGUI weapontoPlace;
    private void Awake()
    {
        scoreBoard = GameObject.FindGameObjectWithTag("scoreboard").GetComponent<TextMeshProUGUI>();
        swRef = GetComponent<selectWeapon>();
        weapontoPlace = GameObject.Find("Weapon to Place").GetComponent<TextMeshProUGUI>();
        lackoffundsDisplay = GameObject.Find("LackofFunds").GetComponent<TextMeshProUGUI>();
        //Debug.Log(score);
    }
    private void Update()
    {
        scoreBoard.text = "Score: " + score.ToString();
        if (swRef.checkT1Funds())
        {
            if (score >= 1)
            {
                LoadWeapon("turret (Tier 1)");
            }
            else
            {
                //lackoffundsdisplayTimer = 2f;
                if (lackoffundsdisplayTimer <= 0f)
                {
                    lackoffundsDisplay.text = "";
                }
                else
                {
                    lackoffundsDisplay.text = "NOT ENOUGH MONEY";
                    lackoffundsdisplayTimer -= .05f;
                    Debug.Log("T1 no funds " + lackoffundsdisplayTimer);
                }
            }
        }
        if (swRef.checkT2Funds())
        {
            if (score >= 2)
            {
                LoadWeapon("turret (Tier 2)");
            }
            else
            {
                //lackoffundsdisplayTimer = 2f;
                if (lackoffundsdisplayTimer <= 0f)
                {
                    lackoffundsDisplay.text = "";
                }
                else
                {
                    lackoffundsDisplay.text = "NOT ENOUGH MONEY";
                    lackoffundsdisplayTimer -= .05f;
                    Debug.Log("T2 no funds " + lackoffundsdisplayTimer);
                }
            }
        }
        if (swRef.checkT3Funds())
        {
            if (score >= 3)
            {
                LoadWeapon("turret (Tier 3)");
            }
            else
            {
                //lackoffundsdisplayTimer = 2f;
                if (lackoffundsdisplayTimer <= 0f)
                {
                    lackoffundsDisplay.text = "";
                }
                else
                {
                    lackoffundsDisplay.text = "NOT ENOUGH MONEY";
                    lackoffundsdisplayTimer -= .05f;
                    Debug.Log("T3 no funds " + lackoffundsdisplayTimer);
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
        instantiatedWeapon = Instantiate(mainWeapon, swRef.getMWP(), mainWeapon.transform.rotation);
        swRef.getweaponsPanel().gameObject.SetActive(false);
        score -= 1;
        swRef.setT1Check(false);
    }

    public GameObject GetMainWeapon()
    {
        return instantiatedWeapon;
    }
}
