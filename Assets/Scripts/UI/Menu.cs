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
    private ShipMovement shipMovement;
    public PlayerStats playerStats;
    public ShipProgress shipProgress;
    private ShootingNormalGun[] shootingNormalGunList;
    private ShootingLaserGun[] shootingLaserGunList;
    private ShootingBigGun[] shootingBigGunList;
    private ShootingDoubleGun[] shootingDoubleGunList;
    public CardsDatabase cardsDB;
    public ShipPartsDatabase shipPartsDB;
    private SwipeInConstructionMenu swipeInMenu;
    private CameraSize camSize;
    private Camera mainCam;
    [Space(20f)]

    [Header("Objects")]
    [SerializeField] private GameObject constructMenu;
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject cardsMenu;
    [SerializeField] private GameObject bossHPBar;
    [SerializeField] private GameObject backgroundBossHPBar;
    [SerializeField] private GameObject shipPartMenuPrefab;
    [SerializeField] private GameObject cardsMenuText;
    public GameObject dashButton;
    [SerializeField] private Animator transition;
    [SerializeField] private GameObject levelLoader;
    [SerializeField] private AudioSource buttonSound;
    [Space(20f)]

    [Header("Texts")]
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text killsText;
    [SerializeField] private TMP_Text goldEarnedText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text goldText;
    [SerializeField] private TMP_Text screwText;
    [SerializeField] private TMP_Text oreText;
    [Space(20f)]

    [Header("Lists")]
    public GameObject[] constructPoints;
    [SerializeField] private List<GameObject> shipPartsInstantiate = new List<GameObject>();
    [SerializeField] private List<GameObject> cards = new List<GameObject>();
    [SerializeField] private List<GameObject> refreshes = new List<GameObject>();
    [SerializeField] private List<int> generatedCardsIndexes = new List<int>();
    [SerializeField] private List<Card> generatedCards = new List<Card>();
    private void Start()
    {
        shipManager = GameObject.FindObjectOfType(typeof(ShipManager)) as ShipManager;
        shipMovement = GameObject.FindObjectOfType(typeof(ShipMovement)) as ShipMovement;
        swipeInMenu = GameObject.FindObjectOfType(typeof(SwipeInConstructionMenu)) as SwipeInConstructionMenu;
        camSize = GameObject.FindObjectOfType(typeof(CameraSize)) as CameraSize;
        mainCam = Camera.main;
        swipeInMenu.enabled = false;
        cardsMenu.GetComponent<CanvasGroup>().alpha = 0f;
        cardsMenu.GetComponent<CanvasGroup>().interactable = false;
    }
    private void Update()
    {
        //shipManager = GameObject.FindObjectOfType(typeof(ShipManager)) as ShipManager;
        goldText.text = playerStats.gold.ToString();
        screwText.text = playerStats.screw.ToString();
        oreText.text = playerStats.ore.ToString();
        if (playerStats.refreshKey <= 0)
        {
            foreach (GameObject refresh in refreshes)
            {
                refresh.SetActive(false);
            }
        }
    }
    public void ConstructMenu()
    {
        constructMenu.GetComponent<CanvasGroup>().DOFade(1f, 0.5f).SetUpdate(UpdateType.Normal, true);
        constructMenu.GetComponent<CanvasGroup>().interactable = true;
        shootingNormalGunList = MonoBehaviour.FindObjectsOfType<ShootingNormalGun>();
        shootingLaserGunList = MonoBehaviour.FindObjectsOfType<ShootingLaserGun>();
        shootingBigGunList = MonoBehaviour.FindObjectsOfType<ShootingBigGun>();
        shootingDoubleGunList = MonoBehaviour.FindObjectsOfType<ShootingDoubleGun>();
        foreach (ShootingNormalGun script in shootingNormalGunList)
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
            if (shipPartsDB.shipParts[i].ownedAmount > 0 && shipManager.activeConstructPoint.GetComponent<ConstructPoint>().shipPartType != shipPartsDB.shipParts[i].shipPartType && shipPartsDB.shipParts[i].isOwned)
            {
                GameObject obj = Instantiate(shipPartMenuPrefab, constructMenu.transform.Find("Scroll View").transform.Find("Panel").transform);
                //obj.GetComponent<RectTransform>().DOAnchorPos(new Vector2(397f, 0f), 0.1f).SetUpdate(UpdateType.Normal, true);
                obj.GetComponent<ShipPartMenu>().index = i;
                obj.transform.Find("ImgPanel").transform.Find("cardImg").GetComponent<Image>().sprite = shipPartsDB.shipParts[i].image;
                obj.transform.Find("BuyButton").transform.Find("TxtPanel").transform.Find("oreTxt").GetComponent<TMP_Text>().text = shipPartsDB.shipParts[i].oreCost.ToString();
                obj.transform.Find("BuyButton").transform.Find("TxtPanel").transform.Find("screwTxt").GetComponent<TMP_Text>().text = shipPartsDB.shipParts[i].screwCost.ToString();
                obj.transform.Find("ImgPanel").transform.Find("Panel").transform.Find("AmountText").GetComponent<TMP_Text>().text = "x" + shipPartsDB.shipParts[i].ownedAmount.ToString();
                shipPartsInstantiate.Add(obj);
                obj.GetComponent<Button>().onClick.AddListener(() => shipManager.NewPart(obj.GetComponent<ShipPartMenu>().index));
            }
        }
        float itemWidth = constructMenu.transform.Find("Scroll View").transform.Find("Panel").transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x;

        constructMenu.transform.Find("Scroll View").transform.Find("Panel").GetComponent<RectTransform>().sizeDelta = Vector2.right * ((itemWidth + 0.5f) * shipPartsInstantiate.Count / 2f - 0.5f);
        buttonSound.Play();
    }
    public void PauseMenu()
    {
        swipeInMenu.enabled = true;
        camSize.enabled = false;
        DOTween.To(() => mainCam.orthographicSize, x => mainCam.orthographicSize = x, 4f, 1f).SetUpdate(UpdateType.Normal, true);
        shipManager.RotateToCenter();
        //shipManager.MoveObj();
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        gameMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        constructPoints = GameObject.FindGameObjectsWithTag("ConstructPoint");
        for (int i = 0; i < constructPoints.Length; i++)
        {
            constructPoints[i].GetComponent<Image>().enabled = true;
            constructPoints[i].GetComponent<Button>().enabled = true;
        }
        buttonSound.Play();
    }
    public void Resume()
    {
        swipeInMenu.enabled = false;
        camSize.enabled = true;
        camSize.ChangeCamSize();
        //constructMenu.SetActive(false);
        gameMenu.SetActive(true);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        //shipManager.MoveObjBack();
        constructPoints = GameObject.FindGameObjectsWithTag("ConstructPoint");
        Time.timeScale = 1f;
        for (int i = 0; i < constructPoints.Length; i++)
        {
            constructPoints[i].GetComponent<Image>().enabled = false;
            constructPoints[i].GetComponent<Button>().enabled = false;
        }
        buttonSound.Play();
    }
    public void Restart()
    {
        Resume();
        playerStats.Reset();
        shipProgress.Reset();
        StartCoroutine("LoadUniverse");
    }
    public void BackToMenu()
    {
        Resume();
        StartCoroutine("LoadMenu");
    }
    public void ExitConstructMenu()
    {
        constructMenu.GetComponent<CanvasGroup>().DOFade(0f, 0.5f).SetUpdate(UpdateType.Normal, true);
        constructMenu.GetComponent<CanvasGroup>().interactable = false;
        buttonSound.Play();
    }
    public void GameOver()
    {
        constructMenu.SetActive(false);
        gameMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        TimeSpan timeSpan = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("BestTimeTimer"));
        timeText.text = string.Format("{0:00}:{1:00}:{2:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        killsText.text = PlayerPrefs.GetInt("Kills").ToString();
        goldEarnedText.text = PlayerPrefs.GetInt("GoldEarned").ToString();
        scoreText.text = PlayerPrefs.GetInt("Score").ToString();

        Time.timeScale = 0f;
        buttonSound.Play();
    }
    public void CardMenu()
    {
        Time.timeScale = 0f;
        shipMovement.enabled = false;
        cardsMenuText.SetActive(true);
        cardsMenu.GetComponentInChildren<GridLayoutGroup>().enabled = true;
        for (int i = 0; i < cards.Count; i++)
        {
            if (!cards[i].activeSelf)
            {
                cards[i].SetActive(true);
            }
        }
        cardsMenu.SetActive(true);
        constructMenu.SetActive(false);
        gameMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        cardsMenu.GetComponent<CanvasGroup>().DOFade(1f, 1f).SetUpdate(UpdateType.Normal, true);
        cardsMenu.GetComponent<CanvasGroup>().interactable = true;
        for (int i = 0; i < cards.Count; i++)
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
        buttonSound.Play();
    }
    public void ChooseCard(int i)
    {
        generatedCardsIndexes.Clear();
        playerStats.normalGunDamageValue += generatedCards[i].normalGunDamageValue;
        playerStats.laserGunDamageValue += generatedCards[i].laserGunDamageValue;
        playerStats.bigGunDamageValue += generatedCards[i].bigGunDamageValue;
        playerStats.doubleGunDamageValue += generatedCards[i].doubleGunDamageValue;
        playerStats.poisonGunCollisionDamageValue += generatedCards[i].poisonGunCollisionDamageValue;
        playerStats.poisonGunBetweenDamageValue += generatedCards[i].poisonGunBetweenDamageValue;
        playerStats.flameGunCollisionDamageValue += generatedCards[i].flameGunCollisionDamageValue;
        playerStats.flameGunBetweenDamageValue += generatedCards[i].flameGunBetweenDamageValue;
        playerStats.bombGunDamageValue += generatedCards[i].bombGunDamageValue;
        playerStats.homingGunDamageValue += generatedCards[i].homingGunDamageValue;
        playerStats.stunningGunDamageValue += generatedCards[i].stunningGunDamageValue;

        playerStats.normalGunAttackSpeedValue += generatedCards[i].normalGunAttackSpeedValue;
        playerStats.laserGunAttackSpeedValue += generatedCards[i].laserGunAttackSpeedValue;
        playerStats.bigGunAttackSpeedValue += generatedCards[i].bigGunAttackSpeedValue;
        playerStats.doubleGunAttackSpeedValue += generatedCards[i].doubleGunAttackSpeedValue;
        playerStats.poisonGunCollisionAttackSpeedValue += generatedCards[i].poisonGunCollisionAttackSpeedValue;
        playerStats.poisonGunBetweenAttackSpeedValue += generatedCards[i].poisonGunBetweenAttackSpeedValue;
        playerStats.poisonGunDurationValue += generatedCards[i].poisonGunDurationValue;
        playerStats.flameGunCollisionAttackSpeedValue += generatedCards[i].flameGunCollisionAttackSpeedValue;
        playerStats.flameGunDurationValue += generatedCards[i].flameGunDurationValue;
        playerStats.bombGunAttackSpeedValue += generatedCards[i].bombGunAttackSpeedValue;
        playerStats.homingGunAttackSpeedValue += generatedCards[i].homingGunAttackSpeedValue;
        playerStats.stunDurationValue += generatedCards[i].stunDurationValue;

        playerStats.shipSpeedValue += generatedCards[i].shipSpeedValue;
        playerStats.oreMiningBonusValue = generatedCards[i].oreMiningBonus;
        generatedCards.Clear();
        cardsMenuText.SetActive(false);
        cardsMenu.GetComponentInChildren <GridLayoutGroup>().enabled = false;
        for (int j = 0; j < cards.Count; j++)
        {
            if (j != i)
            {
                //Destroy(cards[j]);
                cards[j].SetActive(false);
            }
        }
        Vector2 centerOfCameraView = GetCenterOfCameraView();
        Sequence sequence = DOTween.Sequence();
        sequence.Append(cards[i].transform.DOMove(new Vector2(centerOfCameraView.x, centerOfCameraView.y), 1f).SetUpdate(UpdateType.Normal, true));
        sequence.AppendCallback(() =>
        {
            cardsMenu.GetComponent<CanvasGroup>().DOFade(0f, 2f).SetUpdate(UpdateType.Normal, true);
        });
        sequence.Play();
        Time.timeScale = 1f;
        shipMovement.enabled = true;
        Invoke("HideCardMenu", 2f);
        buttonSound.Play();
    }
    public void RefreshCard(int i)
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
        playerStats.refreshKey--;
        refreshes[i].GetComponent<Image>().enabled = false;
        refreshes[i].GetComponent<Button>().enabled = false;
        buttonSound.Play();
    }
    private void HideCardMenu()
    {
        cardsMenu.SetActive(false);
        constructMenu.SetActive(false);
        gameMenu.SetActive(true);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
    }
    public void HideConstructPoints()
    {
        constructPoints = GameObject.FindGameObjectsWithTag("ConstructPoint");
        for (int i = 0; i < constructPoints.Length; i++)
        {
            constructPoints[i].GetComponent<Image>().enabled = false;
            constructPoints[i].GetComponent<Button>().enabled = false;
        }
    }
    public void ActiveBossHealthBar()
    {
        bossHPBar.SetActive(true);
        backgroundBossHPBar.SetActive(true);
    }
    public void DeactiveBossHealthBar()
    {
        bossHPBar.SetActive(false);
        backgroundBossHPBar.SetActive(false);
    }
    private Vector2 GetCenterOfCameraView()
    {
        Vector2 viewportCenter = new Vector2(0.5f, 0.5f);

        Vector2 worldCenter = mainCam.ViewportToWorldPoint(viewportCenter);

        return worldCenter;
    }
    private IEnumerator LoadMenu()
    {
        levelLoader.SetActive(true);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("main");
    }
    private IEnumerator LoadUniverse()
    {
        levelLoader.SetActive(true);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Universe");
    }
}