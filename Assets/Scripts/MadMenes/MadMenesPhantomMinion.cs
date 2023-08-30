using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadMenesPhantomMinion : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private ObjectPool[] objPools;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private string target;
    private void Start()
    {
        objPools = GetComponents<ObjectPool>();
    }
    public void FireBullet()
    {
        foreach (ObjectPool script in objPools)
        {
            if (script.type == "bullet")
            {
                GameObject bullet = script.GetPooledObject();
                bullet.transform.position = firePoint.position;
                bullet.GetComponent<ShootingBullet>().startingPos = firePoint.position;
                bullet.transform.rotation = firePoint.rotation;
                bullet.SetActive(true);
                bullet.GetComponent<ShootingBullet>().target = target;
                bullet.GetComponent<ShootingBullet>().damage = 1;
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                Vector2 bulletVelocity = firePoint.up * bulletSpeed;
                rb.velocity = bulletVelocity;
            }
        }
    }
    public void ChangeRotation()
    {
        GameObject ship = FindClosestObject();
        Vector3 vectorToTarget = ship.transform.position - animator.transform.position;
        animator.transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget);
    }
    public GameObject FindClosestObject()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Ship");

        if (objectsWithTag.Length == 0)
        {
            return null;
        }
        float closestDistance = Mathf.Infinity;
        GameObject closestObject = null;

        foreach (GameObject obj in objectsWithTag)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = obj;
            }
        }

        return closestObject;
    }
}
