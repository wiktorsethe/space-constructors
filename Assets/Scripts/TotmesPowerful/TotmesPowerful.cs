using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class TotmesPowerful : MonoBehaviour
{
    [Header("Other Scripts")]
    public PlayerStats playerStats;
    private ExpBar expBar;
    private GameManager gameManager;
    private ObjectPool[] objPools;
    private CameraShake camShake;
    private CameraSize camSize;
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
    private Camera mainCam;
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
    private bool isDeath = false;
    private bool isHalfDeath = false;
    private bool isFlameStarted = false;
    private bool isPoisonStarted = false;
    private GameObject flameParticle;
    private GameObject poisonParticle;
    private GameObject flameThrowerParticle;
    private bool arePillarsEnabled = false;
    public Vector2 currentCorner;
    public Vector2 nextCorner;
    private void Start()
    {
        expBar = GameObject.FindObjectOfType(typeof(ExpBar)) as ExpBar;
        gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
        camShake = GameObject.FindObjectOfType(typeof(CameraShake)) as CameraShake;
        camSize = GameObject.FindObjectOfType(typeof(CameraSize)) as CameraSize;
        healthBarCanvas = GameObject.Find("BossHPBar");
        objPools = GetComponents<ObjectPool>();
        healthBar = GameObject.Find("BossHPBar").GetComponent<Slider>();
        fillBar = GameObject.Find("BossHPBar").GetComponentInChildren<Image>();
        mainCam = Camera.main;
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
        //animator.SetTrigger("Totems");
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

        if (currentHealth <= 0 && !isDeath)
        {
            isDeath = true;
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
            foreach (ObjectPool script in objPools)
            {
                for (int i = 0; i < script.pooledObjects.Count; i++)
                {
                    Destroy(script.pooledObjects[i].gameObject);
                }
            }
        }

        if (currentHealth <= maxHealth * 0.5f && !isHalfDeath)
        {
            isHalfDeath = true;
            animator.SetTrigger("Totems");
        }

        if (isHalfDeath && arePillarsEnabled)
        {
            GameObject[] pillars = GameObject.FindGameObjectsWithTag("Pillar");
            if (pillars.Length == 0)
            {
                arePillarsEnabled = false;
            }
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
        if (!arePillarsEnabled)
        {
            if (collision.GetComponent<ShootingBullet>().type == "NormalBullet")
            {
                CollisionDetected((int)collision.GetComponent<ShootingBullet>().damage);
            }
            if (collision.GetComponent<ShootingBullet>().type == "PoisonBullet")
            {
                StartPoison();
            }
            if (collision.GetComponent<ShootingBullet>().type == "FlameBullet")
            {
                StartFlame();
            }
        }
        else
        {
            if (damageTextPrefab)
            {
                ShowDamageText(0);
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
        foreach (ObjectPool script in objPools)
        {
            if (script.type == "bullet")
            {
                GameObject bullet = script.GetPooledObject();
                bullet.transform.position = firePoints[i].position;
                bullet.GetComponent<ShootingBullet>().startingPos = firePoints[i].position;
                bullet.transform.rotation = firePoints[i].rotation;
                bullet.SetActive(true);
                bullet.GetComponent<ShootingBullet>().target = target;
                bullet.GetComponent<ShootingBullet>().damage = 1;
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                Vector2 bulletVelocity = firePoints[i].up * bulletSpeed;
                rb.velocity = bulletVelocity;
            }
        }
    }
    private void DestroyParticles()
    {
        Instantiate(destroyParticles, transform.position, Quaternion.identity);
    }
    public void CollisionDetected(int damage)
    {
        currentHealth -= damage;
        SetHealth();
        if (damageTextPrefab)
        {
            ShowDamageText(damage);
        }
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
    public void ChangeRotation()
    {
        GameObject ship = FindClosestObject();
        Vector3 vectorToTarget = ship.transform.position - animator.transform.position;
        animator.transform.Find("BossMain").transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget);
    }
    public void ChangeRotBorder(Quaternion rot)
    {
        animator.transform.Find("BossMain").transform.rotation = Quaternion.Slerp(animator.transform.Find("BossMain").transform.rotation, rot, 5f * Time.deltaTime);
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
    public Vector3 GetRandomPointInCameraView()
    {
        Rect cameraBounds = GetCameraBounds(mainCam);

        float randomX = Random.Range(cameraBounds.xMin, cameraBounds.xMax);
        float randomY = Random.Range(cameraBounds.yMin, cameraBounds.yMax);

        Vector3 randomPoint = new Vector3(randomX, randomY, 0f);

        return randomPoint;
    }
    public Vector3 GetNextCorner(int prevBorder)
    {
        Vector3 randomPoint = Vector3.zero;

        // Get camera information
        float cameraHeight = 2f * mainCam.orthographicSize;
        float cameraWidth = cameraHeight * mainCam.aspect;

        // Calculate random point on the border
        switch (prevBorder)
        {
            case 0: // Top-left corner
                randomPoint = new Vector3(cameraWidth * 0.45f, cameraHeight * 0.4f, 0);
                break;
            case 1: // Top-right corner
                randomPoint = new Vector3(-cameraWidth * 0.45f, cameraHeight * 0.4f, 0);
                break;
            case 2: // Bottom-left corner
                randomPoint = new Vector3(cameraWidth * 0.45f, -cameraHeight * 0.4f, 0);
                break;
            case 3: // Bottom-right corner
                randomPoint = new Vector3(-cameraWidth * 0.45f, -cameraHeight * 0.4f, 0);
                break;
        }

        // Convert to world space
        randomPoint = mainCam.transform.position + mainCam.transform.TransformVector(randomPoint);

        return randomPoint;
    }
    public Vector3 GetRandomPointOnBorder(int randomBorder)
    {
        Vector3 randomPoint = Vector3.zero;

        // Get camera information
        float cameraHeight = 2f * mainCam.orthographicSize;
        float cameraWidth = cameraHeight * mainCam.aspect;

        // Calculate random point on the border
        switch (randomBorder)
        {
            case 0: // Top-left corner
                randomPoint = new Vector3(-cameraWidth * 0.45f, cameraHeight * 0.4f, 0);
                nextCorner = GetNextCorner(0);
                break;
            case 1: // Top-right corner
                randomPoint = new Vector3(cameraWidth * 0.45f, cameraHeight * 0.4f, 0);
                nextCorner = GetNextCorner(1);
                break;
            case 2: // Bottom-left corner
                randomPoint = new Vector3(-cameraWidth * 0.45f, -cameraHeight * 0.4f, 0);
                nextCorner = GetNextCorner(2);
                break;
            case 3: // Bottom-right corner
                randomPoint = new Vector3(cameraWidth * 0.45f, -cameraHeight * 0.4f, 0);
                nextCorner = GetNextCorner(3);
                break;
        }

        // Convert to world space
        randomPoint = mainCam.transform.position + mainCam.transform.TransformVector(randomPoint);

        return randomPoint;
    }

    Rect GetCameraBounds(Camera camera)
    {
        Vector2 bottomLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        Vector2 topRight = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));

        Rect bounds = new Rect(bottomLeft.x, bottomLeft.y, topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);

        return bounds;
    }
    public void SpawnFireTotems()
    {
        foreach (ObjectPool script in objPools)
        {
            if (script.type == "fireTotemParticle")
            {
                for(int i=0; i<2; i++)
                {
                    GameObject fireTotem = script.GetPooledObject();
                    fireTotem.SetActive(true);
                    fireTotem.transform.position = GetRandomPointInCameraView();
                }
            }
        }
    }
    public void FlameThrower()
    {
        foreach (ObjectPool script in objPools)
        {
            if (script.type == "flameThrowerParticle")
            {
                flameThrowerParticle = script.GetPooledObject(); //tu cos nie gra
                flameThrowerParticle.transform.parent = transform.Find("BossMain").transform;
                ParticleSystem.MainModule main = flameThrowerParticle.GetComponent<ParticleSystem>().main;
                main.loop = true;
                float cameraHeight = mainCam.orthographicSize * 0.2f;
                float cameraWidth = cameraHeight * mainCam.aspect;
                main.startLifetime = cameraWidth / 2;
                main.duration = cameraWidth;
                flameThrowerParticle.SetActive(true);
                flameThrowerParticle.transform.position = transform.position;
                flameThrowerParticle.transform.rotation = transform.Find("BossMain").transform.rotation;
            }
        }
    }
    public void FlameThrowerEnd()
    {
        flameThrowerParticle.SetActive(false);
    }

    public float ObjectSize()
    {
        Bounds parentBounds = camSize.CalculateParentBounds();
        return parentBounds.size.x;
    }
}
