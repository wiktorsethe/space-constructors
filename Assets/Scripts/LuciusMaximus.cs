using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class LuciusMaximus : MonoBehaviour
{
    [Header("Other Scripts")]
    public PlayerStats playerStats;
    private ExpBar expBar;
    private GameManager gameManager;
    [Space(20f)]

    [Header("Health System")]
    [SerializeField] private GameObject healthBarCanvas;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Gradient healthGradient;
    [SerializeField] private Image fillBar;
    [SerializeField] private int maxHealth;
    private int currentHealth;
    [Space(20f)]

    [Header("Other GameObjects")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject damageTextPrefab;
    [SerializeField] private Transform[] firePoints;
    [Space(20f)]

    [Header("Shooting")]
    [SerializeField] private float bulletSpeed;
    [SerializeField] private string target;
    public float firstGunShootTimer = 0;
    public float secondGunShootTimer = 0;
    public float timer = 0;
    public bool isFirstGunUsed = false;
    public bool isSecondGunUsed = false;
    [Space(20f)]

    [Header("Other")] 
    [SerializeField] private int experience;
    [SerializeField] private int gold;

    private void Start()
    {
        expBar = GameObject.FindObjectOfType(typeof(ExpBar)) as ExpBar;
        gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
        healthBarCanvas = GameObject.Find("BossHPBar");
        healthBar = GameObject.Find("BossHPBar").GetComponent<Slider>();
        fillBar = GameObject.Find("BossHPBar").GetComponentInChildren<Image>();
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
    }
    private void Update()
    {
        fillBar.color = healthGradient.Evaluate(healthBar.normalizedValue);
        timer += Time.deltaTime;

        if (!healthBarCanvas.activeSelf)
        {
            healthBarCanvas.SetActive(true);
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            currentHealth -= (int)collision.GetComponent<ShootingBullet>().damage;
            SetHealth();
            if (currentHealth <= 0)
            {
                expBar.SetExperience(experience);
                playerStats.gold += gold;
                gameManager.goldEarned += gold;
                gameManager.kills += 1;
                //shootTimer = -10f;
                //GetComponent<PolygonCollider2D>().enabled = false;
                GetComponent<LootBag>().InstantiateLoot(transform.position);
                gameManager.bounds.DeactivateBariers();
                gameManager.bounds.ActivatePlanets();
                Destroy(gameObject, 2f);
            }
            if (damageTextPrefab)
            {
                ShowDamageText(10);
            }
        }
    }
    private void ShowDamageText(int amount)
    {
        var text = Instantiate(damageTextPrefab, transform.position, Quaternion.identity);
        text.GetComponent<TMP_Text>().text = amount.ToString();
    }
    private void FirstSpecialAttack()
    {
        if (firstGunShootTimer >= 1f)
        {
            FireBullet(0);
            firstGunShootTimer = 0f;
        }
        if(secondGunShootTimer >= 3f)
        {
            FireBullet(1);
            secondGunShootTimer = 0f;
            timer = 0f;
        }
    }
    public void FireBullet(int i)
    {
        /*
        for (int i = 0; i < firePoints.Length; i++)
        {
            
        }
        */
        GameObject bullet = Instantiate(bulletPrefab, firePoints[i].position, firePoints[i].rotation);
        bullet.GetComponent<ShootingBullet>().target = target;
        bullet.GetComponent<ShootingBullet>().damage = 15;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 bulletVelocity = firePoints[i].up * bulletSpeed;
        rb.velocity = bulletVelocity;
    }
}
