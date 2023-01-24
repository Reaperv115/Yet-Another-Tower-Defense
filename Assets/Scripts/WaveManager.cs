using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    GameObject round1Pack;
    GameObject[] round2Pack = new GameObject[2];
    GameObject[] round3Pack = new GameObject[3];

    public GameObject[] enemyhealthCheck;

    int level, round;

    bool spawn = false;
    int numenemiestoSpawn, maxnumEnemies;

    bool roundWon;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogError("attempting to make wave manager duplicate");

        round1Pack = EnemyManager.instance.GetRound1Car();

        round2Pack[0] = EnemyManager.instance.GetRound1Car();
        round2Pack[1] = EnemyManager.instance.GetRound2Car();

        round3Pack[0] = EnemyManager.instance.GetRound1Car();
        round3Pack[1] = EnemyManager.instance.GetRound2Car();
        round3Pack[2] = EnemyManager.instance.GetRound3Car();

        maxnumEnemies = numenemiestoSpawn = 10;
        level = round = 1;
        roundWon = false;
    }


    // Update is called once per frame
    void Update()
    {
    }

    public GameObject GetRound1Pack()
    {
        return round1Pack;
    }
    public GameObject[] GetRound2Pack()
    {
        return round2Pack;
    }
    public GameObject[] GetRound3Pack()
    {
        return round3Pack;
    }

    public int GetNumEnemiesToSpawn()
    {
        return numenemiestoSpawn;
    }
    public void SetNumEnemiesToSpawn()
    {
        numenemiestoSpawn -= 1;
    }
    public int GetMaxNumEnemiesToSpawn()
    {
        return maxnumEnemies;
    }
    public void SetMaxNumEnemiesToSpawn()
    {
        maxnumEnemies += 5;
    }
    public void SetSpawn(bool canSpawn)
    {
        spawn = canSpawn;
    }
    public bool CanSpawn()
    {
        return spawn;
    }
    public int GetLevel()
    {
        return level;
    }
    public void SetLevel(int newLevel)
    {
        level = newLevel;
    }
    public int GetRound()
    {
        return round;
    }
    public void SetRound(int newRound)
    {
        round = newRound;
    }
    public void RoundWon()
    {
        roundWon = true;
    }
    public bool DidWinRound()
    {
        return roundWon;
    }
    public void SetWinRound(bool didWin)
    {
        roundWon = didWin;
    }
}
