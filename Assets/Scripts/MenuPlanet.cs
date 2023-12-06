using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class MenuPlanet : MonoBehaviour
{
    public PlayerStats playerStats;
    public ShipProgress shipProgress;
    [Header("UI")]
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    [Space(20f)]
    [Header("Text")]
    [SerializeField] private TMP_Text goldText;
    [SerializeField] private TMP_Text screwText;
    [SerializeField] private TMP_Text oreText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text killsText;
    [SerializeField] private TMP_Text goldEarnedText;
    [SerializeField] private TMP_Text scoreText;
    [Space(20f)]
    [Header("Objects")]
    [SerializeField] private Animator transition;
    [SerializeField] private GameObject levelLoader;
    private void Update()
    {
        goldText.text = playerStats.gold.ToString();
        screwText.text = playerStats.screw.ToString();
        oreText.text = playerStats.ore.ToString();
    }
    public void PauseMenu()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        gameMenu.SetActive(false);
        gameOverMenu.SetActive(false);
    }
    public void Resume()
    {
        gameMenu.SetActive(true);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Restart()
    {
        Resume();
        playerStats.Reset();
        shipProgress.Reset();
        StartCoroutine("LoadUniverse");
    }
    public void BackToMenu()
    {
        Resume();
        StartCoroutine("LoadMenu");
    }
    public void GameOver()
    {
        gameMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        TimeSpan timeSpan = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("BestTimeTimer"));
        timeText.text = string.Format("{0:00}:{1:00}:{2:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        killsText.text = PlayerPrefs.GetInt("Kills").ToString();
        goldEarnedText.text = PlayerPrefs.GetInt("GoldEarned").ToString();
        scoreText.text = PlayerPrefs.GetInt("Score").ToString();

        Time.timeScale = 0f;
    }
    private IEnumerator LoadMenu()
    {
        levelLoader.SetActive(true);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("main");
    }
    private IEnumerator LoadUniverse()
    {
        levelLoader.SetActive(true);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Universe");
    }
}
