using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBullet : MonoBehaviour
{
    public string target;
    private float destroyTimer = 0f;
    public float damage;
    private HpBar hpBar;

    private void Start()
    {
        hpBar = GameObject.FindObjectOfType(typeof(HpBar)) as HpBar;

    }
    private void Update()
    {
        destroyTimer += Time.deltaTime;
        if (destroyTimer >= 4f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ship") && target == "Ship")
        {
            hpBar.SetHealth(damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy") && target == "Enemy")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player") && target == "Player")
        {
            hpBar.SetHealth(damage);
            Destroy(gameObject);
        }
    }
}
