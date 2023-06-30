using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class HpBar : MonoBehaviour
{
    [Header("Other Scripts")]
    public PlayerStats playerStats;
    [Space(20f)]

    [Header("Slider")]
    [SerializeField] private Slider hpBar;

    private void Start()
    {
        SetMaxHealth((int)playerStats.maxHealth);
    }
    public void SetMaxHealth(int health)
    {
        hpBar.maxValue = health;
        hpBar.value = health;
        playerStats.currentHealth = health;
    }
    public void SetHealth(float health)
    {
        playerStats.currentHealth -= health;
        DOTween.To(() => hpBar.value, x => hpBar.value = x, playerStats.currentHealth, 1.5f);
    }
    public void RegenerateHealth()
    {
        playerStats.currentHealth = hpBar.maxValue;
        DOTween.To(() => hpBar.value, x => hpBar.value = x, playerStats.currentHealth, 1.5f);
    }
}