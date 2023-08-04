using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBullet : MonoBehaviour
{
    public string target;
    private float destroyTimer = 0f;
    public float damage;
    private HpBar hpBar;
    [SerializeField] GameObject shootParticles;
    private CameraShake camShake;

    private void Start()
    {
        hpBar = GameObject.FindObjectOfType(typeof(HpBar)) as HpBar;
        camShake = GameObject.FindObjectOfType(typeof(CameraShake)) as CameraShake;
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
            Instantiate(shootParticles, transform.position, Quaternion.identity);
            camShake.ShakeCamera(0.2f, 0.5f, 2);
            hpBar.SetHealth(damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy") && target == "Enemy")
        {
            Instantiate(shootParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player") && target == "Player")
        {
            Instantiate(shootParticles, transform.position, Quaternion.identity);
            camShake.ShakeCamera(0.2f, 0.5f, 2);
            hpBar.SetHealth(damage);
            Destroy(gameObject);
        }
    }
}
