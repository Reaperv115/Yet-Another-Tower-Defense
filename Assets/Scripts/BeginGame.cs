using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginGame : MonoBehaviour
{
    public void StartPlaying() { SceneManager.LoadScene("Game"); }
}
