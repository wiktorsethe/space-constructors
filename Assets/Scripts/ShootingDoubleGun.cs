using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingDoubleGun : MonoBehaviour
{
    [Header("Other Scripts")]
    public PlayerStats playerStats;
    [Space(20f)]

    [Header("Objects and List")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Animator shootAnimator;
    [SerializeField] private Transform[] firePoints;
    [Space(20f)]

    [Header("Variables")]
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private string target;
    public float shootTimer = 0f;
    void Update()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= playerStats.doubleGunAttackSpeedValue)
        {
            FireBullet();
            shootTimer = 0f;
        }

    }
    void FireBullet()
    {
        shootAnimator.SetTrigger("Play");
        for (int i=0; i<firePoints.Length; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoints[i].position, firePoints[i].rotation);
            bullet.GetComponent<ShootingBullet>().target = target;
            bullet.GetComponent<ShootingBullet>().damage = playerStats.doubleGunDamageValue;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 bulletVelocity = firePoints[i].up * bulletSpeed;
            rb.velocity = bulletVelocity;
        }

    }
}
