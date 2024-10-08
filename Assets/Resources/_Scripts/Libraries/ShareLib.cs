﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class ShareLib : Singleton<ShareLib>
{
    private bool isSharing = false;

    private Alert.ButtonDelegate alertCommonButtonDelegate;

    //public static ShareLib instance;

    /*void Awake()
    {
        if (null == instance)
            instance = this;
        else if (instance != this)
            Destroy(this);
    }*/

    public bool CheckToShowRatePanel(Alert.CallbackDelegate PauseGame = null, Alert.ButtonDelegate CommonCallback = null)
    {
        int gamePlayed = PlayerPrefs.GetInt("gamePlayed", 0);
        
        if (gamePlayed == -2)
            return false;

        if (gamePlayed >= Config.gameNoToShowRatePanel)
        {
            PlayerPrefs.SetInt("gamePlayed", 1);

            //StartCoroutine(ShowRatePanel(PauseGame));

            alertCommonButtonDelegate = CommonCallback;
            var buttons = new Dictionary<string, Alert.ButtonDelegate>() {
                {Config.rateNowButtonLabel, ShareLib.Instance.OnClickRateButtonAlert },
                {Config.rateLaterButtonLabel, CommonCallback }
            };

            new Alert(Config.rateAlertTitle, Config.rateDescription, 1, PauseGame, buttons);

            return true;
        }
        else
            PlayerPrefs.SetInt("gamePlayed", gamePlayed + 1);

        return false;
    }

    /*IEnumerator ShowRatePanel(Alert.CallbackDelegate PauseGame)
    {
        yield return new WaitForSeconds(1f);

        var buttons = new Dictionary<string, Alert.ButtonDelegate>() {
                {"Rate Now", ShareLib.instance.OnClickRateButtonAlert },
                {"Later", null }
            };

        Alert.instance.ShowAlert("Rate Us", "Please support us by Rating 5 Star!", true, PauseGame, buttons);
    }*/

    private void OnClickRateButtonAlert()
    {
        alertCommonButtonDelegate?.Invoke();
        PlayerPrefs.SetInt("gamePlayed", -2);
        DoRateUs("Rate Panel");
    }

    public void PrivacyPolicy()
    {
        AudioManager.Click();
        Application.OpenURL(Config.appPrivacyLink);
    }

    public void DoRateUs(string from = "Unknown")
    {
        AudioManager.Click();
        //FirebaseInit.LogEvent(AnalyticsTags.RATE_US, "From", from);
        Application.OpenURL(Config.appLink);
    }

    public void DoShare(string from = "Unknown")
    {
        if (!isSharing)
        {
            AudioManager.Click();
            StartCoroutine(SocialShare());
            //FirebaseInit.LogEvent(AnalyticsTags.SHARE, "From", from);
        }
    }

    IEnumerator FetchShareImage()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, Config.shareImageName);
        if (filePath.Contains("://") || filePath.Contains(":///"))
        {
            UnityWebRequest www = UnityWebRequest.Get(filePath);
            yield return www.SendWebRequest();

            File.WriteAllBytes(Application.persistentDataPath + "/" + Config.shareImageName, www.downloadHandler.data);
        }
        else
            File.WriteAllBytes(Application.persistentDataPath + "/" + Config.shareImageName, File.ReadAllBytes(filePath));
    }

    private IEnumerator SocialShare()
    {
        isSharing = true;
        yield return new WaitForEndOfFrame();

        String filePath = Application.persistentDataPath + "/" + Config.shareImageName;

        if (!File.Exists(filePath))
            yield return StartCoroutine(FetchShareImage());
        
        //new NativeShare().AddFile(filePath).SetSubject(Config.shareSubject).SetText(Config.shareBody).Share();

        isSharing = false;

        // Share on WhatsApp only, if installed(Android only);
        /*if( NativeShare.TargetExists( "com.whatsapp" ) )
		    new NativeShare().AddFile( filePath ).SetText( "Hello world!" ).SetTarget( "com.whatsapp" ).Share();*/
    }
}
