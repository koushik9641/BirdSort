using UnityEngine;
using UnityEngine.UI;

public class GiftProgressBar : MonoBehaviour
{
    public Image splashFillImage;
    public float loadingTime = 0.5f;
    private int buttonClickCount = 0; // Counter for button clicks
    public float fillAmountIncrement = 0.2f; // Amount to increment fill amount per click
    private float savedFillAmount; // Saved fill amount key
    private int adAfterGameCount = 2;

    private void OnEnable()
    {

        int gameCount = PlayerPrefs.GetInt("GamePlayed", 0);
        gameCount++;

        if (adAfterGameCount <= gameCount)
        {
            gameCount = 0;


        }
        PlayerPrefs.SetInt("GamePlayed", gameCount);

        if (PlayerPrefs.HasKey(savedFillAmount.ToString()))
        {
            splashFillImage.fillAmount = PlayerPrefs.GetFloat(savedFillAmount.ToString());
        }

        splashFillImage.fillAmount += fillAmountIncrement;

        if (splashFillImage.fillAmount >= 1)
        {
            Debug.Log("Ok Boss");
            //Helper.Instance.Gift();
            ResetFillAmount();
        }
        PlayerPrefs.SetFloat(savedFillAmount.ToString(), splashFillImage.fillAmount);
        PlayerPrefs.Save();
    }


    public void ButtonClick()
    {
        splashFillImage.fillAmount += fillAmountIncrement; // Increment the fill amount
        if (splashFillImage.fillAmount >= 1)
        {
            Debug.Log("Ok Boss");
            ResetFillAmount();
        }
    }

    public void ResetFillAmount()
    {
        splashFillImage.fillAmount = 0; // Reset the fill amount to 0
        buttonClickCount = 0; // Reset the button click count

        // Save the fill amount to PlayerPrefs
        PlayerPrefs.SetFloat(savedFillAmount.ToString(), splashFillImage.fillAmount);
        PlayerPrefs.Save();
    }
}
