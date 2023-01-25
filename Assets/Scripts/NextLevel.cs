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
        gm.SetTrackIndex(WaveManager.instance.GetLevel());
        gm.LoadTrackInst();
        gm.GetWeaponsButton().gameObject.SetActive(true);
        gameObject.SetActive(false);
        gm.GetVictoryDisplay().GetComponent<TextMeshProUGUI>().text = "";
        tower = gm.FindTower(gm.GetTrack());
        tower.GetComponent<Tower>().setHealth(1f);
        gm.GetHealthBar().localScale = new Vector3(tower.GetComponent<Tower>().getHealth(), 1f);
        gm.SetScore(6);
        WaveManager.instance.SetRound(1);
        WaveManager.instance.ResetMaxNumEnemies();
        WaveSpawner.instance.startingPos = GameManager.instance.FindEnemySpawn(GameManager.instance.GetTrack()).transform;
    }
}
