using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingFragment : MonoBehaviour
{
    [Header("Other Scripts")]
    public PlayerStats playerStats;
    private HpBar hpBar;
    [Space(20f)]
    [Header("Variables")]
    private float regen = 0f;
    private float timer = 0f;
    
    private void Start()
    {
        hpBar = GameObject.FindObjectOfType(typeof(HpBar)) as HpBar;
    }
    private void Update()
    {
        regen = playerStats.shipMaxHealth * 0.0005f;
        timer += Time.deltaTime;
        if (timer >= 0.1f)
        {
            hpBar.RegenerateHealthByFragment(regen);
            timer = 0f;
        }
    }
}
