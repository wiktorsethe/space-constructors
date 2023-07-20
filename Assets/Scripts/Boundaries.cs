using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    private Camera mainCam;
    public GameObject barier;
    public Transform[] spawnPoints; 
    public void ActivateBariers()
    {
        mainCam = Camera.main;
        float cameraWidth = mainCam.orthographicSize * 2f * mainCam.aspect;
        float cameraHeight = mainCam.orthographicSize * 2f;
        GameObject downBarier = Instantiate(barier, spawnPoints[0].position, Quaternion.identity);
        downBarier.transform.localScale = new Vector3(cameraWidth, 1f, 1f);
        downBarier.GetComponent<BoxCollider2D>().isTrigger = false;
        GameObject topBarier = Instantiate(barier, spawnPoints[1].position, Quaternion.identity);
        topBarier.transform.localScale = new Vector3(cameraWidth, 1f, 1f);
        topBarier.GetComponent<BoxCollider2D>().isTrigger = false;
        GameObject leftBarier = Instantiate(barier, spawnPoints[2].position, Quaternion.identity);
        leftBarier.transform.localScale = new Vector3(1f, cameraHeight, 1f);
        leftBarier.GetComponent<BoxCollider2D>().isTrigger = false;
        GameObject rightBarier = Instantiate(barier, spawnPoints[3].position, Quaternion.identity);
        rightBarier.transform.localScale = new Vector3(1f, cameraHeight, 1f);
        rightBarier.GetComponent<BoxCollider2D>().isTrigger = false;
    }
    public void Check()
    {
        Invoke("ActivateBariers", 5f);
    }
}
