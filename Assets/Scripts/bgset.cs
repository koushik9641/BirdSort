using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MainMenu;

public class bgset : MonoBehaviour
{
    public TextMeshProUGUI coinValue;
    void Start()
    {
        
    }
    public void buyBgset()
    {
        int currentCoins = PlayerPrefs.GetInt("coinsValue", 2000);

        if (int.TryParse(coinValue.text, out int cost))
        {
            if (currentCoins >= cost)
            {
                // Retrieve the list of purchased background sets
                string purchasedSets = PlayerPrefs.GetString("purchasedBgsets", "");
                HashSet<string> purchasedSetNames = new HashSet<string>(purchasedSets.Split(','));

                // Check if this background set has already been purchased
                if (purchasedSetNames.Contains(gameObject.name))
                {
                    Debug.Log("This background set has already been purchased.");
                    return; // Exit the method if already purchased
                }

                // Proceed with the purchase
                currentCoins -= cost;
                PlayerPrefs.SetInt("coinsValue", currentCoins);
                UIManager.Instance.updateCoinvalue();

                // Update the list of purchased background sets
                if (!string.IsNullOrEmpty(purchasedSets))
                {
                    purchasedSets += ",";
                }
                purchasedSets += gameObject.name;
                PlayerPrefs.SetString("purchasedBgsets", purchasedSets);

                // Disable the "coin" child GameObject of this background set
                foreach (Transform child in transform)
                {
                    if (child.name.Contains("coin"))
                    {
                        child.gameObject.SetActive(false);
                    }
                    if (child.name.Equals("activeicon"))
                    {
                        child.gameObject.SetActive(true); // Enable the "activeicon"
                    }
                }

                // Optionally set the purchased background set name
                PlayerPrefs.SetString("bgset", gameObject.name);
                // Update the UI to reflect the current background selection
                ShopManager shopManager = FindObjectOfType<ShopManager>();
                if (shopManager != null)
                {
                    shopManager.SetActiveBg();
                }
            }
            else
            {
                Debug.Log("Not enough coins to buy this background set.");
            }
        }
        else
        {
            Debug.Log("Failed to parse the cost from the coinValue text.");
        }
    }
    public void SetCurrentBg()
    {
        // Retrieve the list of purchased background sets
        string purchasedSets = PlayerPrefs.GetString("purchasedBgsets", "");
        HashSet<string> purchasedSetNames = new HashSet<string>(purchasedSets.Split(','));

        // Check if this background set has been purchased
        if (purchasedSetNames.Contains(gameObject.name))
        {
            
            // Enable the "activeicon" for this background set
            foreach (Transform child in transform)
            {
                if (child.name.Equals("activeicon"))
                {
                    child.gameObject.SetActive(true);
                    // Optionally set the purchased background set name
                    PlayerPrefs.SetString("bgset", gameObject.name);
                    // Update the UI to reflect the current background selection
                    ShopManager shopManager = FindObjectOfType<ShopManager>();
                    if (shopManager != null)
                    {
                        shopManager.SetActiveBg();
                    }
                }
            }
        }
        else
        {
            // Optionally handle the case where this background set is not purchased
            foreach (Transform child in transform)
            {
                if (child.name.Equals("activeicon"))
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }


}
