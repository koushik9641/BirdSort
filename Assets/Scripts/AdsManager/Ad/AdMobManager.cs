using System;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System.Collections.Generic;

//10/05/2020
//01/12/2020
//22/02/2021
//18/10/2021
//18/12/2021
//30/12/2021 Variables and Random Reward
//18/03/2022 Changed To AdMod and Added UnityAds

public class AdMobManager : MonoBehaviour
{
    #region Variables

    private const string _testAppID = "ca-app-pub-3940256099942544~3347511713";
    private const string _testBannerID = "ca-app-pub-3940256099942544/6300978111";
    private const string _testInterstitialID = "ca-app-pub-3940256099942544/1033173712";
    private const string _testRewardID = "ca-app-pub-3940256099942544/5224354917";

    private BannerView _bannerView;
    private InterstitialAd _interstitialAd;
    private RewardedAd _rewardedAd;

    private string _appID;
    private string _bannerID;
    private string _interstitialID;
    private string _rewardID;

    private int _adLoadFailedType = 0;

    public BannerView BannerView => _bannerView;
    public InterstitialAd InterstitialAd => _interstitialAd;
    public RewardedAd RewardedAd => _rewardedAd;

    public int AdLoadFailedType { get { return _adLoadFailedType; } set { _adLoadFailedType = value; } }

    #endregion

    #region AdSetup

    protected void Awake()
    {
        //base.Awake();

        if (Config.isDebug)
        {
            DConsole.Log("Test Ad at: " + gameObject.name);
            _appID = _testAppID;
            _bannerID = _testBannerID;
            _interstitialID = _testInterstitialID;
            _rewardID = _testRewardID;
        }
        else
        {
            _appID = Config.adMobAppID;
            _bannerID = Config.adMobBannerID;
            _interstitialID = Config.adMobInterstitialID;
            _rewardID = Config.adMobRewardID;
        }
    }

    public void Start()
    {
        MobileAds.SetiOSAppPauseOnBackground(true);

        List<string> deviceIds = new List<string>() { AdRequest.TestDeviceSimulator };

        // Configure TagForChildDirectedTreatment and test device IDs.
        /*        RequestConfiguration requestConfiguration =
                    new RequestConfiguration.Builder()
                    .SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.False)
                    .SetTestDeviceIds(deviceIds).build();
                MobileAds.SetRequestConfiguration(requestConfiguration);*/

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(HandleInitCompleteAction);
    }

    private void HandleInitCompleteAction(InitializationStatus initstatus)
    {
        // Callbacks from GoogleMobileAds are not guaranteed to be called on
        // main thread.
        // In this example we use MobileAdsEventExecutor to schedule these calls on
        // the next Update() loop.
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            //RequestBannerAd();
        });
    }

    /*    private AdRequest CreateAdRequest()
        {
            return new AdRequest.Builder().Build();
        }*/
    private AdRequest CreateAdRequest()
    {
        return new AdRequest();
    }


    #endregion

    #region BANNER ADS

    public void RequestBannerAd()
    {
        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = _bannerID;
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Clean up banner before reusing
        if (_bannerView != null)
        {
            _bannerView.Destroy();
        }

        var adPostion = AdPosition.Bottom;
        if (AdsManager.Instance.bannerPostion == AdsManager.AdPosition.Top)
            adPostion = AdPosition.Top;

        // Create a 320x50 banner at top of the screen
        _bannerView = new BannerView(adUnitId, AdSize.Banner, adPostion);

        // Add Event Handlers
        _bannerView.OnBannerAdLoaded += HandleBannerAdLoaded;
        _bannerView.OnBannerAdLoadFailed += HandleBannerAdFailedToLoad;
        _bannerView.OnAdFullScreenContentOpened += HandleBannerAdOpened;
        _bannerView.OnAdFullScreenContentClosed += HandleBannerAdClosed;

        // Load a banner ad
        _bannerView.LoadAd(CreateAdRequest());
    }

    public void DestroyBannerAd()
    {
        if (_bannerView != null)
        {
            _bannerView.Destroy();
        }
    }

    #endregion

    #region INTERSTITIAL ADS

    public void RequestInterstitialAd()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = _interstitialID;
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Clean up interstitial before using it
        if (_interstitialAd != null)
        {
            DestroyInterstitialAd();
        }

        Debug.Log("Loading interstitial ad.");

        // Create our request used to load the ad.
        var adRequest = new AdRequest();

        // Send the request to load the ad.
        InterstitialAd.Load(adUnitId, adRequest, (InterstitialAd ad, LoadAdError error) =>
        {
            // If the operation failed with a reason.
            if (error != null)
            {
                HandleInterstitialFailedToLoad(error);
                Debug.LogError("Interstitial ad failed to load an ad with error : " + error);
                return;
            }

            // If the operation failed for unknown reasons.
            // This is an unexpected error, please report this bug if it happens.
            if (ad == null)
            {
                HandleInterstitialFailedToLoad(error);
                Debug.LogError("Unexpected error: Interstitial load event fired with null ad and null error.");
                return;
            }

            // The operation completed successfully.
            Debug.Log("Interstitial ad loaded with response : " + ad.GetResponseInfo());
            _interstitialAd = ad;

            HandleInterstitialLoaded();

            // Register to ad events to extend functionality.
            // Raised when the ad is estimated to have earned money.
            ad.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };
            // Raised when an impression is recorded for an ad.
            ad.OnAdImpressionRecorded += () =>
            {
                Debug.Log("Interstitial ad recorded an impression.");
            };
            // Raised when a click is recorded for an ad.
            ad.OnAdClicked += () =>
            {
                Debug.Log("Interstitial ad was clicked.");
            };
            // Raised when an ad opened full screen content.
            ad.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("Interstitial ad full screen content opened.");
            };
            // Raised when the ad closed full screen content.
            ad.OnAdFullScreenContentClosed += () =>
            {
                AdsManager.Instance.InterstitialOpened();
                Debug.Log("Interstitial ad full screen content closed.");
            };
            // Raised when the ad failed to open full screen content.
            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                Debug.LogError("Interstitial ad failed to open full screen content with error : "
                    + error);
            };
        });
    }

    public void ShowInterstitialAd()
    {
        if (_interstitialAd.CanShowAd())
        {
            _interstitialAd.Show();
        }
    }

    public void DestroyInterstitialAd()
    {
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
        }
    }

    #endregion

    #region REWARDED ADS

    public void RequestRewardAd()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = _rewardID;
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        string adUnitId = "unexpected_platform";
#endif

        DConsole.Log("AdMob Reward Loading");

        // Clean up the old ad before loading a new one.
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        Debug.Log("Loading rewarded ad.");

        // Create our request used to load the ad.
        var adRequest = new AdRequest();

        // Send the request to load the ad.
        RewardedAd.Load(adUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            // If the operation failed with a reason.
            if (error != null)
            {
                HandleRewardAdFailedToLoad(error);
                Debug.LogError("Rewarded ad failed to load an ad with error : " + error);
                return;
            }
            // If the operation failed for unknown reasons.
            // This is an unexpected error, please report this bug if it happens.
            if (ad == null)
            {
                HandleRewardAdFailedToLoad(error);
                Debug.LogError("Unexpected error: Rewarded load event fired with null ad and null error.");
                return;
            }

            // The operation completed successfully.
            Debug.Log("Rewarded ad loaded with response : " + ad.GetResponseInfo());
            _rewardedAd = ad;

            HandleRewardAdLoaded();

            // Register to ad events to extend functionality.
            // Raised when the ad is estimated to have earned money.
            ad.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };
            // Raised when an impression is recorded for an ad.
            ad.OnAdImpressionRecorded += () =>
            {
                Debug.Log("Rewarded ad recorded an impression.");
            };
            // Raised when a click is recorded for an ad.
            ad.OnAdClicked += () =>
            {
                Debug.Log("Rewarded ad was clicked.");
            };
            // Raised when the ad opened full screen content.
            ad.OnAdFullScreenContentOpened += () =>
            {
                HandleRewardAdOpening();
            };
            // Raised when the ad closed full screen content.
            ad.OnAdFullScreenContentClosed += () =>
            {
                HandleRewardAdClosed();
            };
            // Raised when the ad failed to open full screen content.
            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                HandleRewardAdFailedToShow();
                Debug.LogError("Rewarded ad failed to open full screen content with error : "
                    + error);
            };
        });
    }

    public void ShowRewardedAd()
    {
        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _rewardedAd.Show((Reward reward) =>
            {
                Debug.Log(String.Format("Rewarded ad granted a reward: {0} {1}",
                                        reward.Amount,
                                        reward.Type));

                HandleUserEarnedReward();
            });
        }
        else
        {
            DConsole.Log("Rewarded ad is not ready yet");
        }
    }

    #endregion

    #region Banner callback handlers

    public void HandleBannerAdLoaded()
    {
        DConsole.Log("AdMob Banner Loaded");

        UnityMainThread.wkr.AddJob(() =>
        {
            AdsManager.Instance.BannerAdLoaded();
        });
    }

    public void HandleBannerAdFailedToLoad(LoadAdError loadAdError)
    {
        DConsole.Log("AdMob Banner Failed message: " + loadAdError.GetMessage());

        UnityMainThread.wkr.AddJob(() =>
        {
            AdsManager.Instance.BannerAdFailed();
        });
    }

    public void HandleBannerAdOpened()
    {
        DConsole.Log("AdMob Banner Opened");
    }

    public void HandleBannerAdClosed()
    {
        DConsole.Log("AdMob Banner Closed");
    }

    #endregion

    #region Interstitial callback handlers

    public void HandleInterstitialLoaded()
    {
        DConsole.Log("AdMob Interstitial Loaded");

        AdsManager.Instance.InterstitialLoaded();
    }

    public void HandleInterstitialFailedToLoad(AdError loadAdError)
    {
        DConsole.Log("AdMob Interstitial Failed message: " + loadAdError.GetMessage());

        AdsManager.Instance.InterstitialFailedToLoad();
    }

    public void HandleInterstitialOpened()
    {
        DConsole.Log("AdMob Interstitial Opened");

        AdsManager.Instance.InterstitialOpened();
    }

    public void HandleInterstitialClosed()
    {
        DConsole.Log("AdMob Interstitial Closed");
        AdsManager.Instance.InterstitialOpened();

    }

    #endregion

    #region RewardedAd callback handlers

    public void HandleRewardAdLoaded()
    {
        DConsole.Log("AdMob Reward Loaded");
        AdsManager.Instance.RewardLoaded();
    }

    public void HandleRewardAdFailedToLoad(LoadAdError adError)
    {
        // TODO
        if (adError.GetMessage() == "Network Error")
        {
            AdsManager.Instance.rewardAdFailedReason = AdsManager.AdFailedType.NoInternet;
        }
        else
        {
            AdsManager.Instance.rewardAdFailedReason = AdsManager.AdFailedType.Other;
        }

        DConsole.Log("AdMob Reward Failed Error: " + adError.GetMessage());

        AdsManager.Instance.RewardFailedToLoad();
    }

    public void HandleRewardAdOpening()
    {
        DConsole.Log("AdMob Reward Opening");

        AdsManager.Instance.RewardOpened();
    }

    public void HandleRewardAdFailedToShow()
    {
        DConsole.Log("AdMob Reward Failed To Show: ");
    }

    public void HandleRewardAdClosed()
    {
        DConsole.Log("AdMob Reward Closed");
        AdsManager.Instance.RewardClosed();
    }

    public void HandleUserEarnedReward()
    {
        DConsole.Log("AdMob Reward Earned");
        AdsManager.Instance.RewardEarned();

        //DConsole.Log("HandleRewardedAdRewarded event received for " + amount.ToString() + " " + type);
    }

    #endregion
}
