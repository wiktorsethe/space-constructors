using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBullet : MonoBehaviour
{
    public string target;
    public float damage;
    private HpBar hpBar;
    [SerializeField] private GameObject shootParticles;
    private CameraShake camShake;
    private Camera cam;
    private Vector2 startingPos;
    private float cameraHalfWidth;
    private void Start()
    {
        hpBar = GameObject.FindObjectOfType(typeof(HpBar)) as HpBar;
        camShake = GameObject.FindObjectOfType(typeof(CameraShake)) as CameraShake;
        cam = Camera.main;
        startingPos = transform.position;
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
        if(Vector2.Distance(startingPos, transform.position) > cameraHalfWidth)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ship") && target == "Ship")
        {
            Instantiate(shootParticles, transform.position, Quaternion.identity);
            camShake.ShakeCamera(0.2f, 0.5f, 2);
            hpBar.SetHealth(damage);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Enemy") && target == "Enemy")
        {
            Instantiate(shootParticles, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Player") && target == "Player")
        {
            Instantiate(shootParticles, transform.position, Quaternion.identity);
            hpBar.SetHealth(damage);
            gameObject.SetActive(false);
        }
    }
}
