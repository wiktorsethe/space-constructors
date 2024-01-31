using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class EnemyRanger : MonoBehaviour
{
    [Header("Other Scripts")]
    private ExpBar expBar;
    private GameManager gameManager;
    public PlayerStats playerStats;
    private ObjectPool objPool;
    [Space(20f)]
    [Header("Variables")]
    [SerializeField] private int experience;
    [SerializeField] private int gold;
    private float moveSpeed = 2f;
    private float shootTimer = 0f;
    private float bulletSpeed = 10f;
    private float inTarget = 10;
    [Space(20f)]
    [Header("GameObjects and Rest")]
    private GameObject player;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject miningTextPrefab;
    [SerializeField] private Animator animator;
    [SerializeField] private string target = "Player";
    private bool facingRight = false;
    [SerializeField] private AudioSource arrowSound;
    [Space(20f)]
    [Header("Health System")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject healthBarCanvas;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Gradient healthGradient;
    [SerializeField] private Image fillBar;
    [SerializeField] private int maxHealth;
    private int currentHealth;
    [SerializeField] private float Angle;
    private float hideTimer = 0f;


    private void Start()
    {
        expBar = GameObject.FindObjectOfType(typeof(ExpBar)) as ExpBar;
        gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
        player = GameObject.FindGameObjectWithTag("Player");
        arrowSound = GameObject.Find("Arrow").GetComponent<AudioSource>();
        objPool = GetComponent<ObjectPool>();
        canvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
        healthBarCanvas.SetActive(false);
    }
    private void Update()
    {
        fillBar.color = healthGradient.Evaluate(healthBar.normalizedValue);
        hideTimer += Time.deltaTime;

        Vector3 direction = player.transform.position - transform.position;
        if (direction.x > 0 && facingRight)
        {
            Flip();
        }
        if (direction.x < 0 && !facingRight)
        {
            Flip();
        }

        if (hideTimer > 2f)
        {
            healthBarCanvas.SetActive(false);
        }

        float distance = Vector2.Distance(transform.position, player.transform.position);
        shootTimer += Time.deltaTime;
        if (distance < inTarget)
        {
            if (shootTimer >= 3f)
            {
                Invoke("FireBullet", 0.4f);
                animator.SetTrigger("Play");
                shootTimer = 0f;
            }
        }
        else if (distance >= (inTarget - 1f) && distance < 30f)
        {
            Vector3 vectorToTarget = player.transform.position - transform.position;
            //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime); //ciekawe rzeczy jak weŸmiemy .right
            //transform.Find("EnemyRangerImage").transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }

    }
    void FireBullet()
    {
        GameObject bullet = objPool.GetPooledObject();
        arrowSound.Play();
        bullet.transform.position = firePoint.position;
        bullet.GetComponent<ShootingBullet>().startingPos = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
        bullet.SetActive(true);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.GetComponent<ShootingBullet>().target = target;
        bullet.GetComponent<ShootingBullet>().damage = 5f;


        Vector3 direction = player.transform.position - firePoint.transform.position;
        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, rot - 90);
        rb.velocity = new Vector2(direction.x, direction.y).normalized * bulletSpeed;

        
        /*
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<ShootingBullet>().target = target;
        bullet.GetComponent<ShootingBullet>().damage = 5f;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 bulletVelocity = player.transform.position * 0.5f;
        rb.velocity = bulletVelocity;*/
    }
    public void SetMaxHealth(int health)
    {
        currentHealth = health;
        maxHealth = health;
        healthBar.maxValue = health;
        healthBar.value = health;
        fillBar.color = healthGradient.Evaluate(1f);
    }
    public void SetHealth()
    {
        DOTween.To(() => healthBar.value, x => healthBar.value = x, currentHealth, 1.5f);
    }
    public void CollisionDetected()
    {
        healthBarCanvas.SetActive(true);
        hideTimer = 0f;
        currentHealth -= 10;
        SetHealth();
        if (currentHealth <= 0)
        {
            SetMaxHealth(maxHealth);
            expBar.SetExperience(experience);
            playerStats.gold += gold;
            gameManager.goldEarned += gold;
            gameManager.kills += 1;
            shootTimer = -10f;
            GetComponent<LootBag>().InstantiateLoot(transform.position);
            gameObject.SetActive(false);
        }
        if (miningTextPrefab)
        {
            ShowMiningText(10);
        }
    }
    private void ShowMiningText(int amount)
    {
        var text = Instantiate(miningTextPrefab, transform.position, Quaternion.identity);
        text.GetComponent<TMP_Text>().text = amount.ToString();
    }
    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
    }
}
