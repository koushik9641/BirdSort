using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NiobiumStudios;
using TMPro;

public class CoinManager : Singleton<CoinManager>
{
    public Text coins;
    public GameObject RewardPanel;
    public GameObject DoubleCoin;
    private int amount;
    public GameObject FreeCoinPanel;
    public Button FreeCoinsbutt;
    public TMP_Text rewardCoins;

    void Start()
    {
        coins.text = PlayerPrefs.GetInt("Coins").ToString();
    }

    public void FreeCoinPanel2()
    {
        if (FreeCoinPanel.activeSelf)
            FreeCoinPanel.SetActive(false);
        else
            FreeCoinPanel.SetActive(true);
    }

    // Update is called once per frame
    public void AddCoins()
    {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 10);
            coins.text = PlayerPrefs.GetInt("Coins").ToString();
            Debug.Log("AddCoins....");  
    }

    public void SpinCoins()
    {
        //AdsManager.Instance.ShowReward(() =>
        //{
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 10);
            coins.text = PlayerPrefs.GetInt("Coins").ToString();
            Debug.Log("AddCoins....");
        //});
    }

    public void CoinUpdate()
    {
        coins.text = PlayerPrefs.GetInt("Coins").ToString();
    }

    public void AddCoins200()
    {
        //AdsManager.Instance.ShowReward(() =>
        //{
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 500);
            CoinManager.Instance.CoinUpdate();
            //coins.text = PlayerPrefs.GetInt("Coins").ToString();
            Debug.Log("AddCoins....");
            RewardPanel.SetActive(false);
            DoubleCoin.SetActive(false);
            FreeCoinPanel.SetActive(false);
        //});
    }

    public void Coins200Next()
    {
        rewardCoins.text = "200";
       // AdsManager.Instance.ShowReward(() =>
       // {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 200);
            CoinManager.Instance.CoinUpdate();
            //coins.text = PlayerPrefs.GetInt("Coins").ToString();
            Debug.Log("AddCoins....");
            FreeCoinsbutt.interactable = false;
        //});
    }


    public void FreeCoins()
    {
        if (RewardPanel.activeSelf)
            RewardPanel.SetActive(false);
        else
            RewardPanel.SetActive(true);
    }

    public void DoubleCoinPanel()
    {
        CoinManager.Instance.CoinUpdate();
        if (DoubleCoin.activeSelf)
            DoubleCoin.SetActive(false);
        else
            DoubleCoin.SetActive(true);
    }

    void OnEnable()
    {
        DailyRewards.instance.onClaimPrize += OnClaimPrizeDailyRewards;
    }

    void OnDisable()
    {
        DailyRewards.instance.onClaimPrize -= OnClaimPrizeDailyRewards;
    }

    // this is your integration function. Can be on Start or simply a function to be called
    public void OnClaimPrizeDailyRewards(int day)
    {
        //This returns a Reward object
        Reward myReward = DailyRewards.instance.GetReward(day);

        // And you can access any property
        print(myReward.unit);   // This is your reward Unit name
        print(myReward.reward); // This is your reward count

        var rewardsCount = PlayerPrefs.GetInt("Coins", 0);
        rewardsCount += myReward.reward;

        amount = myReward.reward;

        PlayerPrefs.SetInt("Coins", rewardsCount);
        PlayerPrefs.Save();
        coins.text = PlayerPrefs.GetInt("Coins").ToString();

    }

    public void xcoins()
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + amount);
        PlayerPrefs.Save();
        coins.text = PlayerPrefs.GetInt("Coins").ToString();
    }

}
