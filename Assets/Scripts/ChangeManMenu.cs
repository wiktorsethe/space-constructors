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
    private void Start()
    {
        for (int i = 0; i < shipPartsDB.shipParts.Length; i++)
        {
            if (shipPartsDB.shipParts[i].isOwned && shipPartsDB.shipParts[i].ownedAmount > 0)
            {
                GameObject obj = Instantiate(changeItemPrefab, changeManMenuPanel.transform);
                obj.GetComponent<ChangeManItem>().shipPartNumber = i;
                obj.transform.Find("ItemImg").GetComponent<Image>().sprite = shipPartsDB.shipParts[i].image;
                obj.transform.Find("ItemNameTxt").GetComponent<TMP_Text>().text = shipPartsDB.shipParts[i].itemName;
                obj.transform.Find("ItemAmountTxt").GetComponent<TMP_Text>().text = shipPartsDB.shipParts[i].ownedAmount.ToString();
            }
        }
    }
}
