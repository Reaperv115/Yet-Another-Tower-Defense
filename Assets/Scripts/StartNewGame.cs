using UnityEngine;
using UnityEngine.SceneManagement;

public class StartNewGame : MonoBehaviour
{
    public void BeginNewGame() { SceneManager.LoadScene("Game"); }
}
