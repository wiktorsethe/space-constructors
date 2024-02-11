using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBombBullet : MonoBehaviour
{
    public string type;
    public string target;
    public float damage;
    private HpBar hpBar;
    [SerializeField] private GameObject bombParticles;
    private CameraShake camShake;
    private Camera cam;
    public Vector2 startingPos;
    private float cameraHalfWidth;
    public bool isBombGunShoot = false;
    public float speed = 5f;
    private Rigidbody2D rb;
    private bool isBombTime = false;
    private float camHeight;
    // Start is called before the first frame update
    private void Start()
    {
        hpBar = GameObject.FindObjectOfType(typeof(HpBar)) as HpBar;
        camShake = GameObject.FindObjectOfType(typeof(CameraShake)) as CameraShake;
        cam = Camera.main;
        cameraHalfWidth = cam.orthographicSize * cam.aspect;

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 objectPosition = transform.position;

        Vector3 cameraViewportPoint = cam.WorldToViewportPoint(objectPosition);
        float distanceX = Mathf.Abs((cameraViewportPoint.x - 0.5f) * cam.orthographicSize * cam.aspect * 2);
        float distanceY = Mathf.Abs((cameraViewportPoint.y - 0.5f) * cam.orthographicSize * 2);

        if (rb.velocity.magnitude > 0.0f)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2f);
            foreach (Collider2D col in colliders)
            {
                if(col.tag == target)
                {
                    rb.velocity = Vector2.zero;
                    if (!isBombTime)
                    {
                        Instantiate(bombParticles, transform.position, Quaternion.identity);
                        col.GetComponentInParent<EnemyShip>().CollisionDetected((int)damage);
                        isBombTime = true;
                    }
                    
                }

                //if (col.GetComponentInParent<EnemyShip>() != null && col.tag == target) { col.GetComponentInParent<EnemyShip>().CollisionDetected((int)damage); gameObject.SetActive(false); }
                //if (col.tag == "Ship" && col.tag == target) { hpBar.SetHealth(10); gameObject.SetActive(false); }
            }
            rb.velocity -= rb.velocity.normalized * new Vector2(distanceX, distanceY).magnitude * 0.7f * Time.deltaTime;
            if (rb.velocity.magnitude <= 0.005f)
            {
                rb.velocity = Vector2.zero;
                Instantiate(bombParticles, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
            }
        }

        /*
        if (type == "BombBullet" && !isBombGunShoot)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2f);
            foreach (Collider2D col in colliders)
            {
                if (col.GetComponentInParent<EnemyShip>() != null && col.tag == target) { col.GetComponentInParent<EnemyShip>().CollisionDetected((int)damage); gameObject.SetActive(false); }
                if (col.tag == "Ship" && col.tag == target) { hpBar.SetHealth(10); gameObject.SetActive(false); }
            }
            isBombGunShoot = true;
            Instantiate(shootParticles, transform.position, Quaternion.identity);
            camShake.ShakeCamera(0.2f, 0.5f, 2);
        }*/
    }
    private IEnumerator Boom()
    {

        yield return new WaitForSeconds(3f);
    }
    public void ChangeSize()
    {
        Camera mainCamera = Camera.main;
        float orthographicSize = mainCamera.orthographicSize;
        float currentSize = orthographicSize * 2f;
        float scaleFactor = currentSize / 24;
        transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
    }
}
