using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class HpBar : MonoBehaviour
{
    [Header("Other Scripts")]
    public PlayerStats playerStats;
    private ObjectPool[] objPools;
    [Space(20f)]

    [Header("Slider")]
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private Slider hpBar;
    private bool isFlameStarted = false;
    private bool isPoisonStarted = false;
    private GameObject flameParticle;
    private GameObject poisonParticle;
    private GameObject player;

    private void Start()
    {
        SetMaxHealth((int)playerStats.shipMaxHealth);
        objPools = GetComponents<ObjectPool>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (flameParticle != null)
        {
            if (!flameParticle.GetComponent<ParticleSystem>().IsAlive())
            {
                isFlameStarted = false;
                flameParticle.SetActive(false);
            }
        }

        if (poisonParticle != null)
        {
            if (!poisonParticle.GetComponent<ParticleSystem>().IsAlive())
            {
                isPoisonStarted = false;
                poisonParticle.SetActive(false);
            }
        }
    }
    public void SetMaxHealth(int health)
    {
        hpBar.maxValue = health;
        hpBar.value = health;
        //hpText.text = "HP " + playerStats.shipCurrentHealth + "/" + playerStats.shipMaxHealth;
        StartCoroutine(AnimateNumberIteration((int)hpBar.value, (int)playerStats.shipMaxHealth));
        playerStats.shipCurrentHealth = health;
    }
    public void SetHealth(float health)
    {
        playerStats.shipCurrentHealth -= health;
        //hpText.text = "HP " + playerStats.shipCurrentHealth + "/" + playerStats.shipMaxHealth;
        StartCoroutine(AnimateNumberIteration((int)hpBar.value, (int)playerStats.shipCurrentHealth));
        DOTween.To(() => hpBar.value, x => hpBar.value = x, playerStats.shipCurrentHealth, 1.5f);
    }
    public void RegenerateHealth()
    {
        playerStats.shipCurrentHealth = hpBar.maxValue;
        //hpText.text = "HP " + playerStats.shipCurrentHealth + "/" + playerStats.shipMaxHealth;
        StartCoroutine(AnimateNumberIteration((int)hpBar.value, (int)playerStats.shipCurrentHealth));
        DOTween.To(() => hpBar.value, x => hpBar.value = x, playerStats.shipCurrentHealth, 1.5f);
    }
    public void RegenerateHealthByFragment(float health)
    {
        playerStats.shipCurrentHealth += health;
        //hpText.text = "HP " + playerStats.shipCurrentHealth + "/" + playerStats.shipMaxHealth;
        StartCoroutine(AnimateNumberIteration((int)hpBar.value, (int)playerStats.shipCurrentHealth));
        DOTween.To(() => hpBar.value, x => hpBar.value = x, playerStats.shipCurrentHealth, 1.5f);
    }
    private IEnumerator AnimateNumberIteration(int startNumber, int targetNumber)
    {
        float startTime = Time.time;

        while (Time.time - startTime < 1f)
        {
            float t = (Time.time - startTime) / 1f;
            int currentValue = Mathf.RoundToInt(Mathf.Lerp(startNumber, targetNumber, t));
            hpText.text = "HP " + currentValue.ToString() + "/" + playerStats.shipMaxHealth;

            yield return null;
        }

        // Ensure the text shows the final value
        hpText.text = "HP " + targetNumber.ToString() + "/" + playerStats.shipMaxHealth;
    }
    public void StartPoison()
    {
        StartCoroutine("Poison");
    }
    private IEnumerator Poison()
    {
        int t = 0;
        foreach (ObjectPool script in objPools)
        {
            if (script.type == "poisonParticle" && !isPoisonStarted)
            {
                poisonParticle = script.GetPooledObject();
                poisonParticle.transform.parent = player.transform;
                ParticleSystem.MainModule main = poisonParticle.GetComponent<ParticleSystem>().main;
                main.duration = playerStats.poisonGunDurationValue;
                poisonParticle.SetActive(true);
                poisonParticle.transform.position = player.transform.position;
                isPoisonStarted = true;
            }
        }
        while (t < 4)
        {
            SetHealth(1);
            t++;
            yield return new WaitForSeconds(2f);
        }
    }
    public void StartFlame()
    {
        StartCoroutine("Flame");
    }
    private IEnumerator Flame()
    {
        float elapsedTime = 0f;
        foreach (ObjectPool script in objPools)
        {
            if (script.type == "flameParticle" && !isFlameStarted)
            {
                flameParticle = script.GetPooledObject(); //tu cos nie gra
                flameParticle.transform.parent = player.transform;
                ParticleSystem.MainModule main = flameParticle.GetComponent<ParticleSystem>().main;
                main.duration = playerStats.flameGunDurationValue;
                flameParticle.SetActive(true);
                flameParticle.transform.position = player.transform.position;
                isFlameStarted = true;
            }
        }
        while (elapsedTime <= playerStats.flameGunDurationValue)
        {
            SetHealth(1);
            yield return new WaitForSeconds(0.5f);
            elapsedTime += 0.5f;
        }
    }
}