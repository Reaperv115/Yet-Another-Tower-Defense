using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class selectWeapon : MonoBehaviour
{
    GameManager gm;

    TextMeshProUGUI weapontoPlace;
    Player player;
    Ray ray;
    RaycastHit2D hit;
    Touch touch;

    Vector3 newworldPoint, oldworldPoint;
    Vector3 mouseWorldPosition;
    bool placingWeapon;

    float rotationAngle = 90;
    float rotationSpeed;
    float timetoplaceWeapon;

    bool checkfundsT1, checkfundsT2, checkfundsT3, activateweaponsPanel;

    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponent<GameManager>();
        weapontoPlace = GameObject.Find("Weapon to Place").GetComponent<TextMeshProUGUI>();
        player = GetComponent<Player>();
        placingWeapon = false;
        rotationSpeed = rotationAngle;
        timetoplaceWeapon = .05f;
        checkfundsT1 = checkfundsT2 = checkfundsT3 = activateweaponsPanel = false;
        gm.GetStartButton().SetActive(false);
        ToggleWeaponAdjusting(false);
        Debug.Log(activateweaponsPanel);
    }

    // Update is called once per frame
    void Update()
    {
        // weapon placement mechanics for android
        if (Application.platform.Equals(RuntimePlatform.Android))
        {
            if (player.IsPlacingWeapon())
            {
                ToggleWeaponAdjusting(true);
                newworldPoint = touch.position;
                newworldPoint.z = Mathf.Abs(Camera.main.transform.position.z);
                mouseWorldPosition = Camera.main.ScreenToWorldPoint(newworldPoint);
                mouseWorldPosition.z = 0f;
                if (Input.touchCount > 0)
                {
                    touch = Input.GetTouch(0);
                    if (touch.phase.Equals(TouchPhase.Stationary))
                    {
                        if (timetoplaceWeapon <= 0f)
                        {
                            player.mainWeapon.transform.position = mouseWorldPosition;
                            ToggleWeaponAdjusting(false);
                            gm.GetStartButton().SetActive(true);
                            player.SetIsPlacing(false);
                            weapontoPlace.text = "";
                            timetoplaceWeapon = 1f;
                            gm.GetRestartButton().SetActive(true);
                        }
                        else
                            timetoplaceWeapon -= Time.deltaTime;
                    }
                    else
                    {
                        timetoplaceWeapon = .05f;
                        oldworldPoint = newworldPoint;
                        newworldPoint = touch.position;
                        if (!newworldPoint.Equals(oldworldPoint))
                        {
                            newworldPoint = touch.position;
                            newworldPoint.z = Mathf.Abs(Camera.main.transform.position.z);
                            mouseWorldPosition = Camera.main.ScreenToWorldPoint(newworldPoint);
                            mouseWorldPosition.z = 0f;
                            Destroy(player.instantiatedmainWeapon);
                            player.instantiatedmainWeapon = Instantiate(player.mainWeapon, mouseWorldPosition, player.mainWeapon.transform.rotation);
                        }
                    }
                }


            }
        }
        // weapon placement mechanics for pc
        else
        {
            newworldPoint = Input.mousePosition;
            newworldPoint.z = Mathf.Abs(Camera.main.transform.position.z);
            mouseWorldPosition = Camera.main.ScreenToWorldPoint(newworldPoint);
            mouseWorldPosition.z = 0f;

            if (player.IsPlacingWeapon())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    player.mainWeapon.transform.position = mouseWorldPosition;
                    ToggleWeaponAdjusting(false);
                    gm.GetStartButton().SetActive(true);
                    player.SetIsPlacing(false);
                    weapontoPlace.text = "";
                }
                else
                {
                    timetoplaceWeapon = 1.0f;
                    oldworldPoint = newworldPoint;
                    newworldPoint = Input.mousePosition;
                    if (!newworldPoint.Equals(oldworldPoint))
                    {
                        newworldPoint = Input.mousePosition;
                        newworldPoint.z = Mathf.Abs(Camera.main.transform.position.z);
                        mouseWorldPosition = Camera.main.ScreenToWorldPoint(newworldPoint);
                        mouseWorldPosition.z = 0f;
                        Destroy(player.instantiatedmainWeapon);
                        player.instantiatedmainWeapon = Instantiate(player.mainWeapon, mouseWorldPosition, player.mainWeapon.transform.rotation);
                    }
                }
            }
        }
    }

    // used to hide and show weapons selection
    // panel when you hit the weapons button
    public void onClick()
    {
        activateweaponsPanel = !activateweaponsPanel;
        Debug.Log(activateweaponsPanel);
        gm.GetWeaponsPanel().SetActive(activateweaponsPanel);
    }

    // selects the tier 1 turret
    public void GetTurret()
    {
        checkfundsT1 = true;
        player.SetDisplayTimer(2f);
        activateweaponsPanel = !activateweaponsPanel;
        gm.GetRestartButton().SetActive(false);
    }

    // selects the tier 2 turret
    public void GetTurretT2()
    {
        checkfundsT2 = true;
        player.SetDisplayTimer(2f);
        activateweaponsPanel = !activateweaponsPanel;
        gm.GetRestartButton().SetActive(false);
    }

    // selects the tier 3 turret
    public void GetTurretT3()
    {
        checkfundsT3 = true;
        player.SetDisplayTimer(2f);
        activateweaponsPanel = !activateweaponsPanel;
        gm.GetRestartButton().SetActive(false);
    }

    

    public void isselectingWeapon(bool isPlacing)
    {
        placingWeapon = isPlacing;
    }

    public void ToggleWeaponAdjusting(bool isAdjusting)
    {
        gm.GetRotate90().SetActive(isAdjusting);
        gm.GetRotateNeg90().SetActive(isAdjusting);
        gm.GetWeaponsButton().SetActive(!isAdjusting);
    }

    public Vector3 GetWeaponPosition()
    {
        return mouseWorldPosition;
    }

    public Touch GetTouch()
    {
        return touch;
    }

    public bool checkT1Funds()
    {
        return checkfundsT1;
    }
    public void setT1Check(bool check)
    {
        checkfundsT1 = check;
    }
    public bool checkT2Funds()
    {
        return checkfundsT2;
    }
    public void setT2Check(bool check)
    {
        checkfundsT2 = check;
    }
    public bool checkT3Funds()
    {
        return checkfundsT3;
    }
    public void setT3Check(bool check)
    {
        checkfundsT3 = check;
    }
    public Vector3 getMWP()
    {
        return mouseWorldPosition;
    }
}
