using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ChangeManItem : MonoBehaviour
{
    public ShipPartsDatabase shipPartsDB;
    [SerializeField] public int shipPartNumber;
    [SerializeField] public TMP_Text amountText;
    private void Start()
    {
        amountText.text = shipPartsDB.shipParts[shipPartNumber].ownedAmount.ToString();
    }
    public void Minus()
    {
        if(shipPartsDB.shipParts[shipPartNumber].ownedAmount > 0)
        {
            int changeCards = PlayerPrefs.GetInt("ChangeCards");
            PlayerPrefs.SetInt("ChangeCards", changeCards += 1);

            shipPartsDB.shipParts[shipPartNumber].ownedAmount--;
            amountText.text = shipPartsDB.shipParts[shipPartNumber].ownedAmount.ToString();
        }
    }
    public void Plus()
    {
        int changeCards = PlayerPrefs.GetInt("ChangeCards");
        if (changeCards > 0)
        {
            PlayerPrefs.SetInt("ChangeCards", changeCards -= 1);

            shipPartsDB.shipParts[shipPartNumber].ownedAmount++;
            amountText.text = shipPartsDB.shipParts[shipPartNumber].ownedAmount.ToString();
        }
    }
}
