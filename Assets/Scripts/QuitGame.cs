using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
    public void LeaveGame() { SceneManager.LoadScene("MainMenu"); }
}
