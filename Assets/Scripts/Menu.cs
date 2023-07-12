using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using DG.Tweening;
using Random = UnityEngine.Random;

public class Menu : MonoBehaviour
{
    [Header("Other Scripts")]
    private ShipManager shipManager;
    public PlayerStats playerStats;
    private ShootingNormalGun[] shootingNormalGunList;
    private ShootingLaserGun[] shootingLaserGunList;
    private ShootingBigGun[] shootingBigGunList;
    private ShootingDoubleGun[] shootingDoubleGunList;
    public CardsDatabase cardsDB;
    public ShipPartsDatabase shipPartsDB;
    [Space(20f)]

    [Header("Objects")]
    public GameObject constructMenu;
    public GameObject gameMenu;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public GameObject cardsMenu;
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
    public List<GameObject> cards = new List<GameObject>();
    public List<int> generatedCardsIndexes = new List<int>();
    public List<Card> generatedCards = new List<Card>();
    private void Start()
    {
        shipManager = GameObject.FindObjectOfType(typeof(ShipManager)) as ShipManager;
        constructPoints = GameObject.FindGameObjectsWithTag("ConstructPoint");
        for (int i = 0; i < constructPoints.Length; i++)
        {
            constructPoints[i].GetComponent<Image>().enabled = false;
            constructPoints[i].GetComponent<Button>().enabled = false;
        }
    }
    private void Update()
    {
        //shipManager = GameObject.FindObjectOfType(typeof(ShipManager)) as ShipManager;
        goldText.text = "Gold: " + playerStats.gold.ToString();
    }
    public void ConstructMenu()
    {
        constructMenu.SetActive(true);
        shootingNormalGunList = MonoBehaviour.FindObjectsOfType<ShootingNormalGun>();
        shootingLaserGunList = MonoBehaviour.FindObjectsOfType<ShootingLaserGun>();
        shootingBigGunList = MonoBehaviour.FindObjectsOfType<ShootingBigGun>();
        shootingDoubleGunList = MonoBehaviour.FindObjectsOfType<ShootingDoubleGun>();
        foreach(ShootingNormalGun script in shootingNormalGunList)
        {
            script.shootTimer = 0f;
        }
        foreach (ShootingLaserGun script in shootingLaserGunList)
        {
            script.shootTimer = 0f;
        }
        foreach (ShootingBigGun script in shootingBigGunList)
        {
            script.shootTimer = 0f;
        }
        foreach (ShootingDoubleGun script in shootingDoubleGunList)
        {
            script.shootTimer = 0f;
        }
        for (int i = 0; i < shipPartsInstantiate.Count; i++)
        {
            Destroy(shipPartsInstantiate[i]);
        }
        shipPartsInstantiate.Clear();
        for (int i = 0; i < shipPartsDB.shipParts.Length; i++)
        {
            if(shipPartsDB.shipParts[i].amount > 0 && shipManager.activeConstructPoint.GetComponent<ConstructPoint>().shipPartType != shipPartsDB.shipParts[i].shipPartType)
            {
                GameObject obj = Instantiate(shipPartMenuPrefab, constructMenu.transform.Find("Panel").transform);
                //obj.GetComponent<RectTransform>().DOAnchorPos(new Vector2(397f, 0f), 0.1f).SetUpdate(UpdateType.Normal, true);
                obj.GetComponent<ShipPartMenu>().index = i;
                obj.transform.Find("Image").GetComponent<Image>().sprite = shipPartsDB.shipParts[i].image;
                obj.transform.Find("CostText").GetComponent<TMP_Text>().text = shipPartsDB.shipParts[i].cost.ToString();
                obj.transform.Find("AmountText").GetComponent<TMP_Text>().text = shipPartsDB.shipParts[i].amount.ToString();
                shipPartsInstantiate.Add(obj);
                obj.GetComponent<Button>().onClick.AddListener(() => shipManager.NewPart(obj.GetComponent<ShipPartMenu>().index));
            }
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
    public void CardMenu()
    {
        Time.timeScale = 0f;
        cardsMenu.SetActive(true);
        constructMenu.SetActive(false);
        gameMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        for(int i=0; i<cards.Count; i++)
        {
            int randIndex = Random.Range(0, cardsDB.cards.Length);
            while (generatedCardsIndexes.Contains(randIndex))
            {
                randIndex = Random.Range(0, cardsDB.cards.Length);
            }
            generatedCardsIndexes.Add(randIndex);
            cards[i].transform.Find("DescriptionText").GetComponent<TMP_Text>().text = cardsDB.cards[randIndex].description;
            cards[i].transform.Find("Image").GetComponent<Image>().sprite = cardsDB.cards[randIndex].image;
            generatedCards.Add(cardsDB.cards[randIndex]);
        }
    }
    public void ChooseCard(int i)
    {
        generatedCardsIndexes.Clear();
        playerStats.normalGunDamageValue += generatedCards[i].normalGunDamageValue;
        playerStats.laserGunDamageValue += generatedCards[i].laserGunDamageValue;
        playerStats.bigGunDamageValue += generatedCards[i].bigGunDamageValue;
        playerStats.doubleGunDamageValue += generatedCards[i].doubleGunDamageValue;
        playerStats.normalGunAttackSpeedValue += generatedCards[i].normalGunAttackSpeedValue;
        playerStats.laserGunAttackSpeedValue += generatedCards[i].laserGunAttackSpeedValue;
        playerStats.bigGunAttackSpeedValue += generatedCards[i].bigGunAttackSpeedValue;
        playerStats.doubleGunAttackSpeedValue += generatedCards[i].doubleGunAttackSpeedValue;
        playerStats.shipSpeedValue += generatedCards[i].shipSpeedValue;
        generatedCards.Clear();
        cardsMenu.SetActive(false);
        constructMenu.SetActive(false);
        gameMenu.SetActive(true);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        Time.timeScale = 1f;
    }

}
