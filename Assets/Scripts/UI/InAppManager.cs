using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using TMPro;
using UnityEngine.UI;
public class InAppManager : MonoBehaviour
{/*
    //[SerializeField] AudioSource buySound;
    public PlayerStats playerData;
    //public SaveManager save;
    public GameObject restoreButton;

    private static IStoreController m_store_controller;
    private static IExtensionProvider m_store_extension_provider;

    // Product's IDs
    private string coin_pack_1 = "1500gold";
    private string coin_pack_2 = "5000gold";
    private string coin_pack_3 = "12000gold";
    private string coin_pack_4 = "25000gold";
    private string coin_pack_5 = "60000gold";
    private string coin_pack_6 = "130000gold";
    private string remove_ads = "noads";
    private string remove_ads_bonus = "noadsbonus";
    /*
    public void InitializePurchasing()
    {
        if (IsInitialized()) { return; }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(coin_pack_1, ProductType.Consumable);
        builder.AddProduct(coin_pack_2, ProductType.Consumable);
        builder.AddProduct(coin_pack_3, ProductType.Consumable);
        builder.AddProduct(coin_pack_4, ProductType.Consumable);
        builder.AddProduct(coin_pack_5, ProductType.Consumable);
        builder.AddProduct(remove_ads, ProductType.Subscription);
        builder.AddProduct(remove_ads_bonus, ProductType.Consumable);
        UnityPurchasing.Initialize(this, builder);
    }
    private void Start()
    {
        if (Application.platform != RuntimePlatform.IPhonePlayer)
        {
            restoreButton.SetActive(false);
        }
    }
    private bool IsInitialized()
    {
        return m_store_controller != null && m_store_extension_provider != null;
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        throw new System.NotImplementedException();
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        throw new System.NotImplementedException();
    }

    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == coin_pack_1)
        {
            //buySound.Play();
            playerData.gold += ((int)product.definition.payout.quantity);
            //Firebase.Analytics.FirebaseAnalytics.LogEvent("JJF_purchase_1500_diamonds");
            //save.SaveGameBothPlayerStats();
        }
        else if (product.definition.id == coin_pack_2)
        {
            //buySound.Play();
            playerData.gold += ((int)product.definition.payout.quantity);
            //Firebase.Analytics.FirebaseAnalytics.LogEvent("JJF_purchase_5000_diamonds");
            //save.SaveGameBothPlayerStats();
        }
        else if (product.definition.id == coin_pack_3)
        {
            //buySound.Play();
            playerData.gold += ((int)product.definition.payout.quantity);
            //Firebase.Analytics.FirebaseAnalytics.LogEvent("JJF_purchase_12000_diamonds");
            //save.SaveGameBothPlayerStats();
        }
        else if (product.definition.id == coin_pack_4)
        {
            //buySound.Play();
            playerData.gold += ((int)product.definition.payout.quantity);
            //Firebase.Analytics.FirebaseAnalytics.LogEvent("JJF_purchase_25000_diamonds");
            //save.SaveGameBothPlayerStats();
        }
        else if (product.definition.id == coin_pack_5)
        {
            //buySound.Play();
            playerData.gold += ((int)product.definition.payout.quantity);
            //Firebase.Analytics.FirebaseAnalytics.LogEvent("JJF_purchase_60000_diamonds");
            //save.SaveGameBothPlayerStats();
        }
        else if (product.definition.id == coin_pack_6)
        {
            //buySound.Play();
            playerData.gold += ((int)product.definition.payout.quantity);
            //Firebase.Analytics.FirebaseAnalytics.LogEvent("JJF_purchase_130000_diamonds");
            //save.SaveGameBothPlayerStats();
        }/*
        else if (product.definition.id == remove_ads)
        {
            playerData.SetAdsStatus(1);
            //Firebase.Analytics.FirebaseAnalytics.LogEvent("JJF_purchase_NoAds");
            save.SaveGameBothPlayerStats();
            subButton.text = "Purchased";
        }
        else if (product.definition.id == remove_ads_bonus)
        {
            playerData.SetAdsStatus(1);
            //Firebase.Analytics.FirebaseAnalytics.LogEvent("JJF_purchase_NoAds1000diam");
            save.SaveGameBothPlayerStats();
            subBonusButton.text = "Purchased";
        }
    }

    public void OnPurchaseFailure(Product product, PurchaseFailureReason reason)
    {
        Debug.Log("Your purchase failed because " + reason);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        throw new System.NotImplementedException();
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        return PurchaseProcessingResult.Complete;
    }
    public void RestorePurchases()
    {
        m_store_extension_provider.GetExtension<IAppleExtensions>().RestoreTransactions(result => {
            if (result)
            {
                Debug.Log("Restore purchases succeeded.");
            }
            else
            {
                Debug.Log("Restore purchases failed.");
            }
        });
    }
    public void CheckNoAdSub()
    {

        try
        {
            var products = CodelessIAPStoreListener.Instance?.StoreController?.products;
            var product = products.WithID("noadsjumpjumpfaster");
            var subscriptionManager = new SubscriptionManager(product, null);
            var isSubscribed = subscriptionManager.getSubscriptionInfo()?.isSubscribed();
            if (isSubscribed != Result.True)
            {
                //CurrentAds.NoAdsActive = true;
                //playerData.SetAdsStatus(0);
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }

    }*/
}
