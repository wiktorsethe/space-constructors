using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWarrior : MonoBehaviour
{
    private ExpBar expBar;
    private HpBar hpBar;
    public float health;
    public int experience; // ??
    public int gold;
    public GameManager gameManager;
    public PlayerStats playerStats;
    private GameObject player;
    private float attackTimer = 0f;
    private float moveSpeed = 2f;
    private void Start()
    {
        expBar = GameObject.FindObjectOfType(typeof(ExpBar)) as ExpBar;
        hpBar = GameObject.FindObjectOfType(typeof(HpBar)) as HpBar;
        gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        attackTimer += Time.deltaTime;
        if (health <= 0)
        {
            expBar.SetExperience(experience);
            playerStats.gold += gold;
            gameManager.goldEarned += gold;
            gameManager.kills += 1;
            Destroy(gameObject);
        }
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < 30f && attackTimer >= 2.5f)
        {
            Vector3 vectorToTarget = player.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget); // rotacja do przegadania bo pewnie bedzie tylko L/R
            //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime); //ciekawe rzeczy jak weŸmiemy .right
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            attackTimer = 0f;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player" && attackTimer > 2f)
        {
            hpBar.SetHealth(5f);
            attackTimer = 0f;
        }
    }
}
