using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class EnemyShip : MonoBehaviour
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
    private float inTarget = 8;
    private float shootTimer = 0f;
    private float bulletSpeed = 10f;
    public float moveSpeed = 2f;
    [Space(20f)]
    [Header("GameObjects and Rest")]
    private GameObject player;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject miningTextPrefab;
    [SerializeField] private string target;
    private GameObject[] objectsList;
    private float distance;
    [Space(20f)]
    [Header("Health System")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject healthBarCanvas;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Gradient healthGradient;
    [SerializeField] private Image fillBar;
    [SerializeField] private int maxHealth;
    public int currentHealth;
    [SerializeField] private float Angle;
    private float hideTimer = 0f;
    private void Start()
    {
        expBar = GameObject.FindObjectOfType(typeof(ExpBar)) as ExpBar;
        gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
        objPool = GetComponent<ObjectPool>();
        player = GameObject.FindGameObjectWithTag("Player");
        canvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        SetMaxHealth(maxHealth);
        healthBarCanvas.SetActive(false);
    }
    private void Update()
    {
        fillBar.color = healthGradient.Evaluate(healthBar.normalizedValue);
        hideTimer += Time.deltaTime;
        if (hideTimer > 3f)
        {
            healthBarCanvas.SetActive(false);
        }

        objectsList = GameObject.FindGameObjectsWithTag("Ship");
        float closestDistance = Mathf.Infinity;
        GameObject closestObject = null;

        foreach (GameObject obj in objectsList)
        {
            distance = Vector3.Distance(transform.position, obj.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = obj;
            }
        }
        
        //float distance = Vector2.Distance(transform.position, player.transform.position);
        shootTimer += Time.deltaTime;
        if (distance <= inTarget)
        {
            if (shootTimer < 2f)
            {
                Vector3 vectorToTarget = player.transform.position - transform.position;
                transform.Find("EnemyShipImage").transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget);
            }

            if (shootTimer >= 3f)
            {
                FireBullet();
                shootTimer = 0f;
            }
        }
        else if (distance > inTarget)
        {
            Vector3 vectorToTarget = player.transform.position - transform.position;
            //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime); //ciekawe rzeczy jak weümiemy .right
            transform.Find("EnemyShipImage").transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }


        if (currentHealth <= 0)
        {
            SetMaxHealth(maxHealth);
            moveSpeed = 2f;
            expBar.SetExperience(experience);
            playerStats.gold += gold;
            gameManager.goldEarned += gold;
            gameManager.kills += 1;
            shootTimer = -10f;
            gameObject.SetActive(false);
        }
    }
    void FireBullet()
    {
        GameObject bullet = objPool.GetPooledObject();
        bullet.transform.position = firePoint.position;
        bullet.GetComponent<ShootingBullet>().startingPos = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
        bullet.SetActive(true);
        bullet.GetComponent<ShootingBullet>().target = target;
        bullet.GetComponent<ShootingBullet>().damage = 10;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 bulletVelocity = firePoint.up * bulletSpeed;
        rb.velocity = bulletVelocity;
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
    public void CollisionDetected(int damage)
    {
        healthBarCanvas.SetActive(true);
        hideTimer = 0f;
        currentHealth -= damage;
        SetHealth();
        moveSpeed = 0.5f; // popraw
        StartCoroutine(ChangingSpeed());
        if (miningTextPrefab)
        {
            ShowMiningText(damage);
        }
    }
    private void ShowMiningText(int amount)
    {
        var text = Instantiate(miningTextPrefab, transform.position, Quaternion.identity);
        text.GetComponent<TMP_Text>().text = amount.ToString();
    }
    IEnumerator ChangingSpeed()
    {
        yield return new WaitForSeconds(0.3f);

        while (moveSpeed < 2f)
        {
            moveSpeed += 0.1f;
            yield return new WaitForSeconds(0.1f);

        }
    }
    public void StartPoison()
    {
        StartCoroutine("Poison");
    }
    IEnumerator Poison()
    {
        int t = 0;
        while(t < playerStats.poisonGunDurationValue)
        {
            CollisionDetected((int)playerStats.poisonGunBetweenDamageValue);
            t++;
            yield return new WaitForSeconds(playerStats.poisonGunBetweenAttackSpeedValue);
        }
    }
    public void StartFlame()
    {
        StartCoroutine("Flame");
    }
    IEnumerator Flame()
    {
        float elapsedTime = 0f;
        while (elapsedTime <= playerStats.flameGunDurationValue)
        {
            CollisionDetected((int)playerStats.flameGunBetweenDamageValue);
            yield return new WaitForSeconds(0.5f);
            elapsedTime += 0.5f;
        }
    }
}
