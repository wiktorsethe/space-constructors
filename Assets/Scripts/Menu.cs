using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("Other Scripts")]
    private ShipManager shipManager;
    [Space(20f)]

    [Header("Objects")]
    public GameObject constructMenu;
    public GameObject gameMenu;
    public GameObject pauseMenu;
    public GameObject shipPartMenuPrefab;
    [Space(20f)]

    [Header("Lists")]
    public GameObject[] constructPoints;
    public List<GameObject> shipPartsInstantiate = new List<GameObject>();

    private void Start()
    {
        shipManager = GameObject.FindObjectOfType(typeof(ShipManager)) as ShipManager;

    }
    public void ConstructMenu()
    {
        constructMenu.SetActive(true);


        for (int i = 0; i < shipPartsInstantiate.Count; i++)
        {
            Destroy(shipPartsInstantiate[i]);
        }
        shipPartsInstantiate.Clear();
        for (int i = 0; i < shipManager.activeConstructPoint.GetComponent<ShipPart>().shipPrefabList.Count; i++)
        {
            GameObject obj = Instantiate(shipPartMenuPrefab, constructMenu.transform.Find("Panel").transform);
            obj.GetComponent<ShipPartMenu>().index = i;
            shipPartsInstantiate.Add(obj);
            obj.GetComponent<Button>().onClick.AddListener(() => shipManager.NewPart(obj.GetComponent<ShipPartMenu>().index));
        }
    }
    public void PauseMenu()
    {
        shipManager.RotateToCenter();
        shipManager.MoveObj();
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        gameMenu.SetActive(false);
        constructPoints = GameObject.FindGameObjectsWithTag("ConstructPoint");
        for(int i=0; i<constructPoints.Length; i++)
        {
            constructPoints[i].GetComponent<Image>().enabled = true;
            constructPoints[i].GetComponent<Button>().enabled = true;
        }
    }
    public void Resume()
    {
        constructMenu.SetActive(false);
        gameMenu.SetActive(true);
        pauseMenu.SetActive(false);

        constructPoints = GameObject.FindGameObjectsWithTag("ConstructPoint");
        Time.timeScale = 1f;
        for (int i = 0; i < constructPoints.Length; i++)
        {
            constructPoints[i].GetComponent<Image>().enabled = false;
            constructPoints[i].GetComponent<Button>().enabled = false;
        }
    }
    public void ExitConstructMenu()
    {
        constructMenu.SetActive(false);
    }


}
