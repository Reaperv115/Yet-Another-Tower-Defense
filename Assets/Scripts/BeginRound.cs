using UnityEngine;

public class BeginRound : MonoBehaviour
{
    [SerializeField]
    GameObject cam;
    public void StartRound()
    {
        if (cam.GetComponent<Player>().IsPlacingWeapon())
        { }
        else
        {
            WaveManager.instance.BeginRound();
            GameManager.instance.GetRestartButton().SetActive(false);
            GameManager.instance.GetWeaponsButton().SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
