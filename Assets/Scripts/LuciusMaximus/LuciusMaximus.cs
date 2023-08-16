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
    private ObjectPool objPool;
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
    [SerializeField] private GameObject damageTextPrefab;
    [SerializeField] private Transform[] firePoints;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject destroyParticles;
    [Space(20f)]

    [Header("Shooting")]
    [SerializeField] private float bulletSpeed;
    [SerializeField] private string target;
    public bool isFirstGunUsed = false;
    public bool isSecondGunUsed = false;
    [Space(20f)]

    [Header("Other")] 
    [SerializeField] private int experience;
    [SerializeField] private int gold;
    public Vector2 startingPos;
    private bool isStartingPosSaved = false;
    private void Start()
    {
        expBar = GameObject.FindObjectOfType(typeof(ExpBar)) as ExpBar;
        gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
        healthBarCanvas = GameObject.Find("BossHPBar");
        objPool = GetComponent<ObjectPool>();
        healthBar = GameObject.Find("BossHPBar").GetComponent<Slider>();
        fillBar = GameObject.Find("BossHPBar").GetComponentInChildren<Image>();
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
    }
    private void Update()
    {
        if (!isStartingPosSaved)
        {
            startingPos = transform.position;
            isStartingPosSaved = true;
        }
        fillBar.color = healthGradient.Evaluate(healthBar.normalizedValue);

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
                GetComponent<CircleCollider2D>().enabled = false;
                animator.SetTrigger("Death");
                Invoke("DestroyParticles", 1.9f);
                GetComponent<LootBag>().InstantiateLoot(transform.position);
                gameManager.bounds.DeactivateBariers();
                gameManager.bounds.ActivatePlanets();
                Destroy(gameObject, 2f);
            }
            if (damageTextPrefab)
            {
                ShowDamageText((int)collision.GetComponent<ShootingBullet>().damage);
            }
        }
    }
    private void ShowDamageText(int amount)
    {
        var text = Instantiate(damageTextPrefab, transform.position, Quaternion.identity);
        text.GetComponent<TMP_Text>().text = amount.ToString();
    }
    public void FireBullet(int i)
    {
        GameObject bullet = objPool.GetPooledObject();
        bullet.transform.position = firePoints[i].position;
        bullet.GetComponent<ShootingBullet>().startingPos = firePoints[i].position;
        bullet.transform.rotation = firePoints[i].rotation;
        bullet.SetActive(true);
        bullet.GetComponent<ShootingBullet>().target = target;
        bullet.GetComponent<ShootingBullet>().damage = 5;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 bulletVelocity = firePoints[i].up * bulletSpeed;
        rb.velocity = bulletVelocity;
    }
    private void DestroyParticles()
    {
        Instantiate(destroyParticles, transform.position, Quaternion.identity);
    }
}
