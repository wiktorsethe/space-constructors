using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Objects and List")]
    public GameObject bulletPrefab;
    public List<Transform> firePoints = new List<Transform>();
    [Space(20f)]

    [Header("Variables")]
    public float bulletSpeed = 10f;
    public string target;
    private float shootTimer = 0f;

    void Update()
    {
        shootTimer += Time.deltaTime;
        if(shootTimer >= 3f)
        {
            FireBullet();
            shootTimer = 0f;
        }
        
    }

    void FireBullet()
    {
        for(int i=0; i<firePoints.Count; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoints[i].position, firePoints[i].rotation);
            bullet.GetComponent<ShootingBullet>().target = target;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 bulletVelocity = firePoints[i].up * bulletSpeed;
            rb.velocity = bulletVelocity;
        }

    }
    public void AddToList(Transform shootingPoint)
    {
        firePoints.Add(shootingPoint);
    }
}
