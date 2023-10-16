using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingHomingGun : MonoBehaviour
{
    [Header("Other Scripts")]
    public PlayerStats playerStats;
    private ObjectPool objPool;
    [Space(20f)]

    [Header("Objects and List")]
    [SerializeField] private Animator shootAnimator;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] targets;
    [Space(20f)]

    [Header("Variables")]
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private string target;
    [SerializeField] private float distance;
    public float shootTimer = 0f;

    private void Start()
    {
        objPool = GetComponent<ObjectPool>();
    }
    void Update()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= playerStats.homingGunAttackSpeedValue)
        {
            FireBullet();
            shootTimer = 0f;
        }

    }
    void FireBullet()
    {
        targets = GameObject.FindGameObjectsWithTag("Enemy");
        if (targets.Length > 0)
        {
            shootAnimator.SetTrigger("Play");
            GameObject bullet = objPool.GetPooledObject();
            bullet.transform.position = firePoint.position;
            bullet.GetComponent<ShootingBullet>().startingPos = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;
            bullet.GetComponent<ShootingBullet>().target = target;
            bullet.GetComponent<ShootingBullet>().damage = playerStats.homingGunDamageValue;
            bullet.GetComponent<ShootingBombBullet>().ChangeSize();
            float closestDistance = Mathf.Infinity;
            GameObject closestObject = null;
            foreach (GameObject obj in targets)
            {
                distance = Vector3.Distance(transform.position, obj.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestObject = obj;
                }
            }
            bullet.GetComponent<ShootingBullet>().targetT = closestObject.transform;
            bullet.SetActive(true);
        }
    }
}
