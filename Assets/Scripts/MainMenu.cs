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
    [SerializeField] private GameObject skinsMenu;
    [SerializeField] private GameObject shopMenu;
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
        skinsMenu.SetActive(true);
        shopMenu.SetActive(false);
    }
    public void Shop()
    {
        mainMenu.SetActive(false);
        dailyRewardsMenu.SetActive(false);
        dailyChallengesMenu.SetActive(false);
        skinsMenu.SetActive(false);
        shopMenu.SetActive(true);
    }
    public void Exit()
    {
        mainMenu.SetActive(true);
        dailyRewardsMenu.SetActive(false);
        dailyChallengesMenu.SetActive(false);
        skinsMenu.SetActive(false);
        shopMenu.SetActive(false);
    }
}
