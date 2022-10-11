using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToHome : MonoBehaviour
{
    public void GoBackHome() { SceneManager.LoadScene("MainMenu"); }
}
