using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class NextLevel : MonoBehaviour
{
    GameManager gm;
    GameObject[] turrets;
    GameObject tower;

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
        Debug.Log(gm.GetTrackIndex());
        gm.LoadTrackInst();
        gm.GetWeaponsButton().gameObject.SetActive(true);
        gm.SetHasBegun(false);
        gameObject.SetActive(false);
        gm.GetVictoryDisplay().GetComponent<TextMeshProUGUI>().text = "";
        tower = gm.FindTower(gm.GetTrack());
        tower.GetComponent<Tower>().setHealth(1f);
        gm.GetHealthBar().localScale = new Vector3(tower.GetComponent<Tower>().getHealth(), 1f);
        gm.SetScore(6);
        gm.SetCurrentRound(1);
    }
}
