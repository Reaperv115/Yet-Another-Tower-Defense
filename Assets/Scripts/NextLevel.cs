using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    GameObject[] turrets;
    GameObject tower;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ProceedNextLevel()
    {
        Destroy(GameManager.instance.GetTrackInst());
        GameManager.instance.SetTrackIndex(WaveManager.instance.GetLevel());
        GameManager.instance.LoadTrackInst();
        GameManager.instance.GetWeaponsButton().gameObject.SetActive(true);
        GameManager.instance.GetVictoryDisplay().GetComponent<TextMeshProUGUI>().text = "";
        WaveManager.instance.SetRound(1);
        WaveManager.instance.ResetMaxNumEnemies();
        WeaponManager.instance.SetNumTurretsToPlace();
        WaveSpawner.instance.startingPos = GameManager.instance.FindEnemySpawn(GameManager.instance.GetTrack()).transform;
        turrets = GameObject.FindGameObjectsWithTag("weapon");
        foreach (GameObject obj in turrets) Destroy(obj);
        gameObject.SetActive(false);
        tower = GameManager.instance.FindTower(GameManager.instance.GetTrack());
        tower.GetComponent<Tower>().SetHealth(1f);

    }
}
