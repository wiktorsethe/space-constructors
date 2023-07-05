using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerShooting : MonoBehaviour
{
    [Header("Objects and List")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public GameObject[] enemies;
    //public GameObject activeEnemy;
    private float distance;
    [Space(20f)]

    [Header("Variables")]
    public float bulletSpeed = 10f;
    public string target;
    private float shootTimer = 0f;
    private float attractDistance = 10f;

    void Update()
    {
        shootTimer += Time.deltaTime;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        for(int i=0; i<enemies.Length; i++)
        {
            if (i == 0)
            {
                distance = Vector3.Distance(transform.position, enemies[0].transform.position);
                //activeEnemy = enemies[0];
            }
            else if(Vector3.Distance(transform.position, enemies[i].transform.position) < distance)
            {
                distance = Vector3.Distance(transform.position, enemies[i].transform.position);
                //activeEnemy = enemies[i];
            }
        }

        if (shootTimer >= 3f && distance < attractDistance)
        {
            FireBullet();
            shootTimer = 0f;
        }
        else if(enemies.Length == 0)
        {
            distance = 1000f;
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
