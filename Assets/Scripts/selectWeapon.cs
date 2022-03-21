using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class selectWeapon : MonoBehaviour
{
    TextMeshProUGUI weapontoPlace;
    [SerializeField]
    TextMeshProUGUI t1Price, t2Price, t3Price;
    GameObject weaponsPanel;
    GameObject mainWeapon, instantiatedWeapon;
    [SerializeField]
    GameObject startButton, rotate90, rotateneg90, weaponsButton;
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

    bool checkfundsT1, checkfundsT2, checkfundsT3;

    // Start is called before the first frame update
    void Start()
    {
        weaponsPanel = GameObject.Find("Weapons Panel");
        weaponsPanel.gameObject.SetActive(false);
        weapontoPlace = GameObject.Find("Weapon to Place").GetComponent<TextMeshProUGUI>();
        player = GameObject.Find("Main Camera").GetComponent<Player>();
        placingWeapon = false;
        rotationSpeed = rotationAngle;
        timetoplaceWeapon = .05f;
        checkfundsT1 = checkfundsT2 = checkfundsT3 = false;
        startButton.SetActive(false);
        ToggleWeaponAdjusting(false);
    }

    // Update is called once per frame
    void Update()
    {
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
                            startButton.SetActive(true);
                            player.SetIsPlacing(false);
                            weapontoPlace.text = "";
                            timetoplaceWeapon = .05f;
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
                    startButton.SetActive(true);
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

    public void onClick()
    {
        timetoplaceWeapon = 1.0f;
        weaponsPanel.gameObject.SetActive(true);
    }

    public void GetTurret()
    {
        checkfundsT1 = true;
        player.SetDisplayTimer(2f);
    }
    public void GetTurretT2()
    {
        checkfundsT2 = true;
        player.SetDisplayTimer(2f);
    }
    public void GetTurretT3()
    {
        checkfundsT3 = true;
        player.SetDisplayTimer(2f);
    }

    

    public void isselectingWeapon(bool isPlacing)
    {
        placingWeapon = isPlacing;
    }

    public void ToggleWeaponAdjusting(bool isAdjusting)
    {
        rotate90.SetActive(isAdjusting);
        rotateneg90.SetActive(isAdjusting);
        weaponsButton.SetActive(!isAdjusting);
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
    public GameObject getweaponsPanel()
    {
        return weaponsPanel;
    }
}
