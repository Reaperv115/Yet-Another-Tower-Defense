using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class selectWeapon : MonoBehaviour
{
    TextMeshProUGUI weapontoPlace, placingweaponTimer;
    GameObject weaponsPanel;
    GameObject mainWeapon, instantiatedWeapon;
    [SerializeField]
    GameObject startButton, rotate90, rotateneg90;
    Ray ray;
    RaycastHit hit;
    Touch touch;

    Vector3 newworldPoint, oldworldPoint;
    Vector3 mouseWorldPosition;
    bool placingWeapon;

    float rotationAngle = 90;
    float rotationSpeed;
    float timetoplaceWeapon;
    int id;

    // Start is called before the first frame update
    void Start()
    {
        weaponsPanel = GameObject.Find("Weapons Panel");
        weaponsPanel.gameObject.SetActive(false);
        weapontoPlace = GameObject.Find("Weapon to Place").GetComponent<TextMeshProUGUI>();
        placingweaponTimer = GameObject.Find("Place Weapon Timer").GetComponent<TextMeshProUGUI>();
        placingWeapon = false;
        rotationSpeed = rotationAngle;
        timetoplaceWeapon = .05f;
        startButton.SetActive(false);
        ToggleWeaponAdjusting(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform.Equals(RuntimePlatform.Android))
        {
            if (placingWeapon)
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
                        Debug.Log(timetoplaceWeapon);
                        if (timetoplaceWeapon <= 0f)
                        {
                            mainWeapon.transform.position = mouseWorldPosition;
                            ToggleWeaponAdjusting(false);
                            startButton.SetActive(true);
                            placingWeapon = false;
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
                            Destroy(instantiatedWeapon);
                            instantiatedWeapon = Instantiate(mainWeapon, mouseWorldPosition, mainWeapon.transform.rotation);
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

            if (placingWeapon)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    mainWeapon.transform.position = mouseWorldPosition;
                    ToggleWeaponAdjusting(false);
                    startButton.SetActive(true);
                    placingWeapon = false;
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
                        Destroy(instantiatedWeapon);
                        instantiatedWeapon = Instantiate(mainWeapon, mouseWorldPosition, mainWeapon.transform.rotation);
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
        mainWeapon = Resources.Load<GameObject>("turret");
        placingWeapon = true;
        weapontoPlace.text = "turret";
        ToggleWeaponAdjusting(true);
        instantiatedWeapon = Instantiate(mainWeapon, mouseWorldPosition, mainWeapon.transform.rotation);
        weaponsPanel.gameObject.SetActive(false);
    }
    public void GetTurretT2()
    {
        mainWeapon = Resources.Load<GameObject>("turret 2");
        placingWeapon = true;
        weapontoPlace.text = "turret (Tier 2)";
        ToggleWeaponAdjusting(true);
        instantiatedWeapon = Instantiate(mainWeapon, mouseWorldPosition, mainWeapon.transform.rotation);
        weaponsPanel.gameObject.SetActive(false);
    }
    public void GetTurretT3()
    {
        mainWeapon = Resources.Load<GameObject>("turret 3");
        placingWeapon = true;
        weapontoPlace.text = "turret (Tier 3)";
        ToggleWeaponAdjusting(true);
        instantiatedWeapon = Instantiate(mainWeapon, mouseWorldPosition, mainWeapon.transform.rotation);
        weaponsPanel.gameObject.SetActive(false);
    }

    public GameObject GetMainWeapon()
    {
        return mainWeapon;
    }

    public void isselectingWeapon(bool isPlacing)
    {
        placingWeapon = isPlacing;
    }

    public void ToggleWeaponAdjusting(bool isAdjusting)
    {
        rotate90.SetActive(isAdjusting);
        rotateneg90.SetActive(isAdjusting);
    }

    public Vector3 GetWeaponPosition()
    {
        return mouseWorldPosition;
    }

    public Touch GetTouch()
    {
        return touch;
    }
}
