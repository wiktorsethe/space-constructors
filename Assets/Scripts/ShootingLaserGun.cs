using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingLaserGun : MonoBehaviour
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
    [SerializeField] private float delayTimer;
    [SerializeField] private int delayIndex = 0;
    private void Start()
    {
        objPool = GetComponent<ObjectPool>();
    }
    void Update()
    {
        shootTimer += Time.deltaTime;
        delayTimer += Time.deltaTime;
        if (shootTimer >= playerStats.laserGunAttackSpeedValue)
        {
            if (delayTimer >= 0.3f)
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
        shootAnimator.SetTrigger("Play");
        GameObject bullet = objPool.GetPooledObject(0);
        bullet.transform.position = firePoint.position;
        bullet.GetComponent<ShootingBullet>().startingPos = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
        bullet.SetActive(true);
        bullet.GetComponent<ShootingBullet>().target = target;
        bullet.GetComponent<ShootingBullet>().damage = playerStats.laserGunDamageValue;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 bulletVelocity = firePoint.up * bulletSpeed;
        rb.velocity = bulletVelocity;
    }

}
