using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        tmp = 0;
        newtmp = tmp + 1;
        GetComponent<SpriteRenderer>().color = backgroundColors[tmp];
        Title.color = backgroundColors[newtmp];
        colorTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().color = Color.Lerp(backgroundColors[tmp], backgroundColors[tmp + 1], colorTimer);
        Title.color = Color.Lerp(backgroundColors[newtmp], backgroundColors[newtmp + 1], colorTimer);
        if (colorTimer > 1f)
        {
            if (tmp.Equals(backgroundColors.Length - 2))
            {
                tmp = 0;
            }
            else
            {
                ++tmp;
            }
            if (newtmp.Equals(backgroundColors.Length - 2))
            {
                newtmp = 0;
            }
            else
            {
                ++newtmp;
            }
            colorTimer = 0f;
        }
        else
        {
            colorTimer += Time.deltaTime;
        }

    }
}
