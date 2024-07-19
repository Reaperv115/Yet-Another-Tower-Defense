using UnityEngine;
using UnityEngine.Advertisements;
 
public class BannerAd : MonoBehaviour
{
 
    BannerPosition _bannerPosition = BannerPosition.TOP_CENTER;
 
    [SerializeField] string _androidAdUnitId = "Banner_Android";
    [SerializeField] string _iOSAdUnitId = "Banner_iOS";
    string _adUnitId = null; // This will remain null for unsupported platforms.
 
    void Start()
    {
        // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif
        LoadBanner();
    }

    void Update()
    {
        ShowBannerAd();
    }
 
    // Implement a method to call when the Load Banner button is clicked:
    public void LoadBanner()
    {
 
        // Load the Ad Unit with banner content:
        Advertisement.Banner.Load(_adUnitId);
        Debug.Log("Banner loaded");
    }
 
 
    // Implement a method to call when the Show Banner button is clicked:
    void ShowBannerAd()
    {

        Advertisement.Banner.SetPosition(_bannerPosition);
 
        // Show the loaded Banner Ad Unit:
        Advertisement.Banner.Show(_adUnitId);
    }
}