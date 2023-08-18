using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingHomingBullet : MonoBehaviour
{
    public string type = "NormalBullet";
    public Transform targetT;
    private float speed = 10;
    private float rotateSpeed = 200;
    private Rigidbody2D rb;
    public string target;
    public float damage;
    private HpBar hpBar;
    private CameraShake camShake;
    private Camera cam;
    public Vector2 startingPos;
    private float cameraHalfWidth;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hpBar = GameObject.FindObjectOfType(typeof(HpBar)) as HpBar;
        camShake = GameObject.FindObjectOfType(typeof(CameraShake)) as CameraShake;
        cam = Camera.main;
        cameraHalfWidth = cam.orthographicSize * cam.aspect;

        float cameraHeight = cam.orthographicSize * 0.13f;
        float cameraWidth = cameraHeight * cam.aspect;

        float scaleX = cameraWidth / transform.localScale.x;
        float scaleY = cameraHeight / transform.localScale.y;

        float objectScale = Mathf.Min(scaleX, scaleY);
        transform.localScale = new Vector3(objectScale, objectScale, 1f);
    }
    private void Update()
    {
        Vector2 direction = (Vector2)targetT.position - (Vector2)transform.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.velocity = transform.up * speed;

        if (Vector2.Distance(startingPos, transform.position) > cameraHalfWidth)
        {
            gameObject.SetActive(false);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ship") && target == "Ship")
        {
            GameObject particle = GetComponent<ObjectPool>().GetPooledObject();
            particle.transform.position = transform.position;
            particle.SetActive(true);
            camShake.ShakeCamera(0.2f, 0.5f, 2);
            hpBar.SetHealth(damage);
            gameObject.SetActive(false);

        }
        else if (collision.gameObject.CompareTag("Enemy") && target == "Enemy")
        {
            GameObject particle = GetComponent<ObjectPool>().GetPooledObject();
            particle.transform.position = transform.position;
            particle.SetActive(true);
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Player") && target == "Player")
        {
            GameObject particle = GetComponent<ObjectPool>().GetPooledObject();
            particle.transform.position = transform.position;
            particle.SetActive(true);
            hpBar.SetHealth(damage);
            gameObject.SetActive(false);
        }
    }
}
