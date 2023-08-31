using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class MadMenesHealingPillar : MonoBehaviour
{
    public PlayerStats playerStats;
    [SerializeField] private GameObject damageTextPrefab;
    private Camera cam;
    [Header("Health System")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject healthBarCanvas;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Gradient healthGradient;
    [SerializeField] private Image fillBar;
    [SerializeField] private int maxHealth;
    public int currentHealth;
    private bool isDeath = false;
    private float hideTimer = 0f;
    [Space(20f)]
    [Header("Particles and animator staff")]
    private bool isFlameStarted = false;
    private bool isPoisonStarted = false;
    private GameObject flameParticle;
    private GameObject poisonParticle;
    private ObjectPool[] objPools;
    private LineRenderer lineRenderer;
    private GameObject boss;
    private void Awake()
    {
        objPools = GetComponents<ObjectPool>();
        SetMaxHealth(maxHealth);
        healthBarCanvas.SetActive(false);
        lineRenderer = GetComponent<LineRenderer>();
        cam = Camera.main;
        float cameraHeight = cam.orthographicSize * 0.25f;
        float cameraWidth = cameraHeight * cam.aspect;

        float scaleX = cameraWidth / transform.localScale.x;
        float scaleY = cameraHeight / transform.localScale.y;

        float objectScale = Mathf.Min(scaleX, scaleY);
        transform.localScale = new Vector3(objectScale, objectScale, 1f);

        boss = GameObject.Find("MadMenes(Clone)");
    }
    private void Update()
    {
        fillBar.color = healthGradient.Evaluate(healthBar.normalizedValue);
        hideTimer += Time.deltaTime;
        if (hideTimer > 3f)
        {
            healthBarCanvas.SetActive(false);
        }

        if (currentHealth <= 0)
        {
            SetMaxHealth(maxHealth);
            gameObject.SetActive(false);
        }


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

        if (currentHealth <= 0 && !isDeath)
        {
            isDeath = true;
            GetComponent<CircleCollider2D>().enabled = false;
            Invoke("DestroyParticles", 1.9f);
            Destroy(gameObject, 2f);
            foreach (ObjectPool script in objPools)
            {
                for (int i = 0; i < script.pooledObjects.Count; i++)
                {
                    Destroy(script.pooledObjects[i].gameObject);
                }
            }
            PlayerPrefs.DeleteKey("HealingPillar");
        }
    }/*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<ShootingBullet>().type == "PoisonBullet")
        {
            StartPoison();
        }
        if (collision.transform.GetComponent<ShootingBullet>().type == "NormalBullet" || collision.transform.GetComponent<ShootingBullet>().type == "HomingBullet" || collision.transform.GetComponent<ShootingBullet>().type == "StunningBullet")
        {
            CollisionDetected((int)collision.transform.GetComponent<ShootingBullet>().damage);
        }
        if (collision.transform.GetComponent<ShootingBullet>().type == "FlameBullet")
        {
            StartFlame();
        }
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<ShootingBullet>().type == "PoisonBullet")
        {
            StartPoison();
        }
        if (collision.transform.GetComponent<ShootingBullet>().type == "NormalBullet" || collision.transform.GetComponent<ShootingBullet>().type == "HomingBullet" || collision.transform.GetComponent<ShootingBullet>().type == "StunningBullet")
        {
            CollisionDetected((int)collision.transform.GetComponent<ShootingBullet>().damage);
        }
        if (collision.transform.GetComponent<ShootingBullet>().type == "FlameBullet")
        {
            StartFlame();
        }
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
    public void HealBoss()
    {
        boss.GetComponent<MadMenes>().currentHealth += 1;
        boss.GetComponent<MadMenes>().SetHealth();
    }
    private void CollisionDetected(int damage)
    {
        healthBarCanvas.SetActive(true);
        hideTimer = 0f;
        currentHealth -= damage;
        SetHealth();
        if (damageTextPrefab)
        {
            ShowDamageText(damage);
        }
    }
    private void ShowDamageText(int amount)
    {
        var text = Instantiate(damageTextPrefab, transform.position, Quaternion.identity);
        text.GetComponent<TMP_Text>().text = amount.ToString();
    }
    public void StartPoison()
    {
        if (!isPoisonStarted)
        {
            isPoisonStarted = true;
            StartCoroutine("Poison");

        }
    }
    IEnumerator Poison()
    {
        float elapsedTime = 0f;
        foreach (ObjectPool script in objPools)
        {
            if (script.type == "poisonParticle")
            {
                poisonParticle = script.GetPooledObject();
                poisonParticle.transform.parent = transform;
                ParticleSystem.MainModule main = poisonParticle.GetComponent<ParticleSystem>().main;
                main.duration = playerStats.poisonGunDurationValue;
                poisonParticle.SetActive(true);
                poisonParticle.transform.position = transform.position;
            }
        }
        while (elapsedTime <= playerStats.poisonGunDurationValue)
        {
            CollisionDetected((int)playerStats.poisonGunBetweenDamageValue);
            elapsedTime += playerStats.poisonGunBetweenAttackSpeedValue;
            yield return new WaitForSeconds(playerStats.poisonGunBetweenAttackSpeedValue);
        }
    }
    public void StartFlame()
    {
        if (!isFlameStarted)
        {
            isFlameStarted = true;
            StartCoroutine("Flame");
        }
    }
    IEnumerator Flame()
    {
        float elapsedTime = 0f;
        foreach (ObjectPool script in objPools)
        {
            if (script.type == "flameParticle")
            {
                flameParticle = script.GetPooledObject(); //tu cos nie gra
                flameParticle.transform.parent = transform;
                ParticleSystem.MainModule main = flameParticle.GetComponent<ParticleSystem>().main;
                main.duration = playerStats.flameGunDurationValue;
                flameParticle.SetActive(true);
                flameParticle.transform.position = transform.position;
            }
        }
        while (elapsedTime <= playerStats.flameGunDurationValue)
        {
            yield return new WaitForSeconds(0.5f);
            CollisionDetected((int)playerStats.flameGunBetweenDamageValue);
            elapsedTime += 0.5f;
        }
    }
    public void ConnectBossToPillar()
    {
        lineRenderer.SetPositions(new Vector3[] { transform.position, boss.transform.position });
    }
}
