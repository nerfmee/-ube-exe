using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UnityAdsScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Monetization.isSupported) Monetization.Initialize("3573100", false);
    }

    //rewardedVideo
    public void ShowAds()
    {
        if (Monetization.IsReady("rewardedVideo"))
        {
            Time.timeScale = 0;
            ShowAdCallbacks options = new ShowAdCallbacks();
            options.finishCallback = HandleShowResult;
            ShowAdPlacementContent ad = Monetization.GetPlacementContent("rewardedVideo") as ShowAdPlacementContent;
            ad.Show(options);
        }
    }


    void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            Time.timeScale = 1;
        }
        else if (result == ShowResult.Skipped)
        {
            Time.timeScale = 1;
        }
        else if (result == ShowResult.Failed)
        {
            Time.timeScale = 1;
        }
    }
}
