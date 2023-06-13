using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public string target;
    private float shootTimer = 0f;
    void Update()
    {
        shootTimer += Time.deltaTime;
        if(shootTimer >= 1f)
        {
            FireBullet();
            shootTimer = 0f;
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
