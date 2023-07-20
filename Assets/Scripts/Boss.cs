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
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        specialActionTimer += Time.deltaTime;
        shootTimer += Time.deltaTime;

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
        else if(specialActionTimer >= 8f && specialActionTimer < 20f)
        {
            if (!isPositionSelected)
            {
                dashPosition = player.transform.position;
            }
            Vector3 vectorToTarget = dashPosition - transform.position;
            //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime); //ciekawe rzeczy jak weŸmiemy .right
            //transform.Find("EnemyShipImage").transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 5f * Time.deltaTime);
        }
        else if(specialActionTimer >= 20f)
        {
            specialActionTimer = 0f;
        }


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
}
