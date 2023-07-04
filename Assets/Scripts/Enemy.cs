using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private ExpBar expBar;
    public float health;
    public int experience; // ??
    public int gold;
    public GameManager gameManager;
    public PlayerStats playerStats;
    private GameObject player;
    public float inTarget = 7;

    public float bulletSpeed = 10f;
    public string target;
    private float shootTimer = 0f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    private float moveSpeed = 2f;
    private void Start()
    {
        expBar = GameObject.FindObjectOfType(typeof(ExpBar)) as ExpBar;
        gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if(health <= 0)
        {
            expBar.SetExperience(experience);
            playerStats.gold += gold;
            gameManager.goldEarned += gold;
            gameManager.kills += 1;
            Destroy(gameObject);
        }
        float distance = Vector2.Distance(transform.position, player.transform.position);
        shootTimer += Time.deltaTime;
        if (distance < inTarget)
        {
            if(shootTimer < 2f)
            {
                Vector3 vectorToTarget = player.transform.position - transform.position;
                transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget);
            }

            if (shootTimer >= 3f)
            {
                FireBullet();
                shootTimer = 0f;
            }
        }
        else if(distance >= (inTarget - 1f) && distance < 30f)
        {
            Vector3 vectorToTarget = player.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget);
            //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime); //ciekawe rzeczy jak we�miemy .right
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }

    }

    void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<ShootingBullet>().target = target;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 bulletVelocity = firePoint.up * bulletSpeed;
        rb.velocity = bulletVelocity;
    }
}
