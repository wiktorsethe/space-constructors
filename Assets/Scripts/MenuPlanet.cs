using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuPlanet : MonoBehaviour
{
    public PlayerStats playerStats;
    [Header("UI")]
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    [Space(20f)]
    [Header("Text")]
    [SerializeField] private TMP_Text goldText;
    [SerializeField] private TMP_Text screwText;
    [SerializeField] private TMP_Text oreText;

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
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        gameMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        //TimeSpan timeSpan = TimeSpan.FromSeconds(playerStats.bestTime);
        //bestTimeText.text = "Best Time: " + string.Format("{0:00}:{1:00}:{2:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        //mostKillsText.text = "Most Kills: " + playerStats.mostKills.ToString();
        //mostGoldEarnedText.text = "Most Gold Earned: " + playerStats.mostGoldEarned.ToString();
        Time.timeScale = 0f;
    }
}
