using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour, IStoreListener
{
    private static IAPManager instance;
    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    public static string BirdProductID1 = "coins_10000";
    public static string BirdProductID2 = "coins_25000";
    public static string BirdProductID3 = "coins_50000";
    public static string BirdProductID4 = "coins_150000";
    public static string BirdProductID5 = "coins_250000";
    public static string BirdProductID6 = "coins_500000";

    private void Start()
    {
        instance = this;
        InitializePurchasing();
    }

    public void FreeCoins()
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 100);
        CoinManager.Instance.CoinUpdate();
        Debug.Log("AddCoins....");
    }

    private void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(BirdProductID1, ProductType.Consumable);
        builder.AddProduct(BirdProductID2, ProductType.Consumable);
        builder.AddProduct(BirdProductID3, ProductType.Consumable);
        builder.AddProduct(BirdProductID4, ProductType.Consumable);
        builder.AddProduct(BirdProductID5, ProductType.Consumable);
        builder.AddProduct(BirdProductID6, ProductType.Consumable);


        UnityPurchasing.Initialize(this, builder);
    }

    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void BuyConsumable1()
    {
        BuyProductID(BirdProductID1);
    }

    public void BuyConsumable2()
    {
        BuyProductID(BirdProductID2);
    }

    public void BuyConsumable3()
    {
        BuyProductID(BirdProductID3);
    }
    public void BuyConsumable4()
    {
        BuyProductID(BirdProductID4);
    }
    public void BuyConsumable5()
    {
        BuyProductID(BirdProductID5);
    }
    public void BuyConsumable6()
    {
        BuyProductID(BirdProductID6);
    }

    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);

            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (string.Equals(args.purchasedProduct.definition.id, BirdProductID1, System.StringComparison.Ordinal))
        {
            // Handle consumable1 purchase here
            Debug.Log("ProcessPurchase: PASS. Product purchased: 'consumable1'");
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 10000);
            CoinManager.Instance.CoinUpdate();
            Debug.Log("AddCoins....");
        }
        else if (string.Equals(args.purchasedProduct.definition.id, BirdProductID2, System.StringComparison.Ordinal))
        {
            // Handle consumable2 purchase here
            Debug.Log("ProcessPurchase: PASS. Product purchased: 'consumable2'");
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 25000);
            CoinManager.Instance.CoinUpdate();
            Debug.Log("AddCoins....");
        }
        else if (string.Equals(args.purchasedProduct.definition.id, BirdProductID3, System.StringComparison.Ordinal))
        {
            // Handle consumable2 purchase here
            Debug.Log("ProcessPurchase: PASS. Product purchased: 'consumable3'");
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 50000);
            CoinManager.Instance.CoinUpdate();
            Debug.Log("AddCoins....");
        }
        else if (string.Equals(args.purchasedProduct.definition.id, BirdProductID4, System.StringComparison.Ordinal))
        {
            // Handle consumable2 purchase here
            Debug.Log("ProcessPurchase: PASS. Product purchased: 'consumable4'");
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 150000);
            CoinManager.Instance.CoinUpdate();
            Debug.Log("AddCoins....");
        }
        else if (string.Equals(args.purchasedProduct.definition.id, BirdProductID5, System.StringComparison.Ordinal))
        {
            // Handle consumable2 purchase here
            Debug.Log("ProcessPurchase: PASS. Product purchased: 'consumable5'");
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 250000);
            CoinManager.Instance.CoinUpdate();
            Debug.Log("AddCoins....");
        }
        else if (string.Equals(args.purchasedProduct.definition.id, BirdProductID6, System.StringComparison.Ordinal))
        {
            // Handle consumable2 purchase here
            Debug.Log("ProcessPurchase: PASS. Product purchased: 'consumable6'");
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 500000);
            CoinManager.Instance.CoinUpdate();
            Debug.Log("AddCoins....");
        }
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new System.NotImplementedException();
    }
}
