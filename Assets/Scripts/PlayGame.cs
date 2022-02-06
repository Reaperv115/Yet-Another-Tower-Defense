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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        hasBegun = true;
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
