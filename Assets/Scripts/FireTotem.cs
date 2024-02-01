using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class FireTotem : MonoBehaviour
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
    private float hideTimer = 0f;
    [Space(20f)]
    [Header("Particles and animator staff")]
    private bool isFlameStarted = false;
    private bool isPoisonStarted = false;
    private GameObject flameParticle;
    private GameObject poisonParticle;
    private GameObject flameThrowerParticle;
    private ObjectPool[] objPools;
    private void Awake()
    {
        // Inicjalizacja obiektu i ustawienie maksymalnego zdrowia
        objPools = GetComponents<ObjectPool>();
        SetMaxHealth(maxHealth);
        healthBarCanvas.SetActive(false);
        // Obliczenia skali obiektu do rozmiaru ekranu
        cam = Camera.main;
        float cameraHeight = cam.orthographicSize * 0.25f;
        float cameraWidth = cameraHeight * cam.aspect;

        float scaleX = cameraWidth / transform.localScale.x;
        float scaleY = cameraHeight / transform.localScale.y;

        float objectScale = Mathf.Min(scaleX, scaleY);
        transform.localScale = new Vector3(objectScale, objectScale, 1f);
    }
    private void Update()
    {
        // Aktualizacja koloru paska zdrowia w zale¿noœci od aktualnego zdrowia
        fillBar.color = healthGradient.Evaluate(healthBar.normalizedValue);
        hideTimer += Time.deltaTime;

        // Ukrycie paska zdrowia po pewnym czasie bez obra¿eñ
        if (hideTimer > 3f)
        {
            healthBarCanvas.SetActive(false);
        }

        // Zresetowanie zdrowia i dezaktywacja obiektu po jego zniszczeniu
        if (currentHealth <= 0)
        {
            SetMaxHealth(maxHealth);
            gameObject.SetActive(false);
        }

        // Sprawdzenie czy cz¹steczki ognia i trucizny zakoñczy³y swoje dzia³anie
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Rozpoznanie typu pocisku i uruchomienie odpowiedniej reakcji
        if (collision.transform.GetComponent<ShootingBullet>().type == "PoisonBullet")
        {
            StartPoison();
        }
        if (collision.transform.GetComponent<ShootingBullet>().type == "NormalBullet" 
            || collision.transform.GetComponent<ShootingBullet>().type == "HomingBullet" 
            || collision.transform.GetComponent<ShootingBullet>().type == "StunningBullet")
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
        // Ustawienie maksymalnego zdrowia
        currentHealth = health;
        maxHealth = health;
        healthBar.maxValue = health;
        healthBar.value = health;
        fillBar.color = healthGradient.Evaluate(1f);
    }
    public void SetHealth()
    {
        // Animacja zmiany wartoœci paska zdrowia
        DOTween.To(() => healthBar.value, x => healthBar.value = x, currentHealth, 1.5f);
    }
    private void CollisionDetected(int damage)
    {
        // Wykrycie kolizji z pociskiem, wyœwietlenie obra¿eñ i zaktualizowanie paska zdrowia
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
        // Wyœwietlenie tekstu obra¿eñ
        var text = Instantiate(damageTextPrefab, transform.position, Quaternion.identity);
        text.GetComponent<TMP_Text>().text = amount.ToString();
    }
    public void StartPoison()
    {
        // Uruchomienie efektu trucizny, jeœli jeszcze nie zosta³ uruchomiony
        if (!isPoisonStarted)
        {
            isPoisonStarted = true;
            StartCoroutine("Poison");

        }
    }
    IEnumerator Poison()
    {
        // Uruchomienie efektu trucizny i zadawanie obra¿eñ w okreœlonych odstêpach czasu
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
        // Uruchomienie efektu ognia, jeœli jeszcze nie zosta³ uruchomiony
        if (!isFlameStarted)
        {
            isFlameStarted = true;
            StartCoroutine("Flame");
        }
    }
    IEnumerator Flame()
    {
        // Uruchomienie efektu ognia i zadawanie obra¿eñ w okreœlonych odstêpach czasu
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
    public void FlameThrower()
    {
        // Uruchomienie efektu miotacza ognia
        foreach (ObjectPool script in objPools)
        {
            if (script.type == "flameThrowerParticle")
            {
                flameThrowerParticle = script.GetPooledObject(); //tu cos nie gra
                flameThrowerParticle.transform.parent = transform.Find("FireTotemGraphic").transform;
                ParticleSystem.MainModule main = flameThrowerParticle.GetComponent<ParticleSystem>().main;
                main.loop = true;
                cam = Camera.main;
                float cameraHeight = cam.orthographicSize * 0.25f;
                float cameraWidth = cameraHeight * cam.aspect * 0.15f;
                main.startLifetime = cameraWidth;
                flameThrowerParticle.SetActive(true);
                flameThrowerParticle.transform.position = transform.position;
            }
        }
    }
}
