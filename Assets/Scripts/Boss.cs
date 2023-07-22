using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class Boss : MonoBehaviour
{
    public ShootingNormalGun[] shootingScripts;
    private float specialActionTimer = 0;
    public PlayerStats playerStats;
    public GameObject bulletPrefab;
    public Transform[] firePoints;
    public float bulletSpeed;
    public string target;
    public float shootTimer = 0;
    private GameObject[] objectsList;
    private float distance;
    private bool isPositionSelected = false;
    private Vector3 dashPosition;
    private GameObject player;
    private bool firstGunsShoot = false;
    private bool secondGunsShoot = false;
    [SerializeField] private GameObject damageTextPrefab;
    [Header("Health System")]
    [SerializeField] private GameObject healthBarCanvas;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Gradient healthGradient;
    [SerializeField] private Image fillBar;
    [SerializeField] private int maxHealth;
    private int currentHealth;
    [SerializeField] private float Angle;
    private ExpBar expBar;
    private GameManager gameManager;
    [SerializeField] private int experience;
    [SerializeField] private int gold;
    private void Start()
    {
        expBar = GameObject.FindObjectOfType(typeof(ExpBar)) as ExpBar;
        gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
        player = GameObject.FindGameObjectWithTag("Player");
        healthBarCanvas = GameObject.Find("BossHPBar");
        healthBar = GameObject.Find("BossHPBar").GetComponent<Slider>();
        fillBar = GameObject.Find("BossHPBar").GetComponentInChildren<Image>();
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
        healthBarCanvas.SetActive(true);
    }
    private void Update()
    {
        fillBar.color = healthGradient.Evaluate(healthBar.normalizedValue);
        specialActionTimer += Time.deltaTime;
        shootTimer += Time.deltaTime;

        if (specialActionTimer >= 3f && specialActionTimer < 7.5f)
        {
            Sequence rotationSequence = DOTween.Sequence();
            rotationSequence.Append(transform.DORotate(new Vector3(0, 0, 360), 4f, RotateMode.FastBeyond360));
            if (shootTimer >= 0.5f)
            {

                FireBullet();
                shootTimer = 0f;
            }
        }
        else if(specialActionTimer >= 7.5f && specialActionTimer < 12f)
        {
            if (!isPositionSelected)
            {
                dashPosition = player.transform.position;
                isPositionSelected = true;
            }
            //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime); //ciekawe rzeczy jak weŸmiemy .right
            //transform.Find("EnemyShipImage").transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget);
            transform.position = Vector2.MoveTowards(transform.position, dashPosition, 5f * Time.deltaTime);
        }
        else if(specialActionTimer >= 12f && specialActionTimer < 12.5f)
        {
            if (!firstGunsShoot)
            {
                FireFirstWave();
                firstGunsShoot = true;
            }
        }
        else if (specialActionTimer >= 12.5f && specialActionTimer < 13f)
        {
            if (!secondGunsShoot)
            {
                FireSecondWave();
                secondGunsShoot = true;
                isPositionSelected = false;
            }
        }
        else if (specialActionTimer >= 13f && specialActionTimer < 17.5f)
        {
            if (!isPositionSelected)
            {
                dashPosition = player.transform.position;
                firstGunsShoot = false;
                secondGunsShoot = false;
                isPositionSelected = true;
            }
            //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime); //ciekawe rzeczy jak weŸmiemy .right
            //transform.Find("EnemyShipImage").transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget);
            transform.position = Vector2.MoveTowards(transform.position, dashPosition, 5f * Time.deltaTime);
        }
        else if (specialActionTimer >= 17.5f && specialActionTimer < 18f)
        {
            if (!firstGunsShoot)
            {
                FireFirstWave();
                firstGunsShoot = true;
            }
        }
        else if (specialActionTimer >= 18f && specialActionTimer < 18.5f)
        {
            if (!secondGunsShoot)
            {
                FireSecondWave();
                secondGunsShoot = true;
            }
        }
        else if(specialActionTimer >= 20f)
        {
            firstGunsShoot = false;
            secondGunsShoot = false;
            isPositionSelected = false;
            specialActionTimer = 0f;
        }
    }
    void FireBullet()
    {
        for(int i=0; i<firePoints.Length; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoints[i].position, firePoints[i].rotation);
            bullet.GetComponent<ShootingBullet>().target = target;
            bullet.GetComponent<ShootingBullet>().damage = 5 * playerStats.normalGunDamageValue;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 bulletVelocity = firePoints[i].up * bulletSpeed;
            rb.velocity = bulletVelocity;
        }

    }
    void FireFirstWave()
    {
        GameObject bullet1 = Instantiate(bulletPrefab, firePoints[0].position, firePoints[0].rotation);
        bullet1.GetComponent<ShootingBullet>().target = target;
        bullet1.GetComponent<ShootingBullet>().damage = 5 * playerStats.normalGunDamageValue;
        Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
        Vector2 bulletVelocity1 = firePoints[0].up * bulletSpeed;
        rb1.velocity = bulletVelocity1;

        GameObject bullet2 = Instantiate(bulletPrefab, firePoints[2].position, firePoints[2].rotation);
        bullet2.GetComponent<ShootingBullet>().target = target;
        bullet2.GetComponent<ShootingBullet>().damage = 5 * playerStats.normalGunDamageValue;
        Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
        Vector2 bulletVelocity2 = firePoints[2].up * bulletSpeed;
        rb2.velocity = bulletVelocity2;
    }
    void FireSecondWave()
    {
        GameObject bullet1 = Instantiate(bulletPrefab, firePoints[4].position, firePoints[4].rotation);
        bullet1.GetComponent<ShootingBullet>().target = target;
        bullet1.GetComponent<ShootingBullet>().damage = 5 * playerStats.normalGunDamageValue;
        Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
        Vector2 bulletVelocity1 = firePoints[4].up * bulletSpeed;
        rb1.velocity = bulletVelocity1;

        GameObject bullet2 = Instantiate(bulletPrefab, firePoints[7].position, firePoints[7].rotation);
        bullet2.GetComponent<ShootingBullet>().target = target;
        bullet2.GetComponent<ShootingBullet>().damage = 5 * playerStats.normalGunDamageValue;
        Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
        Vector2 bulletVelocity2 = firePoints[7].up * bulletSpeed;
        rb2.velocity = bulletVelocity2;
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
            currentHealth -= 10;
            SetHealth();
            if (currentHealth <= 0)
            {
                expBar.SetExperience(experience);
                playerStats.gold += gold;
                gameManager.goldEarned += gold;
                gameManager.kills += 1;
                shootTimer = -10f;
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
}
