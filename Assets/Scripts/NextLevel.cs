using UnityEngine;
using TMPro;

public class NextLevel : MonoBehaviour
{
    GameObject[] turrets;
    GameObject tower;

    public void ProceedNextLevel()
    {
        turrets = GameObject.FindGameObjectsWithTag("weapon");
        foreach(GameObject obj in turrets)
            Destroy(obj);
        Destroy(GameManager.instance.GetTrackInst());
        GameManager.instance.SetTrackIndex(WaveManager.instance.GetLevel());
        GameManager.instance.LoadTrackInst();
        GameManager.instance.GetWeaponsButton().gameObject.SetActive(true);
        gameObject.SetActive(false);
        GameManager.instance.GetVictoryDisplay().GetComponent<TextMeshProUGUI>().text = "";
        tower = GameManager.instance.FindTower(GameManager.instance.GetTrack());
        tower.GetComponent<Tower>().setHealth(1f);
        GameManager.instance.GetHealthBar().localScale = new Vector3(tower.GetComponent<Tower>().getHealth(), 1f);
        GameManager.instance.SetScore(6);
        WaveManager.instance.SetRound(1);
        WaveManager.instance.ResetMaxNumEnemies();
        WaveSpawner.instance.startingPos = GameManager.instance.FindEnemySpawn(GameManager.instance.GetTrack()).transform;
    }
}
