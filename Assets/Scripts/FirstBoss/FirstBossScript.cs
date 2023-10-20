using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class FirstBossScript : MonoBehaviour
{
    [Header("Other Scripts")]
    private ObjectPool[] objPools;
    public PlayerStats playerStats;
    private GameManager gameManager;
    private ExpBar expBar;
    [Space(20f)]

    [Header("Health System")]
    [SerializeField] private GameObject healthBarCanvas;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Image fillBar;
    [SerializeField] private int maxHealth;
    public int currentHealth;
    private bool isDeath = false;
    private bool isHalfDeath = false;
    private bool isQuarterDeath = false;
    [Space(20f)]

    [Header("Damage/Attack")]
    [SerializeField] private GameObject damageTextPrefab;
    [SerializeField] private Transform[] firePoints;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private string target;
    [SerializeField] private int experience;
    [SerializeField] private int gold;
    [Space(20f)]

    [Header("Animations")]
    private bool isFlameStarted = false;
    private bool isPoisonStarted = false;
    private GameObject flameParticle;
    private GameObject poisonParticle;
    [SerializeField] private bool isShieldActive = true;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject destroyParticles;


    private Camera mainCam;
    public Vector2 nextCorner;
    private GameObject flameThrowerParticle;
    private CameraSize camSize;

    private void Start()
    {
        expBar = GameObject.FindObjectOfType(typeof(ExpBar)) as ExpBar;
        gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
        healthBarCanvas = GameObject.Find("BossHPBar");
        objPools = GetComponents<ObjectPool>();
        healthBar = GameObject.Find("BossHPBar").GetComponent<Slider>();
        fillBar = GameObject.Find("BossHPBar").transform.Find("Bar").GetComponent<Image>();
        camSize = GameObject.FindObjectOfType(typeof(CameraSize)) as CameraSize;
        mainCam = Camera.main;
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
        Shield();
    }
    private void Update()
    {
        //fillBar.color = healthGradient.Evaluate(healthBar.normalizedValue);

        if (!healthBarCanvas.activeSelf)
        {
            healthBarCanvas.SetActive(true);
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
            expBar.SetExperience(experience);
            playerStats.gold += gold;
            gameManager.goldEarned += gold;
            gameManager.kills += 1;
            GetComponent<PolygonCollider2D>().enabled = false;
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
            animator.SetTrigger("Shield");
        }
        if (currentHealth <= maxHealth * 0.75f && !isQuarterDeath)
        {
            isQuarterDeath = true;
            animator.SetTrigger("FlamethrowerStart");
        }
        /*
        if (isHalfDeath && !isShieldActive)
        {
            Shield();
        }*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isShieldActive)
        {
            if (collision.gameObject.GetComponent<ShootingBullet>())
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
        }
        else
        {
            if (damageTextPrefab)
            {
                ShowDamageText(0);
            }
        }
    }
    private void DestroyParticles()
    {
        Instantiate(destroyParticles, transform.position, Quaternion.identity);
    }
    public void ChangeRotation()
    {
        GameObject ship = FindClosestObject();
        Vector3 vectorToTarget = ship.transform.position - animator.transform.position;
        animator.transform.Find("main").transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget);
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
    public void SetMaxHealth(int health)
    {
        healthBar.maxValue = health;
        healthBar.value = health;
        //fillBar.color = healthGradient.Evaluate(1f);
    }
    public void SetHealth()
    {
        DOTween.To(() => healthBar.value, x => healthBar.value = x, currentHealth, 1.5f);
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
    public void Shield()
    {
        GameObject shield = transform.Find("main").transform.Find("Shield").gameObject;
        if (isShieldActive)
        {
            isShieldActive = false;
            shield.SetActive(false);
        }
        else
        {
            isShieldActive = true;
            shield.SetActive(true);
        }
    }
    public void SpawnMinions(int amount)
    {
        for(int i=0; i<amount; i++)
        {
            foreach (ObjectPool script in objPools)
            {
                if (script.type == "minion")
                {
                    GameObject minion = script.GetPooledObject();
                    minion.transform.position = transform.position;
                    minion.GetComponent<FirstBossMinionScript>().bossObject = gameObject;
                    minion.SetActive(true);
                }
            }
                
        }
    }
    public void Pivot(float rotatingSpeed)
    {
        if (FindClosestObject())
        {
            transform.RotateAround(FindClosestObject().transform.position, Vector3.forward, rotatingSpeed * Time.deltaTime);
        }
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
    public void ChangeRotBorder(Quaternion rot)
    {
        animator.transform.Find("main").transform.rotation = Quaternion.Slerp(animator.transform.Find("main").transform.rotation, rot, 5f * Time.deltaTime);
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
    public void FlameThrower()
    {
        foreach (ObjectPool script in objPools)
        {
            if (script.type == "flameThrowerParticle")
            {
                flameThrowerParticle = script.GetPooledObject(); //tu cos nie gra
                flameThrowerParticle.transform.parent = transform.Find("main").transform;
                ParticleSystem.MainModule main = flameThrowerParticle.GetComponent<ParticleSystem>().main;
                main.loop = true;
                float cameraHeight = mainCam.orthographicSize * 0.2f;
                float cameraWidth = cameraHeight * mainCam.aspect;
                main.startLifetime = cameraWidth / 2;
                main.duration = cameraWidth;
                flameThrowerParticle.SetActive(true);
                flameThrowerParticle.transform.position = transform.position;
                flameThrowerParticle.transform.rotation = transform.Find("main").transform.rotation;
            }
        }
    }
    public void FlameThrowerEnd()
    {
        flameThrowerParticle.SetActive(false);
        foreach (ObjectPool script in objPools)
        {
            if (script.type == "wall")
            {
                for(int i=0; i<script.pooledObjects.Count; i++)
                {
                    script.pooledObjects[i].SetActive(false);
                }
            }
        }
    }
    public float ObjectSize()
    {
        Bounds parentBounds = camSize.CalculateParentBounds();
        return parentBounds.size.x;
    }
    public Vector3 GetRandomPointInCameraView()
    {
        Rect cameraBounds = GetCameraBounds(mainCam);

        float randomX = Random.Range(cameraBounds.xMin, cameraBounds.xMax);
        float randomY = Random.Range(cameraBounds.yMin, cameraBounds.yMax);

        Vector3 randomPoint = new Vector3(randomX, randomY, 0f);

        return randomPoint;
    }
    Rect GetCameraBounds(Camera camera)
    {
        Vector2 bottomLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        Vector2 topRight = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));

        Rect bounds = new Rect(bottomLeft.x, bottomLeft.y, topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);

        return bounds;
    }
    public void SpawnWalls(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            foreach (ObjectPool script in objPools)
            {
                if (script.type == "wall")
                {
                    GameObject wall = script.GetPooledObject();
                    wall.transform.position = GetRandomPointInCameraView();
                    wall.SetActive(true);

                }
            }

        }
    }
}
