using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [HideInInspector]
    public int amount;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.LogError("an instance of this class already exists");
        }
        amount = 6;
    }
}
