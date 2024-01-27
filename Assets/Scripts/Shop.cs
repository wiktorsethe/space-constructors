using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    [Header("Other Scripts")]
    public PlayerStats playerStats;
    public SkinsDatabase skinsDB;
    [Space(20f)]
    [Header("UI")]
    [SerializeField] private Image skinSprite;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject selectButton;
    [SerializeField] private TMP_Text costBuyText;
    [SerializeField] private TMP_Text selectText;
    [SerializeField] private TMP_Text titleText;
    [Space(20f)]
    [Header("Other")]
    private int activeSkinIndex;

    private void Start()
    {
        activeSkinIndex = playerStats.selectedSkin;
        skinSprite.sprite = skinsDB.skins[activeSkinIndex].skinSpriteMain;
        titleText.text = skinsDB.skins[activeSkinIndex].skinTitle;
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
        titleText.text = skinsDB.skins[activeSkinIndex].skinTitle;
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
        titleText.text = skinsDB.skins[activeSkinIndex].skinTitle;
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
