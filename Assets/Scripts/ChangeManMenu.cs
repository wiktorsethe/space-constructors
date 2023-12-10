using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeManMenu : MonoBehaviour
{
    public ShipPartsDatabase shipPartsDB;
    [SerializeField] private GameObject changeItemPrefab;
    [SerializeField] private GameObject changeManMenuPanel;
    [SerializeField] private TMP_Text changeCardsAmountText;
    private void Start()
    {
        for (int i = 0; i < shipPartsDB.shipParts.Length; i++)
        {
            if (shipPartsDB.shipParts[i].isOwned && shipPartsDB.shipParts[i].ownedAmount > 0)
            {
                GameObject obj = Instantiate(changeItemPrefab, changeManMenuPanel.transform);
                obj.GetComponent<ChangeManItem>().shipPartNumber = i;
                obj.transform.Find("ItemImgPanel").transform.Find("Image").GetComponent<Image>().sprite = shipPartsDB.shipParts[i].image;
                obj.transform.Find("BackgroundAmount").transform.Find("AmountTxt").GetComponent<TMP_Text>().text = shipPartsDB.shipParts[i].ownedAmount.ToString();
            }
        }
    }
    private void Update()
    {
        changeCardsAmountText.text = "Changepoints: " + PlayerPrefs.GetInt("ChangeCards");
    }
}
