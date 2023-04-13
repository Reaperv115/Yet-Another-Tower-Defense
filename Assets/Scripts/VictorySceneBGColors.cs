using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictorySceneBGColors : MonoBehaviour
{
    Color[] possibleColors = { Color.black, Color.red, Color.green, Color.blue, Color.cyan, Color.magenta };
    int colorIndex, congratscolorIndex;
    float bgcolorTimer = 0f;
    [SerializeField]
    GameObject congratulations;
    // Start is called before the first frame update
    void Start()
    {
        colorIndex = 0;
        congratscolorIndex = colorIndex + 1;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RawImage>().color = Color.Lerp(possibleColors[colorIndex], possibleColors[colorIndex + 1], bgcolorTimer);
        congratulations.GetComponent<TextMeshProUGUI>().color = Color.Lerp(possibleColors[congratscolorIndex], possibleColors[congratscolorIndex + 1], bgcolorTimer);
        if (bgcolorTimer > 1f)
        {
            if (colorIndex.Equals(possibleColors.Length - 2))
                colorIndex = 0;
            else
                ++colorIndex;
            if (congratscolorIndex.Equals(possibleColors.Length - 2))
                congratscolorIndex = 0;
            else
                ++congratscolorIndex;
            bgcolorTimer = 0f;
        }
        else
            bgcolorTimer += Time.deltaTime;
    }
}
