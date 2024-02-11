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
    [SerializeField] private AudioSource healingSound;

    private void Start()
    {
        hpBar = GameObject.FindObjectOfType(typeof(HpBar)) as HpBar;
        healingSound = GameObject.Find("Heal").GetComponent<AudioSource>();
    }
    private void Update()
    {
        regen = playerStats.shipMaxHealth * 0.01f;
        timer += Time.deltaTime;
        if (timer >= 3f && playerStats.shipCurrentHealth < playerStats.shipMaxHealth)
        {
            healAnimator.SetTrigger("Play");
            healingSound.Play();
            hpBar.RegenerateHealthByFragment(regen);
            timer = 0f;
        }
    }
}
