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
    [SerializeField] private Animator healAnimator;
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
        if (timer >= 3f)
        {
            healAnimator.SetTrigger("Play");
            hpBar.RegenerateHealthByFragment(regen);
            timer = 0f;
        }
    }
}
