using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using DG.Tweening;

public class Menu : MonoBehaviour
{
    [Header("Other Scripts")]
    private ShipManager shipManager;
    public PlayerStats playerStats;
    [Space(20f)]

    [Header("Objects")]
    public GameObject constructMenu;
    public GameObject gameMenu;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public GameObject shipPartMenuPrefab;
    [Space(20f)]

    [Header("Texts")]
    public TMP_Text bestTimeText;
    public TMP_Text mostKillsText;
    public TMP_Text mostGoldEarnedText;
    public TMP_Text goldText;
    [Space(20f)]

    [Header("Lists")]
    public GameObject[] constructPoints;
    public List<GameObject> shipPartsInstantiate = new List<GameObject>();

    private void Start()
    {
        shipManager = GameObject.FindObjectOfType(typeof(ShipManager)) as ShipManager;

    }
    private void Update()
    {
        goldText.text = "Gold: " + playerStats.gold.ToString();
    }
    public void ConstructMenu()
    {
        constructMenu.SetActive(true);


        for (int i = 0; i < shipPartsInstantiate.Count; i++)
        {
            Destroy(shipPartsInstantiate[i]);
        }
        shipPartsInstantiate.Clear();
        for (int i = 0; i < shipManager.activeConstructPoint.GetComponent<ConstructPoint>().shipPrefabList.Count; i++)
        {
            GameObject obj = Instantiate(shipPartMenuPrefab, constructMenu.transform.Find("Panel").transform);
            //obj.GetComponent<RectTransform>().DOAnchorPos(new Vector2(397f, 0f), 0.1f).SetUpdate(UpdateType.Normal, true);
            obj.GetComponent<ShipPartMenu>().index = i;
            obj.transform.Find("Image").GetComponent<Image>().sprite = shipManager.activeConstructPoint.GetComponent<ConstructPoint>().shipPrefabList[i].GetComponentInChildren<SpriteRenderer>().sprite;
            obj.transform.Find("CostText").GetComponent<TMP_Text>().text = shipManager.activeConstructPoint.GetComponent<ConstructPoint>().shipPrefabList[i].GetComponent<ShipPart>().cost.ToString();
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
        gameOverMenu.SetActive(false);
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
        gameOverMenu.SetActive(false);
        shipManager.MoveObjBack();
        constructPoints = GameObject.FindGameObjectsWithTag("ConstructPoint");
        Time.timeScale = 1f;
        for (int i = 0; i < constructPoints.Length; i++)
        {
            constructPoints[i].GetComponent<Image>().enabled = false;
            constructPoints[i].GetComponent<Button>().enabled = false;
        }
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void ExitConstructMenu()
    {
        constructMenu.SetActive(false);
    }
    public void GameOver()
    {
        constructMenu.SetActive(false);
        gameMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        TimeSpan timeSpan = TimeSpan.FromSeconds(playerStats.bestTime);
        bestTimeText.text = "Best Time: " + string.Format("{0:00}:{1:00}:{2:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        mostKillsText.text = "Most Kills: " + playerStats.mostKills.ToString();
        mostGoldEarnedText.text = "Most Gold Earned: " + playerStats.mostGoldEarned.ToString();

        Time.timeScale = 0f;
    }

}
