using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WalkerMovement : MonoBehaviour
{
    public FloatingJoystick movementJoystick;
    public FloatingJoystick rotationJoystick;
    private Rigidbody2D rb;
    [Header("Variables")]
    [SerializeField] public float moveSpeed;
    [SerializeField] private float rotationSpeed;
    private Vector2 moveVector;

    private float distanceThreshold = 10f;
    [SerializeField] private GameObject arrowPrefab;
    public GameObject arrow;
    [SerializeField] private float arrowDistance = 2f;
    private GameObject choosenTarget;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        choosenTarget = GameObject.Find("MainFragment");
    }
    private void Update()
    {
        Move();
        Rotate();

        if (choosenTarget && Vector3.Distance(choosenTarget.transform.position, transform.position) >= distanceThreshold)
        {
            Vector3 direction = (choosenTarget.transform.position - transform.position).normalized;
            //Debug.DrawRay(transform.position, direction * 10f, Color.red);
            if (!arrow)
            {
                arrow = Instantiate(arrowPrefab, transform);
            }
            arrow.transform.position = transform.position + direction * arrowDistance;

            Vector3 targetDir = direction;
            targetDir.z = 0f;
            arrow.transform.rotation = Quaternion.LookRotation(Vector3.forward, targetDir);

        }
        else if (choosenTarget && Vector3.Distance(choosenTarget.transform.position, transform.position) < distanceThreshold && arrow != null)
        {
            Destroy(arrow);
            arrow = null;
        }

    }
    private void Move()
    {
        moveVector = Vector2.zero;
        moveVector.x = movementJoystick.Horizontal * moveSpeed * Time.deltaTime;
        moveVector.y = movementJoystick.Vertical * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + moveVector);

    }
    private void Rotate()
    {
        float horizontalInput = rotationJoystick.Horizontal;
        float verticalInput = rotationJoystick.Vertical;

        if (horizontalInput != 0f || verticalInput != 0f)
        {
            float targetAngle = Mathf.Atan2(horizontalInput, verticalInput) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, -targetAngle);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

}
