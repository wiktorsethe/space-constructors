using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    [Header("Other Scripts")]
    private CameraShake camShake;
    private CameraSize camSize;
    public PlayerStats playerStats;
    private GameManager gameManager;
    private BackgroundScaler bgScaler;
    private ShipMovement shipMovement;
    [Space(20f)]
    [Header("Variables")]
    [SerializeField] private float attractionDistance = 5f;
    [SerializeField] private float attractionForce = 10f;
    [SerializeField] private string targetScene;
    private bool sizeChanged = false;
    private bool isFirstTimeChanged = false;
    private float collisionTime = -1;
    private float distance;
    public int gravity;
    private Vector3 size;
    [Space(20f)]
    [Header("Other")]
    private GameObject player;
    private Camera mainCamera;
    [SerializeField] private Transform safeSpawn;
    [SerializeField] private Animator transition;
    [SerializeField] private GameObject levelLoader;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camShake = GameObject.FindObjectOfType(typeof(CameraShake)) as CameraShake;
        camSize = GameObject.FindObjectOfType(typeof(CameraSize)) as CameraSize;
        gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
        bgScaler = GameObject.FindObjectOfType(typeof(BackgroundScaler)) as BackgroundScaler;
        shipMovement = GameObject.FindObjectOfType(typeof(ShipMovement)) as ShipMovement;
        mainCamera = Camera.main;

        SpriteRenderer meshRender = GetComponent<SpriteRenderer>();
        Bounds bounds = meshRender.bounds;
        size = bounds.size;
        transition = GameObject.Find("Loader").GetComponent<Animator>();
        levelLoader = GameObject.Find("Loader");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ship" && gravity <= playerStats.shipGravity)
        {
            playerStats.shipPosition = safeSpawn.position;
            PlayerPrefs.SetFloat("BestTimeTimer", gameManager.bestTimeTimer);
            PlayerPrefs.SetInt("Kills", gameManager.kills);
            PlayerPrefs.SetInt("GoldEarned", gameManager.goldEarned);
            StartCoroutine("LoadLevel");
        }
        else if(collision.transform.tag == "Ship" && gravity > playerStats.shipGravity)
        {
            //Debug.Log("1");
            playerStats.shipCurrentHealth -= playerStats.shipMaxHealth;
        }
    }
    private void Update()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            distance = Vector3.Distance(player.transform.position, transform.position);
        }
        
        if (distance <= attractionDistance && player)
        {
            if(collisionTime == -1)
            {
                collisionTime = Mathf.Sqrt(2f * distance / attractionForce);
            }
            GetComponent<PlanetRotate>().rotatingSpeed = 0f;
            int vibrato = (int)(attractionDistance - distance);
            shipMovement.enabled = false;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Vector3 direction = transform.position - player.transform.position;
            player.GetComponent<Rigidbody2D>().AddForce(direction * attractionForce);
            camShake.ShakeCamera(3f, 0.5f, vibrato);
            float targetSize = mainCamera.orthographicSize + size.x;
            if (!sizeChanged)
            {
                camSize.CamSize(targetSize, collisionTime * 2);
                bgScaler.SmoothChangeScale(targetSize * 1.2f);
                sizeChanged = true;
                player.GetComponent<ShipMovement>().movementJoystick.enabled = false;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        DrawCircle(transform.position, attractionDistance, 32);
    }

    private void DrawCircle(Vector3 center, float radius, int segments)
    {
        float angle = 0f;
        float angleIncrement = 2f * Mathf.PI / segments;

        Vector3 previousPoint = center + new Vector3(radius, 0f, 0f);
        for (int i = 0; i <= segments; i++)
        {
            float x = center.x + Mathf.Cos(angle) * radius;
            float y = center.y + Mathf.Sin(angle) * radius;
            float z = center.z;

            Vector3 currentPoint = new Vector3(x, y, z);

            Gizmos.DrawLine(previousPoint, currentPoint);
            previousPoint = currentPoint;
            angle += angleIncrement;
        }
    }
    public void ChangeAttractionSize()
    {
        Vector2 rozmiar = GetComponent<SpriteRenderer>().bounds.size;
        // Oblicz œredni¹ wielkoœæ obiektu (œrednia z szerokoœci i wysokoœci)
        float sredniaWielkosc = (rozmiar.x + rozmiar.y) / 2f;
        attractionDistance = sredniaWielkosc * 1.1f;
    }
    private IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(targetScene);
    }
}
