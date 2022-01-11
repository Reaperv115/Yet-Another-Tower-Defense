using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int score = 0;
    TextMeshProUGUI scoreBoard;
    [SerializeField] TextMeshProUGUI lackoffundsDisplay;
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
        Debug.Log(score);
    }
    private void Update()
    {
        scoreBoard.text = "Score: " + score.ToString();
        if (swRef.checkT1Funds())
        {
            if (score >= 1)
            {
                mainWeapon = Resources.Load<GameObject>("turret");
                placingWeapon = true;
                weapontoPlace.text = "turret";
                swRef.ToggleWeaponAdjusting(true);
                instantiatedWeapon = Instantiate(mainWeapon, swRef.getMWP(), mainWeapon.transform.rotation);
                swRef.getweaponsPanel().gameObject.SetActive(false);
                score -= 1;
                swRef.setT1Check(false);
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
                    Debug.Log(lackoffundsdisplayTimer);
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
}
