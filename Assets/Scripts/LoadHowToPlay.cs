using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadHowToPlay : MonoBehaviour
{
    public void LoadLevel()
    {
        SceneManager.LoadScene("HowToPlay");
    }
}
