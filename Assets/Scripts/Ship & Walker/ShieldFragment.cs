using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldFragment : MonoBehaviour
{
    private HpBar hpBar;
    public PlayerStats playerStats;
    private float timer = 0f;
    private bool isShieldActive = false;
    [SerializeField] private AudioSource shieldingSound;
    private void Start()
    {
        hpBar = GameObject.FindObjectOfType(typeof(HpBar)) as HpBar;
        shieldingSound = GameObject.Find("Shield").GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(playerStats.shipCurrentShield <= 0)
        {
            timer += Time.deltaTime;
            if (isShieldActive)
            {
                hpBar.DeactivateShield();
                isShieldActive = false;
            }
            if (timer >= 60f)
            {
                hpBar.ActiveShield();
                shieldingSound.Play();
                isShieldActive = true;
                timer = 0f;
            }
        }
        
    }

}
