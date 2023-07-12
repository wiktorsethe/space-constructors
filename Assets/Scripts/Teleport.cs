using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    [SerializeField] private string targetScene;
    private GameObject player;
    [SerializeField] private float attractionDistance = 5f;
    [SerializeField] private float attractionForce = 10f;
    private CameraShake camShake;
    private CameraSize camSize;
    private Camera mainCamera;
    private bool sizeChanged = false;
    private float collisionTime = -1;
    public int gravity;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camShake = GameObject.FindObjectOfType(typeof(CameraShake)) as CameraShake;
        camSize = GameObject.FindObjectOfType(typeof(CameraSize)) as CameraSize;
        mainCamera = Camera.main;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ship")
        {
            SceneManager.LoadScene(targetScene);
        }
    }
    private void Update()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if(distance < attractionDistance)
        {
            if(collisionTime == -1)
            {
                collisionTime = Mathf.Sqrt(2f * distance / attractionForce);
            }
            int vibrato = (int)(attractionDistance - distance);
            player.GetComponent<Rigidbody2D>().AddForce(transform.position.normalized * attractionForce);
            camShake.ShakeCamera(3f, 0.5f, vibrato);
            float targetSize = mainCamera.orthographicSize + 3f;
            if (!sizeChanged)
            {
                camSize.CamSize(targetSize, collisionTime);
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
}
