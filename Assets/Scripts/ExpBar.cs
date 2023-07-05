using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ExpBar : MonoBehaviour
{
    public Slider expBar;
    public PlayerStats playerStats;
    private HpBar hpBar;
    private void Start()
    {
        GetExperience();
        hpBar = GameObject.FindObjectOfType(typeof(HpBar)) as HpBar;
    }
    private void Update()
    {
        if (expBar.value >= expBar.maxValue)
        {
            playerStats.level += 1;
            StartNewLevel();
        }
    }
    public void GetExperience()
    {
        if (playerStats.level == 1)
        {
            expBar.maxValue = 200;
        }
        else if (playerStats.level == 2)
        {
            expBar.maxValue = 350;
        }
        else if (playerStats.level == 3)
        {
            expBar.maxValue = 600;
        }
        else if (playerStats.level == 4)
        {
            expBar.maxValue = 900;
        }
        else if (playerStats.level == 5)
        {
            expBar.maxValue = 1350;
        }
        else if (playerStats.level == 6)
        {
            expBar.maxValue = 1900;
        }
        else if (playerStats.level == 7)
        {
            expBar.maxValue = 2500;
        }
        else if (playerStats.level == 8)
        {
            expBar.maxValue = 3150;
        }
        else if (playerStats.level == 9)
        {
            expBar.maxValue = 3900;
        }
        else if (playerStats.level == 10)
        {
            expBar.maxValue = 4700;
        }
        else if (playerStats.level == 11)
        {
            expBar.maxValue = 5700;
        }
        else if (playerStats.level == 12)
        {
            expBar.maxValue = 6850;
        }
        else if (playerStats.level == 13)
        {
            expBar.maxValue = 8300;
        }
        else if (playerStats.level == 14)
        {
            expBar.maxValue = 10900;
        }
        else if (playerStats.level == 15)
        {
            expBar.maxValue = 13000;
        }
        expBar.value = playerStats.experience;
    }
    public void SetExperience(int exp)
    {
        playerStats.experience += exp;
        DOTween.To(() => expBar.value, x => expBar.value = x, playerStats.experience, 1.5f);
    }
    public void StartNewLevel()
    {
        expBar.value = 0;
        playerStats.experience = 0;
        hpBar.RegenerateHealth();
    }
}
