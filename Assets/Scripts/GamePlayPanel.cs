using System;
using Game;
using dotmob;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayPanel : ShowHidable
{
    [SerializeField] private Text _lvlTxt;
    [SerializeField] private GameObject txtTutorial;
    [SerializeField] private GameObject swapPositionInfo;
    [SerializeField] private GameObject dialogPanelswap;
    [SerializeField] private GameObject[] Locks;

    private void Start()
    {
        _lvlTxt.text = $" LEVEL  {LevelManager.Instance.Level.no}";

        if(LevelManager.Instance.Level.no == 1)
        {
            txtTutorial.SetActive(true);
            Locks[0].SetActive(true);
            Locks[1].SetActive(true);
            Locks[2].SetActive(true);
            Locks[3].SetActive(true);
            Locks[4].SetActive(true);

        }
        else
        {
            txtTutorial.SetActive(false);
            Locks[0].SetActive(false);
            Locks[1].SetActive(false);
            Locks[2].SetActive(false);
            Locks[3].SetActive(false);
            Locks[4].SetActive(false);
        }
        //Debug.Log("Level :" + LevelManager.Instance.Level.no);
    }

    public void OnClickUndo()
    {

        Clickundoonsuccess();
        
    }

    public void Clickundoonsuccess(){

        if (PlayerPrefs.GetInt("Coins") > 100)
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - 100);
            CoinManager.Instance.CoinUpdate();
            Debug.Log("AddCoins....");
            LevelManager.Instance.OnClickUndo();

        }
        else Debug.Log("No coins");
    }



    public void OnClickaddholder()
    {


        /*if (!Advertisements.Instance.IsRewardVideoAvailable())
        {
            SharedUIManager.PopUpPanel.ShowAsInfo("Notice", "Sorry no video ads available.Check your internet connection!");
            return;
        }*/

        SharedUIManager.PopUpPanel.ShowAsConfirmation("Add Extra Branch","Do you want watch Video ads to Add Extra Branch", success =>
        {
            if(!success)
                return;

            //dsManager.Instance.onRewarded1Pressed();
          
        });
        
    }

    public void ClickAddholderonsuccess(){

        if (PlayerPrefs.GetInt("Coins") > 200)
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - 200);
            CoinManager.Instance.CoinUpdate();
            Debug.Log("AddCoins....");
            LevelManager.Instance.Addemptyholder();

        }
        else Debug.Log("No coins");

    }

    public void OnClickRestart()
    {
        if (PlayerPrefs.GetInt("Coins") > 50)
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - 50);
            CoinManager.Instance.CoinUpdate();
            Debug.Log("AddCoins....");
            GameManager.LoadGame(new LoadGameData
            {
                Level = LevelManager.Instance.Level,
                GameMode = LevelManager.Instance.GameMode,
            }, false);
        }
        else Debug.Log("No coins");
    }

    public void OnClickSkip()
    {
        Clickskiplevelonsuccess();
        /*if (!Advertisements.Instance.IsRewardVideoAvailable())
        {
            SharedUIManager.PopUpPanel.ShowAsInfo("Notice", "Sorry no video ads available.Check your internet connection!");
            return;
        }*/

        //SharedUIManager.PopUpPanel.ShowAsConfirmation("Skip Level","Do you want watch Video ads to skip this level", success =>
        //{
        //    if(!success)
        //        return;

        //    //AdsManager.Instance.onRewarded2Pressed();
          
        //});
    }


    public void Clickskiplevelonsuccess(){

        if (PlayerPrefs.GetInt("Coins") > 300)
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - 300);
            CoinManager.Instance.CoinUpdate();
            Debug.Log("AddCoins....");
            ResourceManager.CompleteLevel(LevelManager.Instance.GameMode, LevelManager.Instance.Level.no);
            UIManager.Instance.LoadNextLevel();
        }
        else Debug.Log("No coins");

    }

    public void OnClickMenu()
    {
        //SharedUIManager.PopUpPanel.ShowAsConfirmation("PAUSE", "Are you sure want to exit the game?", success =>
        // {
        //     if (!success)
        //         return;

        //     GameManager.LoadScene("MainMenu");
        // });

        SharedUIManager.PausePanel.Show();

    }
    
    public void swapPosBirds()
    {
        if (PlayerPrefs.GetInt("Coins") > 150)
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - 150);
            CoinManager.Instance.CoinUpdate();
            Debug.Log("AddCoins....");
            LevelManager.Instance.IsShuffleOn = true;
            dialogPanelswap.gameObject.SetActive(true);
        }
        else Debug.Log("No coins");



        // Get all instances of BirdSittingPositions in the scene
        // BirdSittingPositions[] allPositions = FindObjectsOfType<BirdSittingPositions>();

        // foreach (BirdSittingPositions position in allPositions)
        // {
        //     Debug.Log("brunch id:" + position.brunchid);

        //     position.swapposBirds(3); // Ensure that swapPosBirds is a public method in BirdSittingPositions

        // }
    }
}