using System;
using Game;
//#if ADMOB
//using GoogleMobileAds.Api;
//#endif
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using ConsentManager.Common;
using UnityEngine;
//using AppodealStack.Monetization.Api;
//using AppodealStack.Monetization.Common;
//#if UNITY_ADS
//using UnityEngine.Advertisements;

//#endif

// ReSharper disable once HollowTypeName
public partial class AdsManager : MonoBehaviour, IRewardedVideoAdListener
{

    public static AdsManager Instance { get; private set; }


    #region Application keys

#if UNITY_EDITOR && !UNITY_ANDROID && !UNITY_IPHONE
        public static string appKey = "0a1775908e26e0ea880a65e66fe2f84219add8dd0e9775c2";
#elif UNITY_ANDROID
    public static string appKey = "0a1775908e26e0ea880a65e66fe2f84219add8dd0e9775c2";
#elif UNITY_IPHONE
        public static string appKey = "";
#else
    public static string appKey = "0a1775908e26e0ea880a65e66fe2f84219add8dd0e9775c2";
#endif

    #endregion

    //public string appKey;
    public static string Rewardedplacementname = "default";
    public static string Interstitialplacementname = "default";
    bool LevelSkipvideo;
    bool Holdervideo;








    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);
        LevelSkipvideo = false;
        Holdervideo = false;

        AppodealInit();




    }


    public void AppodealInit()
    {
        int adTypes = Appodeal.INTERSTITIAL | Appodeal.BANNER | Appodeal.REWARDED_VIDEO;
        //Appodeal.setAutoCache(Appodeal.INTERSTITIAL, false);
        //Appodeal.setAutoCache(Appodeal.REWARDED_VIDEO, false);
        Appodeal.setRewardedVideoCallbacks(this);
        //Appodeal.SetLogLevel(AppodealLogLevel.Verbose);
        //Appodeal.SetTesting(false);
        //string appKey = "YOUR_APPODEAL_APP_KEY";
        Appodeal.initialize(appKey, adTypes, this);

    }

    public static bool HaveSetupConsent => PrefManager.HasKey(nameof(ConsentActive));

    public static bool ConsentActive
    {
        get => PrefManager.GetBool(nameof(ConsentActive));
        set => PrefManager.SetBool(nameof(ConsentActive), value);
    }

    private void OnDestroy()
    {
        Appodeal.destroy(Appodeal.BANNER);
    }


    public void ShowBanner()
    {
        //Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM);
        Appodeal.show(Appodeal.BANNER_BOTTOM);
    }

    public void HideBanner()
    {
        //Advertisements.Instance.HideBanner();
        Appodeal.hide(Appodeal.BANNER);
    }

    public static void ShowInterstitial()
    {
        //if (Advertisements.Instance.IsInterstitialAvailable()){
        //Advertisements.Instance.ShowInterstitial();
        // }
        if (Appodeal.isLoaded(Appodeal.INTERSTITIAL) && Appodeal.canShow(Appodeal.INTERSTITIAL, Interstitialplacementname))
        {
            Appodeal.show(Appodeal.INTERSTITIAL, Interstitialplacementname);
        }
        else
        {
            Appodeal.cache(Appodeal.INTERSTITIAL);
        }
    }

    /// <summary>
    /// Show rewarded video, assigned from inspector
    /// </summary>
    public void ShowVideoAds()
    {
        //if (Advertisements.Instance.IsRewardVideoAvailable()){
        //Advertisements.Instance.ShowRewardedVideo(Clickonsuccess);
        //}
        if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO) && Appodeal.canShow(Appodeal.REWARDED_VIDEO, Rewardedplacementname))
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO, Rewardedplacementname);
        }
        else
        {
            Appodeal.cache(Appodeal.REWARDED_VIDEO);
        }
    }

    public void Clickonsuccess(bool completed, string advertiser)
    {


    }

    public void onRewarded1Pressed()
    {
        if (Appodeal.canShow(Appodeal.REWARDED_VIDEO, "reward1"))
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO, "reward1");
        }
    }
    public void onRewarded2Pressed()
    {
        if (Appodeal.canShow(Appodeal.REWARDED_VIDEO, "reward2"))
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO, "reward2");
        }
    }
    public void onRewarded3Pressed()
    {
        if (Appodeal.canShow(Appodeal.REWARDED_VIDEO, "reward3"))
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO, "reward3");
        }
    }
    public void onRewarded4Pressed()
    {
        if (Appodeal.canShow(Appodeal.REWARDED_VIDEO, "reward4"))
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO, "reward4");
        }
    }

    void Update()
    {
        if (LevelSkipvideo)
        {
            //do something with rewardAmount and rewardName
            GameObject.Find("GamePlayPanel").GetComponent<GamePlayPanel>().Clickskiplevelonsuccess();
            LevelSkipvideo = false; //don't forget to set flag to false
        }

        if (Holdervideo)
        {
            //do something with rewardAmount and rewardName
            GameObject.Find("GamePlayPanel").GetComponent<GamePlayPanel>().ClickAddholderonsuccess();
            Holdervideo = false; //don't forget to set flag to false
        }
    }
    


    #region Rewarded Video callback handlers

    public void onRewardedVideoLoaded(bool isPrecache)
    {
        //btnShowRewardedVideo.GetComponentInChildren<Text>().text = "SHOW REWARDED VIDEO";
        Debug.Log("onRewardedVideoLoaded");
        Debug.Log($"getPredictedEcpm(): {Appodeal.getPredictedEcpm(Appodeal.REWARDED_VIDEO)}");
    }

    public void onRewardedVideoFailedToLoad()
    {
        Debug.Log("onRewardedVideoFailedToLoad");
    }

    public void onRewardedVideoShowFailed()
    {
        Debug.Log("onRewardedVideoShowFailed");
    }

    public void onRewardedVideoShown()
    {
        Debug.Log("onRewardedVideoShown");
    }

    public void onRewardedVideoClosed(bool finished)
    {
        //btnShowRewardedVideo.GetComponentInChildren<Text>().text = "CACHE REWARDED VIDEO";
        Debug.Log($"onRewardedVideoClosed. Finished - {finished}");
    }

    public void onRewardedVideoFinished(double amount, string name)
    {
        Debug.Log("onRewardedVideoFinished. Reward: " + amount + " " + name);

       /* if (name == "Coins") { GameObject.Find("Canvas").GetComponent<MainMenu.UIManager>().addcoins((int)amount); }
        else if (name == "Second") { GameObject.Find("GamePlayPanel").GetComponent<GamePlayPanel>().AddTimeexaustedfunction(); }*/
        if (name == "LevelSkip") { LevelSkipvideo = true; }
        else if (name == "Holder") { Holdervideo = true; }
        /*else if (name == "UndoMove") { GameObject.Find("GamePlayPanel").GetComponent<GamePlayPanel>().Clickundoonsuccess(); }
        else if (name == "Life") { GameObject.Find("GamePlayPanel").GetComponent<GamePlayPanel>().revivefunction(); }
        else if (name == "Time") { GameObject.Find("GamePlayPanel").GetComponent<GamePlayPanel>().ClickAddtimeonsuccess(); }*/

    }

    public void onRewardedVideoExpired()
    {
        Debug.Log("onRewardedVideoExpired");
    }

    public void onRewardedVideoClicked()
    {
        Debug.Log("onRewardedVideoClicked");
    }

    #endregion



}

