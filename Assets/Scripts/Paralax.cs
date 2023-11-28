using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    private float lengthX;
    private float lengthY;
    private float startPosX;
    private float startPosY;
    private Camera mainCam;
    [SerializeField] private float parallaxEffect;
    private void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        mainCam = Camera.main;

        StartingParallax();
    }
    public void StartingParallax()
    {
        
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
        lengthY = GetComponent<SpriteRenderer>().bounds.size.y;

    }
    private void Update()
    {
        float tempX = (mainCam.transform.position.x * (1 - parallaxEffect));
        float distX = (mainCam.transform.position.x * parallaxEffect);

        float tempY = (mainCam.transform.position.y * (1 - parallaxEffect));
        float distY = (mainCam.transform.position.y * parallaxEffect);

        transform.position = new Vector3(startPosX + distX, startPosY + distY, transform.position.z);

        if (tempX > startPosX + lengthX) { startPosX += lengthX; }
        else if (tempX < startPosX - lengthX) { startPosX -= lengthX; }

        if (tempY > startPosY + lengthY) { startPosY += lengthY; }
        else if (tempY < startPosY - lengthY) { startPosY -= lengthY; }
    }
}
