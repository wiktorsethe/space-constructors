using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public PlayerStats playerStats;
    public SkinsDatabase skinsDB;
    public Image skinSprite;
    private int activeSkinIndex;
    public GameObject buyButton;
    public GameObject selectButton;
    public TMP_Text costBuyText;
    public TMP_Text selectText;
    private void Start()
    {
        activeSkinIndex = playerStats.selectedSkin;
        skinSprite.sprite = skinsDB.skins[activeSkinIndex].skinSpriteMain;
    }
    private void Update()
    {
        if (skinsDB.skins[activeSkinIndex].isPurchased)
        {
            buyButton.SetActive(false);
            selectButton.SetActive(true);
        }
        else
        {
            costBuyText.text = skinsDB.skins[activeSkinIndex].cost.ToString();
            buyButton.SetActive(true);
            selectButton.SetActive(false);
        }

        if (activeSkinIndex == playerStats.selectedSkin)
        {
            selectText.text = "SELECTED";
        }
        else
        {
            selectText.text = "SELECT";
        }
    }
    public void NextIndex()
    {
        if (activeSkinIndex == skinsDB.skins.Length - 1)
        {
            activeSkinIndex = 0;
        }
        else
        {
            activeSkinIndex++;
        }
        skinSprite.sprite = skinsDB.skins[activeSkinIndex].skinSpriteMain;
    }
    public void PrevIndex()
    {
        if (activeSkinIndex == 0)
        {
            activeSkinIndex = skinsDB.skins.Length - 1;
        }
        else
        {
            activeSkinIndex--;
        }
        skinSprite.sprite = skinsDB.skins[activeSkinIndex].skinSpriteMain;
    }
    public void BuySkin()
    {
        if (skinsDB.skins[activeSkinIndex].cost < playerStats.gold)
        {
            playerStats.selectedSkin = activeSkinIndex;
            playerStats.gold -= skinsDB.skins[activeSkinIndex].cost;
            skinsDB.skins[activeSkinIndex].isPurchased = true;
            //save.SaveGameBothPlayerStats();
            //save.SaveGameBothFishesDatabase();
        }
    }
    public void SelectSkin()
    {
        if (skinsDB.skins[activeSkinIndex].isPurchased)
        {
            playerStats.selectedSkin = activeSkinIndex;
            //save.LocalSavePlayerStats();
        }
    }
}
