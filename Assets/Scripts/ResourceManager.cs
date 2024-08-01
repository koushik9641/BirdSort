using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class ResourceManager : Singleton<ResourceManager>
{
  
    public static event Action<int> CoinsChanged; 
#if IN_APP
    public static event Action<string> ProductPurchased;
    public static event Action<bool> ProductRestored;
#endif

   

    public static bool EnableAds
    {
        get => PrefManager.GetBool(nameof(EnableAds), true);
        set => PrefManager.SetBool(nameof(EnableAds), value);
    }

    public static string NoAdsProductId => GameSettings.Default.InAppSetting.removeAdsId;

    public static int Coins
    {
        get => PrefManager.GetInt(nameof(Coins));
        set
        {
            PrefManager.SetInt(nameof(Coins),value);
            CoinsChanged?.Invoke(value);
        }
    }

    protected override void OnInit()
    {
        base.OnInit();
        InitLevels();
      if (!IAPManager.Instance.IsInitialized())
            {
                IAPManager.Instance.InitializeIAPManager(InitializeResult);
            }
    }



private void InitializeResult(IAPOperationStatus status, string message, List<StoreProduct>
shopProducts)
{
if (status == IAPOperationStatus.Success)
 {
 //IAP was successfully initialized
//loop through all products
 for (int i = 0; i < shopProducts.Count; i++)
 {
 

 if (shopProducts[i].productName == "removead")
 {
//if active variable is true, means that user had bought that product
//so enable access
 if (shopProducts[i].active)
 {
 removead();
 }
}


}
}
else
{
Debug.Log("Error occurred "+ message);
}
}


void removead(){
        Advertisements.Instance.RemoveAds(true);
        EnableAds = false;
    }


public void purchaseremovead(){
    if (!EnableAds)
        {
            return;
        }
    IAPManager.Instance.BuyProduct(ShopProductNames.removead, ProductBoughtCallback);
}


public void ProductBoughtCallback(IAPOperationStatus status, string message, StoreProduct
product)
{
if (status == IAPOperationStatus.Success)
 {
 //each consumable gives coins in this example

 
 if (product.productName == "removead")
 removead();





 }else
 {
 //an error occurred in the buy process, log the message for more details
 Debug.Log("Buy product failed: " + message);
}
}

#if IN_APP




    public static bool AbleToRestore => EnableAds;

    //public Purchaser Purchaser { get; private set; }

  
    private void PurchaserOnRestorePurchased(bool success)
    {
        //if (EnableAds && Purchaser.ItemAlreadyPurchased(NoAdsProductId))
        if (EnableAds )
        {
            EnableAds = false;
            ProductPurchased?.Invoke(NoAdsProductId);
        }
        ProductRestored?.Invoke(success);
    }


    public static void RestorePurchase()
    {
        //Instance.Purchaser.Restore();
    }

    public static void PurchaseNoAds(Action<bool> completed = null)
    {
        if (!EnableAds)
        {
            return;
        }

        /*Instance.Purchaser.BuyProduct(NoAdsProductId, success =>
        {
            if (success)
            {
                EnableAds = false;
            }
            completed?.Invoke(success);
            if (success)
                ProductPurchased?.Invoke(NoAdsProductId);
        });*/
    }
#endif
}


public partial class ResourceManager
{
    [SerializeField]private List<TextAsset> _modeLvlAssets = new List<TextAsset>();

    private readonly Dictionary<GameMode,List<Level>> _modeAndLevels = new Dictionary<GameMode, List<Level>>();

    private void InitLevels()
    {
        for (var i = 0; i < _modeLvlAssets.Count; i++)
        {
            _modeAndLevels.Add((GameMode)i, JsonUtility.FromJson<LevelGroup>(_modeLvlAssets[i].text).ToList());
        }
    }

    public static IEnumerable<Level> GetLevels(GameMode mode)
    {
        return Instance._modeAndLevels[mode];
    }

    public static Level GetLevel(GameMode mode,int no)
    {
        if(no>=Instance._modeAndLevels[mode].Count)
            return new Level();
        return Instance._modeAndLevels[mode][no-1];
    }

    public static bool IsLevelLocked(GameMode mode, int no)
    {
        var completedLevel = GetCompletedLevel(mode);

        return no > completedLevel + 1;
    }

    public static int GetCompletedLevel(GameMode mode)
    {
       return PrefManager.GetInt($"{mode}_Level_Complete");
    }

    public static void CompleteLevel(GameMode mode, int lvl)
    {
        if (GetLevel(mode).no>lvl)
        {
            return;
        }

        PrefManager.SetInt($"{mode}_Level_Complete",lvl);
    }

    public static bool HasLevel(GameMode mode, int lvl) => GetLevels(mode).Count() >= lvl;

    public static Level GetLevel(GameMode mode)
    {
       return GetLevel(mode,PrefManager.GetInt($"{mode}_Level_Complete")+1);
    }
}

public enum GameMode
{
    Easy,Normal,Hard,Expert,All
}