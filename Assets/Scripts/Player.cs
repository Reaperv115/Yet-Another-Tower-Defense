using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    float nomoreturretstoplacedisplayTimer = 2f;
    SelectWeapon swRef;
    bool placingWeapon;
    bool tooClose = false;
    
    private void Awake()
    {
        swRef = GetComponent<SelectWeapon>();
    }

    public float GetDisplayTimer() { return nomoreturretstoplacedisplayTimer; }
    public void SetDisplayTimer(float time) { nomoreturretstoplacedisplayTimer = time; }
    public void LoadWeapon(GameObject weapon)
    {
        mainWeapon = weapon;
        GameManager.instance.GetWeaponToPlaceDisplay().GetComponent<TextMeshProUGUI>().text = mainWeapon.name;
        placingWeapon = true;
        GameManager.instance.GetWeaponsButton().SetActive(false);
        instantiatedmainWeapon = Instantiate(mainWeapon, swRef.getMWP(), mainWeapon.transform.rotation);
        WeaponManager.instance.TurretPlaced();
        GameManager.instance.GetWeaponsPanel().SetActive(false);
    }
    public GameObject instantiatedmainWeapon { get;  set; }
    public GameObject mainWeapon { get; set; }
    public bool IsPlacingWeapon() { return placingWeapon; }
    
    public void SetIsPlacing(bool isPlacing) { placingWeapon = isPlacing; }

    public void SetTooClose(bool istooclose) { tooClose = istooclose;  }
    public bool GetIsTooClose() { return tooClose; }
}
