using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGame : MonoBehaviour
{
    private bool hasBegun;
    // Start is called before the first frame update
    void Start()
    {
        hasBegun = false;
    }

    public void startGame()
    {
        Debug.Log("game has started");
        SetHasBegun(true);
        gameObject.SetActive(false);
    }

    public bool hasStarted()
    {
        return hasBegun;
    }

    public void SetHasBegun(bool Begun)
    {
        hasBegun = Begun;
    }
}
