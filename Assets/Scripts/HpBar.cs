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
        SetMaxHealth((int)playerStats.shipMaxHealth);
    }
    public void SetMaxHealth(int health)
    {
        hpBar.maxValue = health;
        hpBar.value = health;
        playerStats.shipCurrentHealth = health;
    }
    public void SetHealth(float health)
    {
        playerStats.shipCurrentHealth -= health;
        DOTween.To(() => hpBar.value, x => hpBar.value = x, playerStats.shipCurrentHealth, 1.5f);
    }
    public void RegenerateHealth()
    {
        playerStats.shipCurrentHealth = hpBar.maxValue;
        DOTween.To(() => hpBar.value, x => hpBar.value = x, playerStats.shipCurrentHealth, 1.5f);
    }
}