using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    private Camera mainCam;
    [SerializeField] private GameObject barier;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private List<GameObject> bariers = new List<GameObject>();
    public GameObject[] planets;
    public void Check()
    {
        Invoke("ActivateBariers", 5f);
        DeactivatePlanets();
    }
    public void ActivateBariers()
    {
        mainCam = Camera.main;
        float cameraWidth = mainCam.orthographicSize * 2f * mainCam.aspect;
        float cameraHeight = mainCam.orthographicSize * 2f;
        GameObject downBarier = Instantiate(barier, spawnPoints[0].position, Quaternion.identity);
        downBarier.transform.localScale = new Vector3(cameraWidth, 1f, 1f);
        downBarier.GetComponent<BoxCollider2D>().isTrigger = false;
        bariers.Add(downBarier);

        GameObject topBarier = Instantiate(barier, spawnPoints[1].position, Quaternion.identity);
        topBarier.transform.localScale = new Vector3(cameraWidth, 1f, 1f);
        topBarier.GetComponent<BoxCollider2D>().isTrigger = false;
        bariers.Add(topBarier);

        GameObject leftBarier = Instantiate(barier, spawnPoints[2].position, Quaternion.identity);
        leftBarier.transform.localScale = new Vector3(1f, cameraHeight, 1f);
        leftBarier.GetComponent<BoxCollider2D>().isTrigger = false;
        bariers.Add(leftBarier);

        GameObject rightBarier = Instantiate(barier, spawnPoints[3].position, Quaternion.identity);
        rightBarier.transform.localScale = new Vector3(1f, cameraHeight, 1f);
        rightBarier.GetComponent<BoxCollider2D>().isTrigger = false;
        bariers.Add(rightBarier);

        
    }
    public void DeactivateBariers()
    {
        foreach (GameObject barier in bariers)
        {
            Destroy(barier);
        }
    }
    public void DeactivatePlanets()
    {
        planets = GameObject.FindGameObjectsWithTag("Planet");
        foreach (GameObject planet in planets)
        {
            planet.SetActive(false);
        }
    }
    public void ActivatePlanets()
    {
        foreach (GameObject planet in planets)
        {
            planet.SetActive(true);
        }
    }
}
