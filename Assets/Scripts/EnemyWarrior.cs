using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class EnemyWarrior : MonoBehaviour
{
    [Header("Other Scripts")]
    private ExpBar expBar;
    private HpBar hpBar;
    private GameManager gameManager;
    public PlayerStats playerStats;
    [Space(20f)]
    [Header("Variables")]
    [SerializeField] private int experience;
    [SerializeField] private int gold;
    public float attackTimer = 0f;
    private float moveSpeed = 2f;
    [Space(20f)]
    [Header("GameObjects and Rest")]
    private GameObject player;
    [SerializeField] private GameObject miningTextPrefab;
    [SerializeField] private Animator animator;
    private bool facingRight;
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
        hpBar = GameObject.FindObjectOfType(typeof(HpBar)) as HpBar;
        gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
        player = GameObject.FindGameObjectWithTag("Player");

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

        attackTimer += Time.deltaTime;
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < 3f)
        {
            if (attackTimer < 3f)
            {
                //Vector3 vectorToTarget = player.transform.position - transform.position;
                //transform.Find("EnemyRangerImage").transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget);
            }

            if (attackTimer >= 3f)
            {
                //FireBullet();
                animator.SetTrigger("Play");
                hpBar.SetHealth(5f);
                attackTimer = 0f;
            }
        }
        else if (distance >= 3f && distance < 30f)
        {
            //Vector3 vectorToTarget = player.transform.position - transform.position;
            //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime); //ciekawe rzeczy jak weŸmiemy .right
            //transform.Find("EnemyRangerImage").transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }


        /*
        if (distance < 30f && attackTimer >= 2.5f)
        {
            if (velocityX > 0 && facingRight)
            {
                Flip();
            }
            if (velocityX < 0 && !facingRight)
            {
                Flip();
            }
            //Vector3 vectorToTarget = player.transform.position - transform.position;
            //transform.Find("EnemyWarriorImage").transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget); // rotacja do przegadania bo pewnie bedzie tylko L/R
            //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime); //ciekawe rzeczy jak weŸmiemy .right
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }*/
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
    public void CollisionDetected()
    {
        healthBarCanvas.SetActive(true);
        hideTimer = 0f;
        currentHealth -= 10;
        SetHealth();
        if (currentHealth <= 0)
        {
            expBar.SetExperience(experience);
            playerStats.gold += gold;
            gameManager.goldEarned += gold;
            gameManager.kills += 1;
            Destroy(gameObject, 2f);
            GetComponentInChildren<CapsuleCollider2D>().enabled = false;
            GetComponent<LootBag>().InstantiateLoot(transform.position);
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
