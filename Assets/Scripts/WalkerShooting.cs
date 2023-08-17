using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerShooting : MonoBehaviour
{
    [Header("Objects and List")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] enemies;
    private ObjectPool objPool;
    //public GameObject activeEnemy;
    private float distance;
    [Space(20f)]

    [Header("Variables")]
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private string target;
    private float shootTimer = 0f;
    private float attractDistance = 10f;

    private void Start()
    {
        objPool = GetComponent<ObjectPool>();
    }
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
        GameObject bullet = objPool.GetPooledObject();
        bullet.transform.position = firePoint.position;
        bullet.GetComponent<ShootingBullet>().startingPos = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
        bullet.SetActive(true);
        bullet.GetComponent<ShootingBullet>().target = target;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 bulletVelocity = firePoint.up * bulletSpeed;
        rb.velocity = bulletVelocity;

    }
}
