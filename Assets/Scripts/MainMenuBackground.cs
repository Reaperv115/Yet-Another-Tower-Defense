using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuBackground : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI Title;
    Color[] backgroundColors = { Color.black, Color.green, Color.blue, Color.yellow, Color.red };
    float colorTimer;
    int newtmp;
    int tmp;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("background length: " + backgroundColors.Length);
        tmp = Random.Range(0, backgroundColors.Length);
        Debug.Log("tmp: " + tmp);
        newtmp = tmp + 3;
        Debug.Log("newtmp: " + newtmp);
        if (newtmp > backgroundColors.Length - 1)
        {
            newtmp = newtmp - backgroundColors.Length;
            Debug.Log("newtmp: " + newtmp);
        }
        GetComponent<SpriteRenderer>().color = backgroundColors[tmp];
        Title.color = backgroundColors[newtmp];
        colorTimer = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (colorTimer <= 0f)
        {
            tmp = Random.Range(0, backgroundColors.Length);
            Debug.Log("tmp: " + tmp);
            newtmp = tmp + 3;
            Debug.Log("newtmp: " + newtmp);
            if (newtmp > backgroundColors.Length - 1)
            {
                newtmp = newtmp - backgroundColors.Length;
                Debug.Log("newtmp: "  + newtmp);
            }
            GetComponent<SpriteRenderer>().color = backgroundColors[tmp];
            Title.color = backgroundColors[newtmp];
            colorTimer = 5f;
        }
        else
        {
            colorTimer -= Time.deltaTime;
        }
    }
}
