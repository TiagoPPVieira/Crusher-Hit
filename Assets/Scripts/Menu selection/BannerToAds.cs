using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerToAds : MonoBehaviour
{
    string googlePlayID = "4093133";
    string placementId = "Banner";

    public bool testMode = false;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        Advertisement.Initialize(googlePlayID, testMode);
        while (!Advertisement.IsReady(placementId))
            yield return null;

        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        Advertisement.Banner.Show(placementId);
    }
}
