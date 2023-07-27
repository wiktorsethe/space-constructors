using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuMisteriousPlace : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameMenu;
    public GameObject misteriousManMenu;
    public GameObject cardPrefab;
    public PlayerStats playerStats;
    public CardsDatabase cardsDB;
    private CameraSize camSize;
    private Camera mainCam;
    public List<Card> generatedCards = new List<Card>();

    private void Start()
    {
        camSize = GameObject.FindObjectOfType(typeof(CameraSize)) as CameraSize;
        mainCam = Camera.main;
    }
    public void PauseMenu()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        gameMenu.SetActive(false);
        misteriousManMenu.SetActive(false);
    }
    public void Resume()
    {
        gameMenu.SetActive(true);
        pauseMenu.SetActive(false);
        misteriousManMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void MisteriousManMenu()
    {
        gameMenu.SetActive(false);
        pauseMenu.SetActive(false);
        misteriousManMenu.SetActive(true);
        for(int i=0; i<cardsDB.cards.Length; i++)
        {
            GameObject obj = Instantiate(cardPrefab, misteriousManMenu.transform.Find("Scroll").transform.Find("Panel").transform);
            obj.GetComponent<CardMenu>().index = i;
            obj.transform.Find("Image").GetComponent<Image>().sprite = cardsDB.cards[i].image;
            obj.transform.Find("DescriptionText").GetComponent<TMP_Text>().text = cardsDB.cards[i].description.ToString();
            generatedCards.Add(cardsDB.cards[i]);
            //shipPartsInstantiate.Add(obj);
            obj.GetComponent<Button>().onClick.AddListener(() => ChooseCard(obj.GetComponent<CardMenu>().index));
        }
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void ChooseCard(int i)
    {
        playerStats.normalGunDamageValue += generatedCards[i].normalGunDamageValue * 2;
        playerStats.laserGunDamageValue += generatedCards[i].laserGunDamageValue * 2;
        playerStats.bigGunDamageValue += generatedCards[i].bigGunDamageValue * 2;
        playerStats.doubleGunDamageValue += generatedCards[i].doubleGunDamageValue * 2;
        playerStats.normalGunAttackSpeedValue += generatedCards[i].normalGunAttackSpeedValue * 2;
        playerStats.laserGunAttackSpeedValue += generatedCards[i].laserGunAttackSpeedValue * 2;
        playerStats.bigGunAttackSpeedValue += generatedCards[i].bigGunAttackSpeedValue * 2;
        playerStats.doubleGunAttackSpeedValue += generatedCards[i].doubleGunAttackSpeedValue * 2;
        playerStats.shipSpeedValue += generatedCards[i].shipSpeedValue * 2;
        playerStats.oreMiningBonusValue = generatedCards[i].oreMiningBonus * 2;
        gameMenu.SetActive(true);
        pauseMenu.SetActive(false);
        misteriousManMenu.SetActive(false);
        Time.timeScale = 1f;
        camSize.CamSize(mainCam.orthographicSize / 3f, 5f);
        Invoke("ChangeScene", 5f);
    }
    void ChangeScene()
    {
        SceneManager.LoadScene("Universe");

    }
}
