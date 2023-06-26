using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraSize : MonoBehaviour
{
    [Header("Objects")]
    public Transform parentObject;
    public Transform shipCenter;
    private Camera mainCamera;
    [Space(20f)]

    [Header("Variables")]
    public float shipX;
    public float shipY;
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

            shipX = parentBounds.size.x;
            shipY = parentBounds.size.y;
            float objectHeight = parentBounds.size.y * 2;

            float objectWidth = parentBounds.size.x * 2;
            float distance = (objectHeight / 2f) / Mathf.Tan(mainCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);

            Vector3 cameraPosition = shipCenter.position - mainCamera.transform.forward * distance;
            mainCamera.transform.position = cameraPosition;

            float frustumHeight = 2f * distance * Mathf.Tan(mainCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
            float frustumWidth = frustumHeight * mainCamera.aspect;

            if (objectWidth > objectHeight)
            {
                mainCamera.orthographicSize = objectWidth * 0.5f;
                if (mainCamera.orthographicSize <= 5f)
                {
                    mainCamera.orthographicSize = 5f;
                }
            }
            else
            {
                mainCamera.orthographicSize = objectHeight * 0.5f;
                if(mainCamera.orthographicSize <= 5f)
                {
                    mainCamera.orthographicSize = 5f;
                }
            }
        }
    }

    public Bounds CalculateParentBounds()
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
