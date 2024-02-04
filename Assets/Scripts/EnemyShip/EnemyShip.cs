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
    [SerializeField] private PlayerStats playerStats;
    [Space(20f)]

    [Header("Variables")]
    [SerializeField] private int experience;
    [SerializeField] private int gold;
    [SerializeField] private string target;
    private float bulletSpeed = 10f;
    public float moveSpeed;
    [Space(20f)]

    [Header("GameObjects and Rest")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject textPrefab;
    [SerializeField] private AudioSource dashingSound;
    public Vector3 savedPos;
    [Space(20f)]

    [Header("Health System")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject healthBarCanvas;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Gradient healthGradient;
    [SerializeField] private Image fillBar;
    [SerializeField] private int maxHealth;
    private float hideTimer = 0f;
    public int currentHealth;
    [Space(20f)]

    [Header("Particles and animator staff")]
    public Animator animator;
    private bool isFlameStarted = false;
    private bool isPoisonStarted = false;
    private GameObject flameParticle;
    private GameObject poisonParticle;
    private GameObject dashParticle;
    private ObjectPool[] objPools;
    private bool isObjectActivated = false;
    public Vector2 retreatVector;
    private void Start()
    {
        expBar = GameObject.FindObjectOfType(typeof(ExpBar)) as ExpBar;
        gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
        dashingSound = GameObject.Find("Dash").GetComponent<AudioSource>();
        objPools = GetComponents<ObjectPool>();
        canvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        SetMaxHealth(maxHealth);
        healthBarCanvas.SetActive(false);
    }
    private void Update()
    {
        // Sprawdzenie, czy obiekt jest ju¿ aktywny i ustawienie zdrowia do 100%
        if (!isObjectActivated)
        {
            SetMaxHealth(maxHealth);
            isObjectActivated = true;
        }
        fillBar.color = healthGradient.Evaluate(healthBar.normalizedValue);
        hideTimer += Time.deltaTime;
        if (hideTimer > 3f)
        {
            healthBarCanvas.SetActive(false);
        }

        // Zresetowanie zdrowia i dezaktywacja obiektu po jego zniszczeniu 
        if (currentHealth <= 0)
        {
            ShowLootText();
            playerStats.screw += 5;
            moveSpeed = 45f;
            expBar.SetExperience(experience);
            playerStats.gold += gold;
            gameManager.goldEarned += gold;
            gameManager.kills += 1;
            isObjectActivated = false;
            gameObject.SetActive(false);
        }

        // Sprawdzenie czy cz¹steczki ognia, trucizny i ataku "dash" zakoñczy³y swoje dzia³anie
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

        if (dashParticle != null)
        {
            if (!dashParticle.GetComponent<ParticleSystem>().IsAlive())
            {
                dashParticle.SetActive(false);
            }
        }
        ChangeRotation();
    }
    public void FireBullet()
    {
        // Znalezienie obiektu z puli i strza³ w kierunku celu 
        foreach (ObjectPool script in objPools)
        {
            if (script.type == "bullet")
            {
                GameObject bullet = script.GetPooledObject();
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
        }
    }

    // Ustawienie rotacji w kierunku najbli¿szego fragmentu statku gracza
    public void ChangeRotation()
    {
        GameObject ship = FindClosestObject(); // Znalezienie najbli¿szego fragmentu statku 
        if (ship)
        {
            Vector3 vectorToTarget = ship.transform.position - animator.transform.position;
            animator.transform.Find("EnemyShipImage").transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget);
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
    public void CollisionDetected(int damage)
    {
        // Wykrycie kolizji z pociskiem, wyœwietlenie obra¿eñ i zaktualizowanie paska zdrowia
        healthBarCanvas.SetActive(true);
        hideTimer = 0f;
        currentHealth -= damage;
        SetHealth();
        moveSpeed = 40f;
        StartCoroutine(ChangingSpeed());
        if (textPrefab)
        {
            ShowDamageText(damage);
        }
    }
    private void ShowDamageText(int amount)
    {
        // Wyœwietlenie tekstu obra¿eñ
        var text = Instantiate(textPrefab, transform.position, Quaternion.identity);
        text.GetComponent<TMP_Text>().text = amount.ToString();
    }
    private void ShowLootText()
    {
        // Wyœwietlenie tekstu zdobyczy
        var text = Instantiate(textPrefab, transform.position, Quaternion.identity);
        text.GetComponent<TMP_Text>().text = "+5 screws";
    }
    IEnumerator ChangingSpeed()
    {
        // Zmiana prêdkoœci poruszania po trafieniu
        yield return new WaitForSeconds(0.3f);

        while (moveSpeed < 45f)
        {
            moveSpeed += 0.1f;
            yield return new WaitForSeconds(0.1f);

        }
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
                flameParticle = script.GetPooledObject();
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
    public void StartStun()
    {
        // Uruchomienie efektu og³uszenia
        StartCoroutine("Stun");
    }
    IEnumerator Stun()
    {
        moveSpeed = 0f;
        yield return new WaitForSeconds(playerStats.stunDurationValue);

        while (moveSpeed < 2f)
        {
            moveSpeed += 0.1f;
            yield return new WaitForSeconds(0.1f);

        }
    }
    public GameObject FindClosestObject()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Ship");

        if (objectsWithTag.Length == 0)
        {
            return null;
        }
        float closestDistance = Mathf.Infinity;
        GameObject closestObject = null;

        foreach (GameObject obj in objectsWithTag)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = obj;
            }
        }

        return closestObject;
    }
    public void Dash()
    {
        dashingSound.Play();
        foreach (ObjectPool script in objPools)
        {
            if (script.type == "dashParticle")
            {
                dashParticle = script.GetPooledObject();
                dashParticle.transform.parent = transform;
                dashParticle.SetActive(true);
                dashParticle.transform.position = firePoint.transform.position;
            }
        }
    }
}
