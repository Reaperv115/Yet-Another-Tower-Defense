using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefeatedSceneColorChange : MonoBehaviour
{
    RawImage background;

    float fadeDuration = 1f;
    Color color1, color2;

    Color startColor, endColor;
    float lastcolorchangeTime;
    // Start is called before the first frame update
    void Start()
    {
        color1 = Color.red;
        color2 = Color.black;
        startColor = color1;
        endColor = color2;
        background = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        float ratio = (Time.time - lastcolorchangeTime) / fadeDuration;
        ratio = Mathf.Clamp01(ratio);
        background.color = Color.Lerp(startColor, endColor, ratio);

        if (ratio.Equals(1f))
        {
            lastcolorchangeTime = Time.time;
            Color temp = startColor;
            startColor = endColor;
            endColor = temp;
        }
    }
}
