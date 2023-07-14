using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class OreMining : MonoBehaviour
{
    [Header("Other Objects")]
    private GameObject player;
    public PlayerStats playerStats;
    [SerializeField] private GameObject miningTextPrefab;
    [Space(20f)]

    [Header("Health System")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject healthBarCanvas;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Gradient healthGradient;
    [SerializeField] private Image fillBar;
    public int maxHealth;
    private int currentHealth;
    [SerializeField] private float Angle;
    private bool attacked = false;
    private float attackTimer = 0;


    private void Start()
    {
        canvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) > Angle)
        {
            healthBarCanvas.SetActive(false);
        }
        fillBar.color = healthGradient.Evaluate(healthBar.normalizedValue);
        attackTimer += Time.deltaTime;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            healthBarCanvas.SetActive(true);
            if (attackTimer > 4f && !attacked)
            {
                attacked = true;
                currentHealth -= 10;
                SetHealth();
                playerStats.ore += (int)(10 * playerStats.oreMiningBonusValue);
                if (miningTextPrefab)
                {  
                    ShowMiningText((int)(10 * playerStats.oreMiningBonusValue));
                }
                if (currentHealth <= 0)
                {
                    Destroy(gameObject, 2f);
                }
                attackTimer = 0f;
            }
            if (attackTimer < 4f)
            {
                attacked = false;
            }
        }
    }
    public void SetMaxHealth(int health)
    {
        healthBar.maxValue = health;
        healthBar.value = health;
        fillBar.color = healthGradient.Evaluate(1f);
    }
    public void SetHealth()
    {
        DOTween.To(() => healthBar.value, x => healthBar.value = x, currentHealth, 1.5f);
    }
    private void ShowMiningText(int amount)
    {
        var text = Instantiate(miningTextPrefab, transform.position, Quaternion.identity);
        text.GetComponent<TMP_Text>().text = "+ " + amount.ToString();
    }
}
