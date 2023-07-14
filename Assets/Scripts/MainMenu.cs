using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public PlayerStats playerStats;
    public ShipProgress shipProgress;
    public ShipPartsDatabase shipPartsDB;
    public void Play()
    {
        playerStats.Reset();
        shipProgress.Reset();
        shipPartsDB.Reset();
        PlayerPrefs.SetFloat("BestTimeTimer", 0);
        PlayerPrefs.SetInt("Kills", 0);
        PlayerPrefs.SetInt("GoldEarned", 0);
        SceneManager.LoadScene(1);
    }
}
