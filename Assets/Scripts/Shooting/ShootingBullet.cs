using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBullet : MonoBehaviour
{
    public string type;
    public string target;
    public float damage;
    private HpBar hpBar;
    private CameraShake camShake;
    private Camera cam;
    public Vector2 startingPos;
    private float cameraHalfWidth;
    public bool isBombGunShoot = false;
    private Rigidbody2D rb;
    public Transform targetT;
    private float speed = 10;
    private float rotateSpeed = 200;
    private float camHeight;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hpBar = GameObject.FindObjectOfType(typeof(HpBar)) as HpBar;
        camShake = GameObject.FindObjectOfType(typeof(CameraShake)) as CameraShake;
        cam = Camera.main;
        cameraHalfWidth = cam.orthographicSize * cam.aspect;
    }
    private void Update()
    {
        if (type == "HomingBullet")
        {
            Vector2 direction = (Vector2)targetT.position - (Vector2)transform.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            rb.angularVelocity = -rotateAmount * rotateSpeed;
            rb.velocity = transform.up * speed;
        }

        if (Vector2.Distance(startingPos, transform.position) > cameraHalfWidth) // tu jest b³¹d kule sie nie pokazuja trzeba przerobiæ startingpos
        {
            gameObject.SetActive(false);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject particle = GetComponent<ObjectPool>().GetPooledObject();
        if (collision.gameObject.CompareTag("Ship") && target == "Ship")
        {
            particle.transform.position = transform.position;
            particle.SetActive(true);
            camShake.ShakeCamera(0.2f, 0.5f, 2);

            if(type == "PoisonBullet")
            {
                hpBar.StartPoison();
            }
            else if (type == "NormalBullet" || type == "HomingBullet")
            {
                hpBar.SetHealth(damage);
            }
            if (type == "FlameBullet")
            {
                hpBar.StartFlame();
            }
            gameObject.SetActive(false);
            
        }
        else if (collision.gameObject.CompareTag("Enemy") && target == "Enemy")
        {
            particle.transform.position = transform.position;
            particle.SetActive(true);
            if(type != "StunningBullet")
            {
                gameObject.SetActive(false);
            }
        }
        else if (collision.gameObject.CompareTag("Player") && target == "Player")
        {
            particle.transform.position = transform.position;
            particle.SetActive(true);
            hpBar.SetHealth(damage);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Pillar") && target == "Enemy")
        {
            particle.transform.position = transform.position;
            particle.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    public void ChangeSize()
    {
        Camera cam = Camera.main;
        float cameraHeight = cam.orthographicSize * 0.13f;
        float cameraWidth = cameraHeight * cam.aspect;
        if (cameraHeight != camHeight)
        {
            camHeight = cameraHeight;
            float scaleX = cameraWidth / transform.localScale.x;
            float scaleY = cameraHeight / transform.localScale.y;

            float objectScale = Mathf.Min(scaleX, scaleY);
            transform.localScale = new Vector3(objectScale, objectScale, 1f);
        }
    }
}
