using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBullet : MonoBehaviour
{
    public string target;
    private float destroyTimer = 0f;
    private void Update()
    {
        destroyTimer += Time.deltaTime;
        if (destroyTimer >= 4f)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(target))
        {
            Destroy(other.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);

        if (collision.gameObject.CompareTag(target))
        {
            Destroy(collision.gameObject);
        }
    }
}
