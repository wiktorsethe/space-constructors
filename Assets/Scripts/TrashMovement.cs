using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashMovement : MonoBehaviour
{
    public Transform playerShip;
    public float speed = 5f;

    private Camera mainCamera;
    private float bufferDistance = 1f;
    private float distanceToPlayer = 4f;
    public GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
        gameManager.AddToTrashList(gameObject);
        mainCamera = Camera.main;

        playerShip = GameObject.FindGameObjectWithTag("Player").transform;

        Vector3 direction = playerShip.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += CalculateAngleOffset(direction.magnitude, distanceToPlayer);
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
