using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 1f;

    private void Start()
    {
        smoothSpeed = 1e+17f; // Natychmiastowe œledzenie
    }
    void Update()
    {
        if (target)
        {
            // Obliczenie docelowej pozycji kamery
            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            // Wyg³adzenie ruchu kamery za pomoc¹ funkcji Lerp
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            // Przypisanie pozycji kamery
            transform.position = smoothedPosition;
        }
    }
}
