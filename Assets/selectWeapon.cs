using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class selectWeapon : MonoBehaviour
{
    TextMeshProUGUI weapontoPlace;
    GameObject weaponsPanel;
    GameObject mainWeapon;
    GameObject mainweaponInstantiated;
    bool selectingWeapon;

    Vector3 newworldPoint, oldworldPoint;
    Vector3 mouseWorldPosition;

    float rotationAngle = 90;
    float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        weaponsPanel = GameObject.Find("Weapons Panel");
        weapontoPlace = GameObject.Find("Weapon to Place").GetComponent<TextMeshProUGUI>();
        Debug.Log(weaponsPanel);
        selectingWeapon = false;
        rotationSpeed = rotationAngle;
    }

    // Update is called once per frame
    void Update()
    {
        //you have selected a weapon
        if (mainweaponInstantiated)
        {
            
            newworldPoint.z = Mathf.Abs(Camera.main.transform.position.z);
            mouseWorldPosition = Camera.main.ScreenToWorldPoint(newworldPoint);
            mouseWorldPosition.z = 0f;
            mainweaponInstantiated.transform.position = mouseWorldPosition;
        }

        // placing the chosen weapon where you clicked the mouse
        if (Input.GetMouseButtonDown(0))
        {
            if (!mainweaponInstantiated) { }
            else
            {
                newworldPoint = Input.mousePosition;
                newworldPoint.z = Mathf.Abs(Camera.main.transform.position.z);
                mouseWorldPosition = Camera.main.ScreenToWorldPoint(newworldPoint);
                mouseWorldPosition.z = 0f;

                mainweaponInstantiated.transform.position = mouseWorldPosition;
                mainweaponInstantiated = null;
            }
        }

        if (selectingWeapon)
            weaponsPanel.gameObject.SetActive(true);
        else
            weaponsPanel.gameObject.SetActive(false);
    }

    public void onClick()
    {
        selectingWeapon = !selectingWeapon;
        
    }

    public void getTurret()
    {
        mainWeapon = Resources.Load<GameObject>("turret");
        Debug.Log(mainWeapon);
        selectingWeapon = !selectingWeapon;
        newworldPoint = Input.mousePosition;
        newworldPoint.z = Mathf.Abs(Camera.main.transform.position.z);
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(newworldPoint);
        mouseWorldPosition.z = 0f;
        mainweaponInstantiated = Instantiate(mainWeapon, mouseWorldPosition, mainWeapon.transform.rotation);
        weapontoPlace.text = "turret";
        Debug.Log("turret selected");
    }
}
