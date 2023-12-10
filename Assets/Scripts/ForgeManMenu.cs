using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ForgeManMenu : MonoBehaviour
{
    public ShipPartsDatabase shipPartsDB;
    [SerializeField] private GameObject changeItemPrefab;
    [SerializeField] private GameObject changeManMenuPanel;
    [SerializeField] private TMP_Text bosspointsTxt;
    private void Start()
    {
        for (int i = 0; i < shipPartsDB.shipParts.Length; i++)
        {
            GameObject obj = Instantiate(changeItemPrefab, changeManMenuPanel.transform);
            obj.GetComponent<ForgeManSlider>().shipPartNumber = i;
            obj.transform.Find("ItemImgPanel").transform.Find("Image").GetComponent<Image>().sprite = shipPartsDB.shipParts[i].image;
            obj.transform.Find("ItemNameTxt").GetComponent<TMP_Text>().text = shipPartsDB.shipParts[i].itemName;
        }
    }
    private void Update()
    {
        bosspointsTxt.text = "Bosspoints: " + PlayerPrefs.GetInt("BossPoints");
    }
}
