using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public List<GameObject> bgProducts;
    void Start()
    {
        
    }
    public void OnEnable()
    {
        UpdatePurchasedBgsets();
        SetActiveBg();
    }

    void UpdatePurchasedBgsets()
    {
        // Retrieve the list of purchased background sets
        string purchasedSets = PlayerPrefs.GetString("purchasedBgsets", "");
        HashSet<string> purchasedSetNames = new HashSet<string>(purchasedSets.Split(','));

        // Iterate through all background products
        foreach (GameObject bgProduct in bgProducts)
        {
            // Check if the background product has been purchased
            bool isPurchased = purchasedSetNames.Contains(bgProduct.name);

            // Iterate through child objects
            foreach (Transform child in bgProduct.transform)
            {
                // Disable the child GameObject with name containing "coin" if the parent is purchased
                if (child.name.Contains("coin"))
                {
                    child.gameObject.SetActive(!isPurchased); // Disable if purchased
                }
            }

            // Always disable the "coin" child in "defaultBackground"
            if (bgProduct.name == "defaultBackground")
            {
                foreach (Transform child in bgProduct.transform)
                {
                    if (child.name.Contains("coin"))
                    {
                        child.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
    public void SetActiveBg()
    {
        // Retrieve the current background set from PlayerPrefs
        string currentBgSetName = PlayerPrefs.GetString("bgset", "defaultBackground");

        // Iterate through all background products
        foreach (GameObject bgProduct in bgProducts)
        {
            bool isCurrentBgSet = bgProduct.name == currentBgSetName;

            // Iterate through child objects
            foreach (Transform child in bgProduct.transform)
            {
                if (child.name.Equals("activeicon"))
                {
                    child.gameObject.SetActive(isCurrentBgSet); // Enable for the current set, disable otherwise
                }
            }
        }

        // Disable activeicon on all other bgProducts
        foreach (GameObject bgProduct in bgProducts)
        {
            if (bgProduct.name != currentBgSetName)
            {
                foreach (Transform child in bgProduct.transform)
                {
                    if (child.name.Equals("activeicon"))
                    {
                        child.gameObject.SetActive(false); // Ensure it's disabled for non-current sets
                    }
                }
            }
        }
    }


    void Update()
    {
        
    }
}
