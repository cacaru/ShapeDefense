using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using GoogleMobileAds;
//using GoogleMobileAds.Api;
using System;
using static ShapeDefenseSpace.GameData;

public class GoogleAdvManager : SceneSingleton<GoogleAdvManager>
{
    /*
    private string _adUnitId;
    private RewardedAd _rewardedAd;
    private AdRequest _adRequest;

    // 0 : x // 1 : c_grade_chest // 2 : 
    private int reward_type = 0;

    // Start is called before the first frame update
    void Start()
    {
        // initialize
        MobileAds.Initialize((InitializationStatus init_status) => {
            // init call back
        });

                  // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
    _adUnitId = "ca-app-pub-3940256099942544/5224354917";
#else
    private string _adUnitId = "unused";
#endif

        LoadRewardedAd();
    }

    public void LoadRewardedAd() {
        // Clean up the old ad before loading a new one.
        if (_rewardedAd != null) {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        //Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        _adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(_adUnitId, _adRequest,
            (RewardedAd ad, LoadAdError error) => {
                // if error is not null, the load request failed.
                if (error != null || ad == null) {
                    Debug.LogError("Rewarded ad failed to load an ad with error : " + error);
                    return;
                }

                //Debug.Log("Rewarded ad loaded with response : " + ad.GetResponseInfo());

                _rewardedAd = ad;
            });
    }

    public void ShowAds(int type) {
        reward_type = type;
        if (_rewardedAd.CanShowAd()) {
            _rewardedAd.Show(GetReward);
        }
        else {
            Debug.LogError("adv failed");
        }
    }

    public void GetReward(Reward reward) {
        switch (reward_type) {
            case 1:
                PlayerPrefs.SetInt("Adv_UnitChest_RemainTime", 1800);
                PlayerPrefs.Save();
                BuyConfirm.Instance.ChestCheck();
                datahub.AdvUnitTimerStart();
                AdvChestChecker.Instance.UnitAdvTimerShowing();
                break;
            case 2:
                PlayerPrefs.SetInt("Adv_StaChest_RemainTime", 1800);
                PlayerPrefs.Save();
                BuyConfirm.Instance.AdvStaminaBuy();
                datahub.AdvStaTimerStart();
                AdvChestChecker.Instance.StaAdvTimerShowing();
                break;
        }
        //BuyConfirm.Instance.ChestBuyCancel();
    }


    */
}
