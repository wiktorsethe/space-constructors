using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotate : MonoBehaviour
{
    public float rotatingSpeed;
    [SerializeField] private float rotatingLocalSpeed;
    [SerializeField] private GameObject pivotObject;

    private void Update()
    {
        if (pivotObject)
        {
            transform.RotateAround(pivotObject.transform.position, Vector3.forward, rotatingSpeed * Time.deltaTime);
        }

        float currentRotation = transform.rotation.eulerAngles.z;
        float newRotation = currentRotation + (rotatingLocalSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, 0f, newRotation);
    }
}
