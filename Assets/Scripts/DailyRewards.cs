using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DailyRewards : MonoBehaviour
{
    [Header("Other Scripts")]
    public PlayerStats playerStats;
    //public SaveManager save;
    //public AdManager adManager;
    [Space(20f)]
    [Header("UI")]
    [SerializeField] private GameObject[] days;
    [SerializeField] private Button claimButton;
    [SerializeField] private GameObject adButton;
    [Space(20f)]
    [Header("Variables")]
    private int currentDay;
    private string lastDate;
    private void Start()
    {
        Invoke("CheckRewards", 1f);
    }
    public void GetReward()
    {
        if (playerStats.dailyRewardsDay == 0)
        {
            if (PlayerPrefs.GetInt("BonusActivated") == 1)
            {
                playerStats.gold += 100;
            }
            else
            {
                playerStats.gold += 50;
            }
        }
        else if (playerStats.dailyRewardsDay == 1)
        {
            if (PlayerPrefs.GetInt("BonusActivated") == 1)
            {
                playerStats.gold += 240;
            }
            else
            {
                playerStats.gold += 120;
            }
        }
        else if (playerStats.dailyRewardsDay == 2)
        {
            if (PlayerPrefs.GetInt("BonusActivated") == 1)
            {
                playerStats.gold += 420;
            }
            else
            {
                playerStats.gold += 210;
            }
        }
        else if (playerStats.dailyRewardsDay == 3)
        {
            if (PlayerPrefs.GetInt("BonusActivated") == 1)
            {
                playerStats.gold += 600;
            }
            else
            {
                playerStats.gold += 300;
            }
        }
        else if (playerStats.dailyRewardsDay == 4)
        {
            if (PlayerPrefs.GetInt("BonusActivated") == 1)
            {
                playerStats.gold += 840;
            }
            else
            {
                playerStats.gold += 420;
            }
        }
        else if (playerStats.dailyRewardsDay == 5)
        {
            if (PlayerPrefs.GetInt("BonusActivated") == 1)
            {
                playerStats.gold += 1100;
            }
            else
            {
                playerStats.gold += 550;
            }
        }
        if (playerStats.dailyRewardsDay != 6)
        {
            playerStats.dailyRewardsDay++;
        }
        else if (playerStats.dailyRewardsDay == 6)
        {
            if (PlayerPrefs.GetInt("BonusActivated") == 1)
            {
                playerStats.gold += 1400;
            }
            else
            {
                playerStats.gold += 700;
            }
            playerStats.dailyRewardsDay = 0;
        }
        currentDay = playerStats.dailyRewardsDay;
        lastDate = DateTime.Now.ToString();
        playerStats.lastDateBonus = lastDate;
        //PlayerPrefs.SetString("lastDate", lastDate);
        if (!playerStats.firstBonus)
        {
            playerStats.firstBonus = true;
        }
        PlayerPrefs.SetInt("BonusActivated", 0);
        //save.LocalSavePlayerStats();
        CheckRewards();
    }
    public void Update()
    {
        currentDay = playerStats.dailyRewardsDay;
        if (playerStats.firstBonus)
        {
            DateTime currentDate = DateTime.Now;
            DateTime lastBonusDate = DateTime.Parse(playerStats.lastDateBonus); //  PlayerPrefs.GetString("lastDate")
            double elapsedHours = (currentDate - lastBonusDate).TotalHours; //zamien na totalhours
            if (elapsedHours >= 24f)
            {
                days[currentDay].transform.Find("TimerTxt").gameObject.SetActive(false);
                claimButton.interactable = true;
                /*
                if (adManager.isReadyIntersitial)
                {
                    adButton.SetActive(true);
                }
                else
                {
                    adButton.SetActive(false);
                }*/
            }
            if (elapsedHours < 24f)
            {
                TimeSpan timeSpan = TimeSpan.FromHours(elapsedHours);
                int hours = 23 - timeSpan.Hours;
                int minutes = 59 - timeSpan.Minutes;
                int seconds = 59 - timeSpan.Seconds;
                days[currentDay].transform.Find("TimerTxt").GetComponent<TMP_Text>().text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
                days[currentDay].transform.Find("TimerTxt").gameObject.SetActive(true);
                claimButton.interactable = false;
                adButton.SetActive(false);
            }
        }
    }
    private void CheckRewards()
    {
        for (int i = 0; i < days.Length; i++)
        {
            var image = days[i].gameObject.GetComponent<Image>();
            var tempColor = image.color;
            tempColor = new Color(0.6f, 0.6f, 0.6f, 1);
            image.color = tempColor;

            var dayText = days[i].transform.Find("DayTxt").GetComponent<TMP_Text>();
            /*
            var dayTextTempColor = dayText.color;
            dayTextTempColor = new Color(0.6f, 0.6f, 0.6f, 1);
            dayText.color = dayTextTempColor;
            */
            var priceText = days[i].transform.Find("PriceTxt").GetComponent<TMP_Text>();
            /*
            var priceTextTempColor = priceText.color;
            priceTextTempColor = new Color(0.6f, 0.6f, 0.6f, 1);
            priceText.color = priceTextTempColor;
            */
            days[i].transform.Find("ActivatedDay").gameObject.SetActive(false);

            days[i].transform.Find("TimerTxt").gameObject.SetActive(false);

            if (i < currentDay)
            {
                days[i].transform.Find("ActivatedDay").gameObject.SetActive(true);
                days[i].transform.Find("DeactivatedDay").gameObject.SetActive(false);
            }
            if (currentDay == 0 && playerStats.firstBonus)
            {
                if (i < 0)
                {
                    days[i].transform.Find("ActivatedDay").gameObject.SetActive(true);
                }
                else if (i == 0)
                {
                    days[i].gameObject.GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f, 1);
                    days[i].transform.Find("DayTxt").GetComponent<TMP_Text>().color = new Color(0.6f, 0.6f, 0.6f, 1);
                    days[i].transform.Find("PriceTxt").GetComponent<TMP_Text>().color = new Color(0.6f, 0.6f, 0.6f, 1);
                    days[i].transform.Find("TimerTxt").gameObject.SetActive(true);
                }
            }
            if (i == currentDay)
            {
                image.color = new Color(1, 1, 1, 1);
                days[i].transform.Find("DeactivatedDay").gameObject.SetActive(false);
                //dayText.color = new Color(0, 0, 0, 1);
                //priceText.color = new Color(0, 0, 0, 1);
            }
            if (PlayerPrefs.GetInt("BonusActivated") == 1)
            {
                days[0].transform.Find("PriceTxt").GetComponent<TMP_Text>().text = "100";
                days[1].transform.Find("PriceTxt").GetComponent<TMP_Text>().text = "240";
                days[2].transform.Find("PriceTxt").GetComponent<TMP_Text>().text = "420";
                days[3].transform.Find("PriceTxt").GetComponent<TMP_Text>().text = "600";
                days[4].transform.Find("PriceTxt").GetComponent<TMP_Text>().text = "840";
                days[5].transform.Find("PriceTxt").GetComponent<TMP_Text>().text = "1100";
                days[6].transform.Find("PriceTxt").GetComponent<TMP_Text>().text = "1400";
            }
            else
            {
                days[0].transform.Find("PriceTxt").GetComponent<TMP_Text>().text = "50";
                days[1].transform.Find("PriceTxt").GetComponent<TMP_Text>().text = "120";
                days[2].transform.Find("PriceTxt").GetComponent<TMP_Text>().text = "210";
                days[3].transform.Find("PriceTxt").GetComponent<TMP_Text>().text = "300";
                days[4].transform.Find("PriceTxt").GetComponent<TMP_Text>().text = "420";
                days[5].transform.Find("PriceTxt").GetComponent<TMP_Text>().text = "550";
                days[6].transform.Find("PriceTxt").GetComponent<TMP_Text>().text = "700";
            }
        }
    }
    /*
    public void BonusAd()
    {
        adManager.ShowDailyBonusAd();
        PlayerPrefs.SetInt("BonusActivated", 1);
        CheckRewards();
    }*/
}
