using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictorySceneBGColors : MonoBehaviour
{
    Color[] possibleColors = { Color.black, Color.red, Color.green, Color.blue, Color.cyan, Color.magenta };
    int colorIndex;
    float colorTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        colorIndex = 0;
        Debug.Log(possibleColors.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
        GetComponent<RawImage>().color = Color.Lerp(possibleColors[colorIndex], possibleColors[colorIndex + 1], colorTimer);
        if (colorTimer > 1f)
        {
            if (colorIndex.Equals(possibleColors.Length - 2))
            {
                colorIndex = 0;
            }
            else
            {
                ++colorIndex;
            }
            colorTimer = 0f;
        }
        else
        {
            colorTimer += Time.deltaTime;
        }
    }
}
