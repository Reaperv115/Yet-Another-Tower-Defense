using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    private float fHealth;

    GameObject towerhealthDisplay;

    // Start is called before the first frame update
    void Start()
    {
        fHealth = 5f;
        towerhealthDisplay = GameObject.Find("tower health");
        //towerhealthDisplay.GetComponent<Slider>().value = fHealth;
        Debug.Log(towerhealthDisplay.GetComponent<Slider>().maxValue);
        //fHealth = towerhealthDisplay.GetComponent<Slider>().value;
        Debug.Log("tower health: " + fHealth);
    }

    // Update is called once per frame
    void Update()
    {
        towerhealthDisplay.GetComponent<Slider>().value = fHealth;
    }

    public float getHealth()
    {
        //Debug.Log(fHealth);
        return fHealth;
    }

    public void setHealth(float health)
    {
        fHealth = health;
    }

    public GameObject gettowerhealthDisplay()
    {
        return towerhealthDisplay;
    }
}
