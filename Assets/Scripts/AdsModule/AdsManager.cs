using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour {
    // Start is called before the first frame update

    const string playstoreGameID = "4060235";
    const string bannerID = "banner2";
    const string videoID = "video";

    bool adsStart;
    public bool isTest = true;
    public static AdsManager _instance;

    private void Awake () {
        if (_instance == null) {
            _instance = this;
        }
        else if (_instance != this) {
            Destroy (gameObject);
        }
    }

    public static void CallAds (AdsType type ) {
        if(type == AdsType.banner)  _instance.StartCoroutine (_instance.CallBanner ());
        else _instance.StartCoroutine (_instance.CallVideo ());
    }
    IEnumerator CallBanner () {
        Advertisement.Initialize (playstoreGameID, isTest);

        while (!Advertisement.IsReady ()) yield return null;

        Advertisement.Show (bannerID);
    }
    IEnumerator CallVideo () {
        Advertisement.Initialize (playstoreGameID, isTest);

        while (!Advertisement.IsReady ()) yield return null;

        Advertisement.Show (videoID);
    }
}