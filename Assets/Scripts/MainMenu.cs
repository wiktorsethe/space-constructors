using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Other Scripts")]
    public PlayerStats playerStats;
    public ShipProgress shipProgress;
    public ShipPartsDatabase shipPartsDB;
    [Space(20f)]
    [Header("UI")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject dailyRewardsMenu;
    [SerializeField] private GameObject dailyChallengesMenu;
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private GameObject optionsMenu;
    public void Play()
    {
        playerStats.Reset();
        shipProgress.Reset();
        shipPartsDB.Reset();
        PlayerPrefs.SetFloat("BestTimeTimer", 0);
        PlayerPrefs.SetInt("Kills", 0);
        PlayerPrefs.SetInt("GoldEarned", 0);
        PlayerPrefs.DeleteKey("FirstBoss");
        SceneManager.LoadScene(1);
    }
    public void Skins()
    {
        mainMenu.SetActive(false);
        dailyRewardsMenu.SetActive(false);
        dailyChallengesMenu.SetActive(false);
        optionsMenu.SetActive(true);
        shopMenu.SetActive(false);
    }
    public void Shop()
    {
        mainMenu.SetActive(false);
        dailyRewardsMenu.SetActive(false);
        dailyChallengesMenu.SetActive(false);
        optionsMenu.SetActive(false);
        shopMenu.SetActive(true);
    }
    public void DailyRewards()
    {
        mainMenu.SetActive(false);
        dailyRewardsMenu.SetActive(true);
        dailyChallengesMenu.SetActive(false);
        optionsMenu.SetActive(false);
        shopMenu.SetActive(false);
    }
    public void DailyChallenges()
    {
        mainMenu.SetActive(false);
        dailyRewardsMenu.SetActive(false);
        dailyChallengesMenu.SetActive(true);
        optionsMenu.SetActive(false);
        shopMenu.SetActive(false);
    }
    public void Options()
    {
        mainMenu.SetActive(false);
        dailyRewardsMenu.SetActive(false);
        dailyChallengesMenu.SetActive(false);
        optionsMenu.SetActive(true);
        shopMenu.SetActive(false);
    }
    public void Exit()
    {
        mainMenu.SetActive(true);
        dailyRewardsMenu.SetActive(false);
        dailyChallengesMenu.SetActive(false);
        optionsMenu.SetActive(false);
        shopMenu.SetActive(false);
    }
}
