using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public FloatingJoystick movementJoystick;
    public FloatingJoystick rotationJoystick;
    private Rigidbody2D rb;
    [Header("Variables")]
    [SerializeField] private float rotationSpeed;
    private Vector2 moveVector;
    public PlayerStats playerStats;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        movementJoystick = GameObject.Find("Movement JoyStick").GetComponent<FloatingJoystick>();
        rotationJoystick = GameObject.Find("Rotation JoyStick").GetComponent<FloatingJoystick>();
    }
    private void Update()
    {
        Move();
        Rotate();
    }
    private void Move()
    {
        moveVector = Vector2.zero;
        moveVector.x = movementJoystick.Horizontal * playerStats.shipSpeedValue * Time.deltaTime;
        moveVector.y = movementJoystick.Vertical * playerStats.shipSpeedValue * Time.deltaTime;
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
