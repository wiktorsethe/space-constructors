using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBigGun : MonoBehaviour
{
    [Header("Other Scripts")]
    public PlayerStats playerStats;
    [Space(20f)]

    [Header("Objects and List")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Animator shootAnimator;
    [SerializeField] private Transform firePoint;
    [Space(20f)]

    [Header("Variables")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private string target;
    public float shootTimer = 0f;
    void Update()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= playerStats.bigGunAttackSpeedValue)
        {
            FireBullet();
            shootTimer = 0f;
        }

    }
    void FireBullet()
    {
        shootAnimator.SetTrigger("Play");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<ShootingBullet>().target = target;
        bullet.GetComponent<ShootingBullet>().damage = playerStats.bigGunDamageValue;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 bulletVelocity = firePoint.up * bulletSpeed;
        rb.velocity = bulletVelocity;
    }
}
