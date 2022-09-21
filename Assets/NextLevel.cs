using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class NextLevel : MonoBehaviour
{
    GameManager gm;
    GameObject[] turrets;

    private void Start()
    {
        gm = GameObject.Find("Main Camera").GetComponent<GameManager>();
    }
    public void ProceedNextLevel()
    {
        turrets = GameObject.FindGameObjectsWithTag("weapon");
        foreach(GameObject obj in turrets)
            Destroy(obj);
        Destroy(gm.GetTrackInst());
        gm.SetTrackIndex(gm.GetTrackIndex() + 1);
        gm.LoadTrackInst();
        gm.GetWeaponsButton().gameObject.SetActive(true);
        gm.SetHasBegun(false);
        gameObject.SetActive(false);
        gm.GetVictoryDisplay().GetComponent<TextMeshProUGUI>().text = "";
    }
}
