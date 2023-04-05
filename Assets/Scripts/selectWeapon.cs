using TMPro;
using UnityEngine;

public class SelectWeapon : MonoBehaviour
{
    Player player;
    Ray ray;
    RaycastHit2D hit;
    Touch touch;

    Vector3 newworldPoint, oldworldPoint;
    Vector3 mouseWorldPosition;
    bool placingWeapon;

    float rotationAngle = 90;
    float rotationSpeed;
    float timetoplaceWeapon;

    bool activateweaponsPanel;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        placingWeapon = false;
        rotationSpeed = rotationAngle;
        timetoplaceWeapon = .05f;
        activateweaponsPanel = false;
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.instance.GetWeaponsPanel().SetActive(activateweaponsPanel);
        // weapon placement mechanics for android
        if (Application.platform.Equals(RuntimePlatform.Android))
        {
            if (player.IsPlacingWeapon())
            {
                GameManager.instance.GetWeaponsButton().SetActive(false);
                
                if (Input.touchCount > 0)
                {
                    touch = Input.GetTouch(0);
                    newworldPoint = touch.position;
                    newworldPoint.z = Mathf.Abs(Camera.main.transform.position.z);
                    mouseWorldPosition = Camera.main.ScreenToWorldPoint(newworldPoint);
                    mouseWorldPosition.z = 0f;
                    if (touch.phase.Equals(TouchPhase.Stationary))
                    {
                        if (timetoplaceWeapon <= 0f)
                        {
                            player.mainWeapon.transform.position = mouseWorldPosition;
                            GameManager.instance.GetWeaponsButton().SetActive(true);
                            GameManager.instance.GetBeginRoundButton().SetActive(true);
                            GameManager.instance.GetWeaponToPlaceDisplay().GetComponent<TextMeshProUGUI>().text = "";
                            player.SetIsPlacing(false);
                            timetoplaceWeapon = 1f;
                            GameManager.instance.GetRestartButton().SetActive(true);
                        }
                        else
                            timetoplaceWeapon -= Time.deltaTime;
                    }
                    else
                    {
                        timetoplaceWeapon = .05f;
                        oldworldPoint = newworldPoint;
                        newworldPoint = touch.position;
                        if (!newworldPoint.Equals(oldworldPoint))
                        {
                            newworldPoint = touch.position;
                            newworldPoint.z = Mathf.Abs(Camera.main.transform.position.z);
                            mouseWorldPosition = Camera.main.ScreenToWorldPoint(newworldPoint);
                            mouseWorldPosition.z = 0f;
                            Destroy(player.instantiatedmainWeapon);
                            player.instantiatedmainWeapon = Instantiate(player.mainWeapon, mouseWorldPosition, player.mainWeapon.transform.rotation);
                        }
                    }
                }


            }
        }
        // weapon placement mechanics for pc
        if (Application.platform.Equals(RuntimePlatform.WindowsPlayer))
        {
            newworldPoint = Input.mousePosition;
            newworldPoint.z = Mathf.Abs(Camera.main.transform.position.z);
            mouseWorldPosition = Camera.main.ScreenToWorldPoint(newworldPoint);
            mouseWorldPosition.z = 0f;

            if (player.IsPlacingWeapon())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    player.mainWeapon.transform.position = mouseWorldPosition;
                    ToggleWeaponAdjusting(false);
                    GameManager.instance.GetStartButton().SetActive(true);
                    player.SetIsPlacing(false);
                }
                else
                {
                    timetoplaceWeapon = 1.0f;
                    oldworldPoint = newworldPoint;
                    newworldPoint = Input.mousePosition;
                    if (!newworldPoint.Equals(oldworldPoint))
                    {
                        newworldPoint = Input.mousePosition;
                        newworldPoint.z = Mathf.Abs(Camera.main.transform.position.z);
                        mouseWorldPosition = Camera.main.ScreenToWorldPoint(newworldPoint);
                        mouseWorldPosition.z = 0f;
                        Destroy(player.instantiatedmainWeapon);
                        player.instantiatedmainWeapon = Instantiate(player.mainWeapon, mouseWorldPosition, player.mainWeapon.transform.rotation);
                    }
                }
            }
        }
    }

    // used to hide and show weapons selection
    // panel when you hit the weapons button
    public void onClick()
    {
        activateweaponsPanel = !activateweaponsPanel;
        GameManager.instance.GetWeaponsPanel().SetActive(activateweaponsPanel);
    }

    // selects the tier 1 turret
    public void GetTurret()
    {
        if (GameManager.instance.GetT1Price() > ScoreManager.instance.amount) GameManager.instance.SetDisplayTimer(2f);
        else player.LoadWeapon(WeaponManager.instance.GetBasicTurret());
        activateweaponsPanel = !activateweaponsPanel;
        GameManager.instance.GetRestartButton().SetActive(false);
    }

    // selects the tier 2 turret
    public void GetTurretT2()
    {
        if (GameManager.instance.GetT2Price() > ScoreManager.instance.amount) GameManager.instance.SetDisplayTimer(2f);
        else player.LoadWeapon(WeaponManager.instance.GetAdvancedTurret());
        activateweaponsPanel = !activateweaponsPanel;
        GameManager.instance.GetRestartButton().SetActive(false);
    }

    // selects the tier 3 turret
    public void GetTurretT3()
    {
        if (GameManager.instance.GetT3Price() > ScoreManager.instance.amount) GameManager.instance.SetDisplayTimer(2f);
        else player.LoadWeapon(WeaponManager.instance.GetAdvancedTurret());
        activateweaponsPanel = !activateweaponsPanel;
        GameManager.instance.GetRestartButton().SetActive(false);
    }

    

    public void isselectingWeapon(bool isPlacing)
    {
        placingWeapon = isPlacing;
    }

    public void ToggleWeaponAdjusting(bool isAdjusting)
    {
        GameManager.instance.GetWeaponsButton().SetActive(isAdjusting);
    }

    public Vector3 GetWeaponPosition()
    {
        return mouseWorldPosition;
    }
    public Vector3 getMWP()
    {
        return mouseWorldPosition;
    }
}
