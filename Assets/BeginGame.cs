using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginGame : MonoBehaviour
{

    public void StartPlaying()
    {
        SceneManager.LoadScene("Game");
    }
}