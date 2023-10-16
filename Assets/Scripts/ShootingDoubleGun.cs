using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingDoubleGun : MonoBehaviour
{
    [Header("Other Scripts")]
    public PlayerStats playerStats;
    private ObjectPool objPool;
    [Space(20f)]

    [Header("Objects and List")]
    [SerializeField] private Animator shootAnimator;
    [SerializeField] private Transform[] firePoints;
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
            GameObject bullet = objPool.GetPooledObject();
            bullet.transform.position = firePoints[i].position;
            bullet.GetComponent<ShootingBullet>().startingPos = firePoints[i].position;
            bullet.transform.rotation = firePoints[i].rotation;
            bullet.SetActive(true);
            bullet.GetComponent<ShootingBullet>().target = target;
            bullet.GetComponent<ShootingBullet>().damage = playerStats.doubleGunDamageValue;
            bullet.GetComponent<ShootingBombBullet>().ChangeSize();
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 bulletVelocity = firePoints[i].up * bulletSpeed;
            rb.velocity = bulletVelocity;
        }

    }
}
