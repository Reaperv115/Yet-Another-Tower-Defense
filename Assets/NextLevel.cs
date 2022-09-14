using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public void ProceedNextLevel()
    {
        SceneManager.LoadScene("Level 2");
    }
}
