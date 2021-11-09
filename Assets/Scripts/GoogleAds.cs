using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class GoogleAds : MonoBehaviour, IUnityAdsListener
{
    public static GoogleAds instance;

    string googlePlayID = "4093133";
    string mySurfacingId = "rewardedVideo";

    public GameObject ExtraBallBt;

    public bool testMode = false;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Advertisement.AddListener(this);
        Advertisement.Initialize(googlePlayID, testMode);
    }

    public void DisplayAdsNextLevel()
    {
        Advertisement.Show();
    }
    public void DisplayAds()
    {
        //Debug.Log("Active? " + BallControl.instance.noMove);
        StartCoroutine(WaitingCanvas());
        //Debug.Log("Active 2? " + BallControl.instance.noMove);
        Advertisement.Show();
    }

    public void DisplayVideo()
    {
        Advertisement.Show(mySurfacingId);
    }
    public void ShowRewardedVideo()
    {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady(mySurfacingId))
        {
            Advertisement.Show(mySurfacingId);
        }
        else
        {
            Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
        }
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string surfacingId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            StartCoroutine(WaitingCanvas());
            Destroy(ExtraBallBt);
            //ExtraBallBt.SetActive(false);
            BallControl.instance.numOfMoves += 1;
            Debug.Log("Ganhou a recompensa!");
            // Reward the user for watching the ad to completion.
        }
        else if (showResult == ShowResult.Skipped)
        {
            Debug.Log("Não ganhou a recompensa :(");
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsReady(string surfacingId)
    {
        // If the ready Ad Unit or legacy Placement is rewarded, show the ad:
        if (surfacingId == mySurfacingId)
        {
            // Optional actions to take when theAd Unit or legacy Placement becomes ready (for example, enable the rewarded ads button)
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string surfacingId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }

    IEnumerator WaitingCanvas()
    {
        BallControl.instance.noMove = !BallControl.instance.noMove;
        //Debug.Log("Active 3? " + BallControl.instance.noMove);
        yield return new WaitForSeconds(1f);
    }
}
