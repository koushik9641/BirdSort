

using System;
using System.Collections;
using dotmob;
using UnityEngine;

public class Splash : MonoBehaviour
{
    private void Start()
    {
        /*if ((!AdsManager.HaveSetupConsent)||(Advertisements.Instance.UserConsentWasSet()==false))
        {
            SharedUIManager.ConsentPanel.Show();
            yield return new WaitUntil(() => !SharedUIManager.ConsentPanel.Showing);
        }*/

        GameManager.LoadScene("MainMenu");
    }
}