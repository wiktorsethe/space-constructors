using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    [Header("Other Scripts")]
    public GameManager gameManager;
    public CameraSize camSize;
    [Space(20f)]

    [Header("Objects")]
    public Transform playerShip;
    private Camera mainCamera;
    [Space(20f)]

    [Header("Variables")]
    public float speed = 5f;
    private float bufferDistance = 1f;

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
        camSize = GameObject.FindObjectOfType(typeof(CameraSize)) as CameraSize;
        gameManager.AddToTrashList(gameObject);
        mainCamera = Camera.main;


        playerShip = GameObject.FindGameObjectWithTag("Player").transform;

        Vector3 direction = playerShip.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += CalculateAngleOffset(direction.magnitude, 2f);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (IsObjectOutsideScreen())
        {
            Destroy(gameObject);
        }
    }
    private bool IsObjectOutsideScreen()
    {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);

        bool isOutsideScreen = screenPoint.x < -bufferDistance ||
                               screenPoint.x > 1 + bufferDistance ||
                               screenPoint.y < -bufferDistance ||
                               screenPoint.y > 1 + bufferDistance;

        return isOutsideScreen;
    }
    private float CalculateAngleOffset(float distance, float offsetDistance)
    {
        float minDistance = 0.01f;
        float offsetAngle = Mathf.Asin(offsetDistance / Mathf.Max(distance, minDistance)) * Mathf.Rad2Deg;
        return offsetAngle;
    }
}
