using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public void ProceedNextLevel()
    {
        GameManager gm = GameObject.Find("Main Camera").GetComponent<GameManager>();
        Destroy(GameObject.FindGameObjectWithTag("path"));
        gm.SetCurrentRound(gm.GetCurrentRound() + 1);
        GameObject track = Resources.Load<GameObject>("Levels/" + gm.GetCurrentRound() + "/track");
        Instantiate(track, track.transform.position, track.transform.rotation);
    }
}
