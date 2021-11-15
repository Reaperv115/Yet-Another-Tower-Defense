using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class selectWeapon : MonoBehaviour
{
    TextMeshProUGUI weapontoPlace;
    GameObject weaponDirection;
    GameObject weaponsPanel;
    GameObject mainWeapon, instantiatedWeapon;
    GameObject spot;
    GameObject startButton;
    Sprite pointer;
    Transform tPointer;
    Ray ray;
    RaycastHit hit;
    Touch touch;

    Vector3 newworldPoint, oldworldPoint;
    Vector3 mouseWorldPosition;
    bool placingWeapon;

    float rotationAngle = 90;
    float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        weaponsPanel = GameObject.Find("Weapons Panel");
        weaponsPanel.gameObject.SetActive(false);
        weapontoPlace = GameObject.Find("Weapon to Place").GetComponent<TextMeshProUGUI>();
        weaponDirection = GameObject.Find("Weapon Direction");
        startButton = GameObject.Find("BeginGame");
        pointer = Resources.Load<Sprite>("Arrow1");
        Debug.Log(weaponsPanel);
        placingWeapon = false;
        rotationSpeed = rotationAngle;
        spot = Resources.Load<GameObject>("touch test");
        Debug.Log(spot);
        tPointer = weaponDirection.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.touchCount > 0)
        // {
        //     Touch touch = Input.GetTouch(0);
        //     newworldPoint = touch.position;
        //     newworldPoint.z = Mathf.Abs(Camera.main.transform.position.z);
        //     mouseWorldPosition = Camera.main.ScreenToWorldPoint(newworldPoint);
        //     mouseWorldPosition.z = 0f;
        //     Instantiate(spot, mouseWorldPosition, spot.transform.rotation);
        //     //you have selected a weapon
        //     if (mainWeapon)
        //     {
        //         weaponDirection.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = pointer;
        //         mainWeapon.transform.rotation = weaponDirection.transform.rotation;
        //         Instantiate(mainWeapon, touch.position, mainWeapon.transform.rotation);
        //         Debug.Log("instantiating weapon");
        //     }
        //     else
        //     {
        //         weaponDirection.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        //     }
        // }
        

        if (placingWeapon)
        {
            newworldPoint = touch.position;
            newworldPoint.z = Mathf.Abs(Camera.main.transform.position.z);
            mouseWorldPosition = Camera.main.ScreenToWorldPoint(newworldPoint);
            mouseWorldPosition.z = 0f;
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
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
                newworldPoint.z = Mathf.Abs(Camera.main.transform.position.z);
                mouseWorldPosition = Camera.main.ScreenToWorldPoint(newworldPoint);
                mouseWorldPosition.z = 0f;
                Destroy(instantiatedWeapon);
                instantiatedWeapon = Instantiate(mainWeapon, mouseWorldPosition, mainWeapon.transform.rotation);
            }
        }
    }

    public void onClick()
    {
        
        weaponsPanel.gameObject.SetActive(true);
    }

    public void getTurret()
    {
        mainWeapon = Resources.Load<GameObject>("turret");
        Debug.Log(mainWeapon);
        placingWeapon = true;
        // //selectingWeapon = !selectingWeapon;
        weapontoPlace.text = "turret";
        tPointer.GetComponent<SpriteRenderer>().sprite = pointer;
        instantiatedWeapon = Instantiate(mainWeapon, mouseWorldPosition, mainWeapon.transform.rotation);
        // Debug.Log("turret selected");
        //gameObject.SetActive(false);
        weaponsPanel.SetActive(false);
        Debug.Log("turret button pressed");
    }

    public GameObject GetMainWeapon()
    {
        return mainWeapon;
    }

    public GameObject getSpot()
    {
        return spot;
    }

    public void isselectingWeapon(bool isPlacing)
    {
        placingWeapon = isPlacing;
    }
}
