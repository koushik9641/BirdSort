using UnityEngine;
using EasyUI.PickerWheelUI;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;

/*@" <b>Index:</b> " + wheelPiece.Index + "           <b>Label:</b> " + wheelPiece.Label
+ "\n <b>Amount:</b> " + wheelPiece.Amount + "      <b>Chance:</b> " + wheelPiece.Chance + "%"*/

public class Demo : MonoBehaviour
{
    [SerializeField] private Button uiSpinButton;
    [SerializeField] private Text uiSpinButtonText;

    [SerializeField] private PickerWheel pickerWheel;


    //public TextMeshProUGUI timerText;

    bool isShowTimer = false;

    private int amount;
    public TextMeshProUGUI anountCoins;


    public int rewardAmount = 100; // Set to any value you want
    private DateTime lastRewardClaimTime;
    public TextMeshProUGUI timerText;
    public GameObject freeButtom;

    private void Start()
    {
        string lastClaimTimeString = PlayerPrefs.GetString("LastRewardClaimTime", "");
        if (!string.IsNullOrEmpty(lastClaimTimeString))
        {
            lastRewardClaimTime = DateTime.Parse(lastClaimTimeString);
        }
        else
        {
            lastRewardClaimTime = DateTime.Now.AddDays(-1); // Set to a time in the past
            PlayerPrefs.SetString("LastRewardClaimTime", lastRewardClaimTime.ToString());
        }


        uiSpinButton.onClick.AddListener(() => {

            uiSpinButton.interactable = false;
            uiSpinButtonText.text = "Spinning";

            pickerWheel.OnSpinEnd(wheelPiece => {
                Debug.Log(wheelPiece.Amount);

                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + wheelPiece.Amount);
                //PlayerPrefs.SetFloat("startTime", startTime);
                amount = wheelPiece.Amount;
                anountCoins.text = amount.ToString();
                CoinManager.Instance.CoinUpdate();
                uiSpinButton.interactable = true;
                uiSpinButtonText.text = "Spin";
                CoinManager.Instance.DoubleCoinPanel();
            });

            pickerWheel.Spin();

        });
    }




    public void DoubleCoins()
    {
        AdsManager.Instance.ShowReward(() => {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + amount);
            CoinManager.Instance.CoinUpdate();
        });
    }


    public void FreeSpin()
    {
        AdsManager.Instance.ShowReward(() =>
        {
            freeButtom.SetActive(false);
            pickerWheel.Spin();
            pickerWheel.OnSpinEnd(wheelPiece =>
            {
                Debug.Log(wheelPiece.Amount);

                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + wheelPiece.Amount);
                //PlayerPrefs.SetFloat("startTime", startTime);
                amount = wheelPiece.Amount;
                anountCoins.text = amount.ToString();
                CoinManager.Instance.CoinUpdate();
                //uiSpinButton.interactable = true;
                uiSpinButtonText.text = "Spin";
                CoinManager.Instance.DoubleCoinPanel();
            });
        });
    }

    void Update()
    {
        TimeSpan timeSinceLastClaim = DateTime.Now - lastRewardClaimTime;
        if (timeSinceLastClaim.TotalHours >= 24)
        {
            uiSpinButton.interactable = true;
            timerText.gameObject.SetActive(false); // Hide the timer
        }
        else
        {
            uiSpinButton.interactable = false;
            timerText.gameObject.SetActive(true); // Show the timer
            TimeSpan timeUntilNextClaim = TimeSpan.FromHours(24) - timeSinceLastClaim;
            timerText.text = string.Format("Next claim in {0:D2}:{1:D2}:{2:D2}",
                timeUntilNextClaim.Hours, timeUntilNextClaim.Minutes, timeUntilNextClaim.Seconds);
        }
    }


    public void OnClaimButtonClick()
    {
        lastRewardClaimTime = DateTime.Now;
        PlayerPrefs.SetString("LastRewardClaimTime", lastRewardClaimTime.ToString());
    }
}
