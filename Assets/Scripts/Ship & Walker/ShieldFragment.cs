using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldFragment : MonoBehaviour
{
    private HpBar hpBar;
    public PlayerStats playerStats;
    private float timer = 0f;
    private bool isShieldActive = false;
    private void Start()
    {
        hpBar = GameObject.FindObjectOfType(typeof(HpBar)) as HpBar;
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
                isShieldActive = true;
                timer = 0f;
            }
        }
        
    }

}
