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
    [SerializeField] private AudioSource buttonSound;
    [Space(20f)]
    [Header("Rest")]
    [SerializeField] private Animator menuAnimation;
    [SerializeField] private Animator transition;
    [SerializeField] private GameObject levelLoader;

    private void Start()
    {
        menuAnimation.Rebind();
        menuAnimation.Play("MenuAnim");
    }
    public void Play()
    {
        playerStats.Reset();
        shipProgress.Reset();
        shipPartsDB.Reset();
        PlayerPrefs.SetFloat("BestTimeTimer", 0);
        PlayerPrefs.SetInt("Kills", 0);
        PlayerPrefs.SetInt("GoldEarned", 0);
        PlayerPrefs.DeleteKey("FirstBoss");
        buttonSound.Play();
        StartCoroutine("LoadLevel");
    }
    public void Skins()
    {
        mainMenu.SetActive(false);
        dailyRewardsMenu.SetActive(false);
        dailyChallengesMenu.SetActive(false);
        optionsMenu.SetActive(true);
        shopMenu.SetActive(false);
        buttonSound.Play();
    }
    public void Shop()
    {
        mainMenu.SetActive(false);
        dailyRewardsMenu.SetActive(false);
        dailyChallengesMenu.SetActive(false);
        optionsMenu.SetActive(false);
        shopMenu.SetActive(true);
        buttonSound.Play();
    }
    public void DailyRewards()
    {
        mainMenu.SetActive(false);
        dailyRewardsMenu.SetActive(true);
        dailyChallengesMenu.SetActive(false);
        optionsMenu.SetActive(false);
        shopMenu.SetActive(false);
        buttonSound.Play();
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
        buttonSound.Play();
    }
    public void Exit()
    {
        mainMenu.SetActive(true);
        dailyRewardsMenu.SetActive(false);
        dailyChallengesMenu.SetActive(false);
        optionsMenu.SetActive(false);
        shopMenu.SetActive(false);
        buttonSound.Play();
    }
    private IEnumerator LoadLevel()
    {
        levelLoader.SetActive(true);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
}
