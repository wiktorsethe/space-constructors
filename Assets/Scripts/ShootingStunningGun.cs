using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStunningGun : MonoBehaviour
{
    [Header("Other Scripts")]
    public PlayerStats playerStats;
    private ObjectPool objPool;
    [Space(20f)]

    [Header("Objects and List")]
    [SerializeField] private Animator shootAnimator;
    [SerializeField] private Transform firePoint;
    [Space(20f)]

    [Header("Variables")]
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private string target;
    public float shootTimer = 0f;

    private void Start()
    {
        objPool = GetComponent<ObjectPool>();
    }
    void Update()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= playerStats.stunningGunAttackSpeedValue)
        {
            FireBullet();
            shootTimer = 0f;
        }

    }
    void FireBullet()
    {
        shootAnimator.SetTrigger("Play");
        GameObject bullet = objPool.GetPooledObject();
        bullet.transform.position = firePoint.position;
        bullet.GetComponent<ShootingBullet>().startingPos = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
        bullet.SetActive(true);
        bullet.GetComponent<ShootingBullet>().target = target;
        bullet.GetComponent<ShootingBullet>().damage = playerStats.stunningGunDamageValue;
        bullet.GetComponent<ShootingBombBullet>().ChangeSize();
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 bulletVelocity = firePoint.up * bulletSpeed;
        rb.velocity = bulletVelocity;
    }
}
