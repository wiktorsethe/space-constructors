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
    private float rotationModifier = 0;
    public float inTarget = 7;

    public float bulletSpeed = 10f;
    public string target;
    private float shootTimer = 0f;

    public GameObject bulletPrefab;
    public Transform firePoint;
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
            Vector3 vectorToTarget = player.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 10f);
            if (shootTimer >= 3f)
            {
                FireBullet();
                shootTimer = 0f;
            }
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
