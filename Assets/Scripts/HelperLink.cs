using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HelperLink : MonoBehaviour
{
    public GameObject shopPanel;
    public GameObject spinPanel;
    //public GameObject dailyPanel;
    public GameObject settingPanel;
    public GameObject IAP_Panel;


    //public Text coinsN;

    string subject = "Hey I am playing this awesome new game called 'Sweet Bird Sort', Do give it try and enjoy.";
    string body = "Hey I am playing this awesome new game called 'Sweet Bird Sort', Do give it try and enjoy. " + "https://play.google.com/store/apps/details?id=com.sweetbirdsortpuzzle.spiraapps";

    

    #region  //Links
    public void MoreApp()
    {
        Application.OpenURL("https://play.google.com/store/apps/dev?id=5907824834163781853");
    }

    public void RateApp()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);
    }

    public void Facebook()
    {
        Application.OpenURL("https://www.facebook.com/nexsagames");
    }

    public void Instagram()
    {
        Application.OpenURL("https://www.facebook.com/nexsagames");
    }
    public void Gameosophy()
    {
        Application.OpenURL("https://www.gameosophy.net/");
    }

    #endregion 


    public void IAPPanel()
    {
        if (IAP_Panel.activeSelf)
            IAP_Panel.SetActive(false);
        else
            IAP_Panel.SetActive(true);
    }


    public void Shop()
    {
        if(shopPanel.activeSelf)
            shopPanel.SetActive(false);
        else
            shopPanel.SetActive(true);
    }

    public void Spin()
    {
        if (spinPanel.activeSelf)
            spinPanel.SetActive(false);
        else
            spinPanel.SetActive(true);
    }

    /*public void Daily()
    {
        if (dailyPanel.activeSelf)
            dailyPanel.SetActive(false);
        else
            dailyPanel.SetActive(true);
    }*/
    
 /*   public void Setting()
    {
        PopupManager.Instance.PopupOpen(PopupID.Settings);
    }*/

    public void OnAndroidTextSharingClick()
    {
        //FindObjectOfType<AudioManager>().Play("Enter");
        StartCoroutine(ShareAndroidText());
    }

    IEnumerator ShareAndroidText()
    {
        yield return new WaitForEndOfFrame();
        //execute the below lines if being run on a Android device
        //Reference of AndroidJavaClass class for intent
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        //Reference of AndroidJavaObject class for intent
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
        //call setAction method of the Intent object created
        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
        //set the type of sharing that is happening
        intentObject.Call<AndroidJavaObject>("setType", "text/plain");
        //add data to be passed to the other activity i.e., the data to be sent
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), "TITLE");
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), subject);
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), body);
        //get the current activity
        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
        //start the activity by sending the intent data
        AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share Via");
        currentActivity.Call("startActivity", jChooser);
    }
}
