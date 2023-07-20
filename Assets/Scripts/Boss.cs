using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Boss : MonoBehaviour
{
    public ShootingNormalGun[] shootingScripts;
    private float specialActionTimer = 0;
    public PlayerStats playerStats;
    public GameObject bulletPrefab;
    public Transform[] firePoints;
    public float bulletSpeed;
    public string target;
    public float shootTimer = 0;
    private GameObject[] objectsList;
    private float distance;
    private bool isPositionSelected = false;
    private Vector3 dashPosition;
    private GameObject player;
    private bool firstGunsShoot = false;
    private bool secondGunsShoot = false;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        specialActionTimer += Time.deltaTime;
        shootTimer += Time.deltaTime;

        if (specialActionTimer >= 3f && specialActionTimer < 7.5f)
        {
            Sequence rotationSequence = DOTween.Sequence();
            rotationSequence.Append(transform.DORotate(new Vector3(0, 0, 360), 4f, RotateMode.FastBeyond360));
            if (shootTimer >= 0.5f)
            {

                FireBullet();
                shootTimer = 0f;
            }
        }
        else if(specialActionTimer >= 7.5f && specialActionTimer < 12f)
        {
            if (!isPositionSelected)
            {
                dashPosition = player.transform.position;
                isPositionSelected = true;
            }
            //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime); //ciekawe rzeczy jak weŸmiemy .right
            //transform.Find("EnemyShipImage").transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget);
            transform.position = Vector2.MoveTowards(transform.position, dashPosition, 5f * Time.deltaTime);
        }
        else if(specialActionTimer >= 12f && specialActionTimer < 12.5f)
        {
            if (!firstGunsShoot)
            {
                FireFirstWave();
                firstGunsShoot = true;
            }
        }
        else if (specialActionTimer >= 12.5f && specialActionTimer < 13f)
        {
            if (!secondGunsShoot)
            {
                FireSecondWave();
                secondGunsShoot = true;
            }
        }
        else if (specialActionTimer >= 13f && specialActionTimer < 17.5f)
        {
            if (!isPositionSelected)
            {
                dashPosition = player.transform.position;
                firstGunsShoot = false;
                secondGunsShoot = false;
                isPositionSelected = true;
            }
            //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime); //ciekawe rzeczy jak weŸmiemy .right
            //transform.Find("EnemyShipImage").transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget);
            transform.position = Vector2.MoveTowards(transform.position, dashPosition, 5f * Time.deltaTime);
        }
        else if (specialActionTimer >= 17.5f && specialActionTimer < 18f)
        {
            if (!firstGunsShoot)
            {
                FireFirstWave();
                firstGunsShoot = true;
            }
        }
        else if (specialActionTimer >= 18f && specialActionTimer < 18.5f)
        {
            if (!secondGunsShoot)
            {
                FireSecondWave();
                secondGunsShoot = true;
            }
        }
        else if(specialActionTimer >= 20f)
        {
            firstGunsShoot = false;
            secondGunsShoot = false;
            isPositionSelected = false;
            specialActionTimer = 0f;
        }
        /*
        if (specialActionTimer >= 3f && specialActionTimer <= 7.5f)
        {
            Sequence rotationSequence = DOTween.Sequence();
            rotationSequence.Append(transform.DORotate(new Vector3(0, 0, 360), 4f, RotateMode.FastBeyond360));
            if (shootTimer >= 0.5f)
            {
                
                FireBullet();
                shootTimer = 0f;
            }
        }
        else if(specialActionTimer >= 8f && specialActionTimer < 12f || specialActionTimer >= 15f && specialActionTimer < 20f)
        {
            if (!isPositionSelected)
            {
                dashPosition = player.transform.position;
                isPositionSelected = true;
            }
            //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime); //ciekawe rzeczy jak weŸmiemy .right
            //transform.Find("EnemyShipImage").transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget);
            transform.position = Vector2.MoveTowards(transform.position, dashPosition, 5f * Time.deltaTime);
        }
        else if(specialActionTimer >= 12f && specialActionTimer < 15f)
        {
            isPositionSelected = false;
        }
        else if(specialActionTimer >= 30f)
        {
            isPositionSelected = false;
            specialActionTimer = 0f;
        }
        */

            /*
            objectsList = GameObject.FindGameObjectsWithTag("Ship");
            float closestDistance = Mathf.Infinity;
            GameObject closestObject = null;

            foreach (GameObject obj in objectsList)
            {
                distance = Vector3.Distance(transform.position, obj.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestObject = obj;
                }
            }*/
    }
    void FireBullet()
    {
        for(int i=0; i<firePoints.Length; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoints[i].position, firePoints[i].rotation);
            bullet.GetComponent<ShootingBullet>().target = target;
            bullet.GetComponent<ShootingBullet>().damage = 5 * playerStats.normalGunDamageValue;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 bulletVelocity = firePoints[i].up * bulletSpeed;
            rb.velocity = bulletVelocity;
        }

    }
    void FireFirstWave()
    {
        GameObject bullet1 = Instantiate(bulletPrefab, firePoints[0].position, firePoints[0].rotation);
        bullet1.GetComponent<ShootingBullet>().target = target;
        bullet1.GetComponent<ShootingBullet>().damage = 5 * playerStats.normalGunDamageValue;
        Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
        Vector2 bulletVelocity1 = firePoints[0].up * bulletSpeed;
        rb1.velocity = bulletVelocity1;

        GameObject bullet2 = Instantiate(bulletPrefab, firePoints[2].position, firePoints[2].rotation);
        bullet2.GetComponent<ShootingBullet>().target = target;
        bullet2.GetComponent<ShootingBullet>().damage = 5 * playerStats.normalGunDamageValue;
        Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
        Vector2 bulletVelocity2 = firePoints[2].up * bulletSpeed;
        rb2.velocity = bulletVelocity2;
    }
    void FireSecondWave()
    {
        GameObject bullet1 = Instantiate(bulletPrefab, firePoints[4].position, firePoints[4].rotation);
        bullet1.GetComponent<ShootingBullet>().target = target;
        bullet1.GetComponent<ShootingBullet>().damage = 5 * playerStats.normalGunDamageValue;
        Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
        Vector2 bulletVelocity1 = firePoints[4].up * bulletSpeed;
        rb1.velocity = bulletVelocity1;

        GameObject bullet2 = Instantiate(bulletPrefab, firePoints[7].position, firePoints[7].rotation);
        bullet2.GetComponent<ShootingBullet>().target = target;
        bullet2.GetComponent<ShootingBullet>().damage = 5 * playerStats.normalGunDamageValue;
        Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
        Vector2 bulletVelocity2 = firePoints[7].up * bulletSpeed;
        rb2.velocity = bulletVelocity2;
    }
}
