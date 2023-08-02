using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingLaserGun : MonoBehaviour
{
    [Header("Other Scripts")]
    public PlayerStats playerStats;
    [Space(20f)]

    [Header("Objects and List")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [Space(20f)]

    [Header("Variables")]
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private string target;
    public float shootTimer = 0f;
    [SerializeField] private float delayTimer;
    [SerializeField] private int delayIndex = 0;
    void Update()
    {
        shootTimer += Time.deltaTime;
        delayTimer += Time.deltaTime;
        if (shootTimer >= playerStats.laserGunAttackSpeedValue)
        {
            if (delayTimer >= 0.2f)
            {
                if(delayIndex < 3)
                {
                    FireBullet();
                    delayIndex++;
                    if (delayIndex == 3)
                    {
                        shootTimer = 0f;
                    }
                }
                delayTimer = 0f;
            }
        }
        else
        {
            delayIndex = 0;
        }

    }
    void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<ShootingBullet>().target = target;
        bullet.GetComponent<ShootingBullet>().damage = 2 * playerStats.laserGunDamageValue;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 bulletVelocity = firePoint.up * bulletSpeed;
        rb.velocity = bulletVelocity;
    }

}
