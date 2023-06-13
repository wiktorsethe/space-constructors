using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraSize : MonoBehaviour
{
    public Transform parentObject;
    public Transform shipCenter;

    private Camera mainCamera;


    private void Start()
    {
        mainCamera = Camera.main;
        ChangeCamSize();
    }
    
    public void ChangeCamSize()
    {
        if (parentObject != null)
        {
            Bounds parentBounds = CalculateParentBounds();

            // Obliczanie odleg³oœci kamery od obiektu, aby ca³y obiekt by³ widoczny
            float objectHeight = parentBounds.size.y * 5;
            float objectWidth = parentBounds.size.x * 2;
            float distance = (objectHeight / 2f) / Mathf.Tan(mainCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);

            // Ustawianie pozycji kamery na œrodek obiektu
            Vector3 cameraPosition = shipCenter.position - mainCamera.transform.forward * distance;
            mainCamera.transform.position = cameraPosition;

            // Ustawianie wielkoœci widoku kamery
            float frustumHeight = 2f * distance * Mathf.Tan(mainCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
            float frustumWidth = frustumHeight * mainCamera.aspect;

            if (objectWidth > objectHeight)
            {
                mainCamera.orthographicSize = objectWidth * 0.5f;
            }
            else
            {
                mainCamera.orthographicSize = objectHeight * 0.5f;
            }
        }
    }

    private Bounds CalculateParentBounds()
    {
        Renderer[] renderers = parentObject.GetComponentsInChildren<Renderer>();

        if (renderers.Length > 0)
        {
            Bounds bounds = renderers[0].bounds;

            for (int i = 1; i < renderers.Length; i++)
            {
                bounds.Encapsulate(renderers[i].bounds);
            }

            return bounds;
        }

        return new Bounds(Vector3.zero, Vector3.zero);
    }
}
