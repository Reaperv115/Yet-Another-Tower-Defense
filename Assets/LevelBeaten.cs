using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelBeaten : MonoBehaviour
{
    TextMeshProUGUI message;
    [SerializeField]
    GameObject nextlevelButton;
    float messageTimer;
    // Start is called before the first frame update
    void Start()
    {
        message = GameObject.Find("Level Beaten Message").GetComponent<TextMeshProUGUI>();
        messageTimer = 3f;
    }

    // Update is called once per frame
    void Update()
    {


        if (messageTimer <= 0f)
        {
            message.text = "";
            nextlevelButton.SetActive(true);
        }
        else
        {
            message.text = "Congrats! You've beaten the level! Get ready for the next one!";
            messageTimer -= Time.deltaTime;
        }    
    }
}
