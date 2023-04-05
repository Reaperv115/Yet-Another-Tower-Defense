using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginRound : MonoBehaviour
{
    public void StartRound()
    {
        WaveManager.instance.BeginRound();
        GameManager.instance.GetRestartButton().SetActive(false);
        GameManager.instance.GetWeaponsButton().SetActive(false);
        gameObject.SetActive(false);
    }
}
