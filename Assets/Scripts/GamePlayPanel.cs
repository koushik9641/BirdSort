

using System;
using Game;
using dotmob;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayPanel : ShowHidable
{
    [SerializeField] private Text _lvlTxt;
    [SerializeField] private GameObject txtTutorial;

    private void Start()
    {
        _lvlTxt.text = $" LEVEL  {LevelManager.Instance.Level.no}";

        if(LevelManager.Instance.Level.no == 1)
        {
            txtTutorial.SetActive(true);
        }
        else
        {
            txtTutorial.SetActive(false);
        }
        //Debug.Log("Level :" + LevelManager.Instance.Level.no);
    }

    public void OnClickUndo()
    {


        /*if (!Advertisements.Instance.IsRewardVideoAvailable())
        {
            SharedUIManager.PopUpPanel.ShowAsInfo("Notice", "Sorry no video ads available.Check your internet connection!");
            return;
        }*/

        SharedUIManager.PopUpPanel.ShowAsConfirmation("Undo Last Move","Do you want watch Video ads to Undo Last Move", success =>
        {
            if(!success)
                return;

            //AdsManager.Instance.onRewarded3Pressed();
          
        });
        
    }

    public void Clickundoonsuccess(){

        LevelManager.Instance.OnClickUndo();

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

        LevelManager.Instance.Addemptyholder();

    }

    public void OnClickRestart()
    {
        GameManager.LoadGame(new LoadGameData
        {
            Level = LevelManager.Instance.Level,
            GameMode = LevelManager.Instance.GameMode,
        },false);
    }

    public void OnClickSkip()
    {
        /*if (!Advertisements.Instance.IsRewardVideoAvailable())
        {
            SharedUIManager.PopUpPanel.ShowAsInfo("Notice", "Sorry no video ads available.Check your internet connection!");
            return;
        }*/

        SharedUIManager.PopUpPanel.ShowAsConfirmation("Skip Level","Do you want watch Video ads to skip this level", success =>
        {
            if(!success)
                return;

            //AdsManager.Instance.onRewarded2Pressed();
          
        });
    }


    public void Clickskiplevelonsuccess(){

        ResourceManager.CompleteLevel(LevelManager.Instance.GameMode, LevelManager.Instance.Level.no);
        UIManager.Instance.LoadNextLevel();

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

    private void Update()
    {
    }
}