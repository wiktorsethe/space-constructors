using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuMisteriousPlace : MonoBehaviour
{
    [Header("Other Scripts")]
    public PlayerStats playerStats;
    public ShipProgress shipProgress;
    public CardsDatabase cardsDB;
    private WalkerMovement walkerMovement;
    private CameraSize camSize;
    [Space(20f)]
    [Header("Other GameObjects")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject misteriousManMenu;
    [SerializeField] private GameObject forgeManMenu;
    [SerializeField] private GameObject changeManMenu;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private AudioSource buttonSound;
    private Camera mainCam;
    [Space(20f)]
    [Header("Lists")]
    [SerializeField] private List<Card> generatedCards = new List<Card>();
    [SerializeField] private Animator transition;
    [SerializeField] private GameObject levelLoader;
    private void Start()
    {
        camSize = GameObject.FindObjectOfType(typeof(CameraSize)) as CameraSize;
        mainCam = Camera.main;
    }
    public void ExitMenu()
    {
        Time.timeScale = 1f;
        buttonSound.Play();
        gameMenu.SetActive(true);
        pauseMenu.SetActive(false);
        misteriousManMenu.SetActive(false);
        forgeManMenu.SetActive(false);
        changeManMenu.SetActive(false);
    }
    public void PauseMenu()
    {
        Time.timeScale = 0f;
        buttonSound.Play();
        pauseMenu.SetActive(true);
        gameMenu.SetActive(false);
        misteriousManMenu.SetActive(false);
        forgeManMenu.SetActive(false);
        changeManMenu.SetActive(false);
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        buttonSound.Play();
        gameMenu.SetActive(true);
        pauseMenu.SetActive(false);
        misteriousManMenu.SetActive(false);
        forgeManMenu.SetActive(false);
        changeManMenu.SetActive(false);

    }
    public void MisteriousManMenu()
    {
        buttonSound.Play();
        gameMenu.SetActive(false);
        pauseMenu.SetActive(false);
        misteriousManMenu.SetActive(true);
        forgeManMenu.SetActive(false);
        changeManMenu.SetActive(false);
        for (int i=0; i<cardsDB.cards.Length; i++)
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
    public void ForgeManMenu()
    {
        buttonSound.Play();
        gameMenu.SetActive(false);
        pauseMenu.SetActive(false);
        misteriousManMenu.SetActive(false);
        forgeManMenu.SetActive(true);
        changeManMenu.SetActive(false);
    }
    public void ChangeManMenu()
    {
        buttonSound.Play();
        gameMenu.SetActive(false);
        pauseMenu.SetActive(false);
        misteriousManMenu.SetActive(false);
        forgeManMenu.SetActive(false);
        changeManMenu.SetActive(true);
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
    public void ChooseCard(int i)
    {
        buttonSound.Play();
        playerStats.normalGunDamageValue += generatedCards[i].normalGunDamageValue * 2;
        playerStats.laserGunDamageValue += generatedCards[i].laserGunDamageValue * 2;
        playerStats.bigGunDamageValue += generatedCards[i].bigGunDamageValue * 2;
        playerStats.doubleGunDamageValue += generatedCards[i].doubleGunDamageValue * 2;
        playerStats.poisonGunCollisionDamageValue += generatedCards[i].poisonGunCollisionDamageValue * 2;
        playerStats.poisonGunBetweenDamageValue += generatedCards[i].poisonGunBetweenDamageValue * 2;
        playerStats.flameGunCollisionDamageValue += generatedCards[i].flameGunCollisionDamageValue * 2;
        playerStats.flameGunBetweenDamageValue += generatedCards[i].flameGunBetweenDamageValue * 2;
        playerStats.bombGunDamageValue += generatedCards[i].bombGunDamageValue * 2;
        playerStats.homingGunDamageValue += generatedCards[i].homingGunDamageValue * 2;
        playerStats.stunningGunDamageValue += generatedCards[i].stunningGunDamageValue * 2;

        playerStats.normalGunAttackSpeedValue += generatedCards[i].normalGunAttackSpeedValue * 2;
        playerStats.laserGunAttackSpeedValue += generatedCards[i].laserGunAttackSpeedValue * 2;
        playerStats.bigGunAttackSpeedValue += generatedCards[i].bigGunAttackSpeedValue * 2;
        playerStats.doubleGunAttackSpeedValue += generatedCards[i].doubleGunAttackSpeedValue * 2;
        playerStats.poisonGunCollisionAttackSpeedValue += generatedCards[i].poisonGunCollisionAttackSpeedValue * 2;
        playerStats.poisonGunBetweenAttackSpeedValue += generatedCards[i].poisonGunBetweenAttackSpeedValue * 2;
        playerStats.poisonGunDurationValue += generatedCards[i].poisonGunDurationValue * 2;
        playerStats.flameGunCollisionAttackSpeedValue += generatedCards[i].flameGunCollisionAttackSpeedValue * 2;
        playerStats.flameGunDurationValue += generatedCards[i].flameGunDurationValue * 2;
        playerStats.bombGunAttackSpeedValue += generatedCards[i].bombGunAttackSpeedValue * 2;
        playerStats.homingGunAttackSpeedValue += generatedCards[i].homingGunAttackSpeedValue * 2;
        playerStats.stunDurationValue += generatedCards[i].stunDurationValue * 2;

        playerStats.shipSpeedValue += generatedCards[i].shipSpeedValue * 2;
        playerStats.oreMiningBonusValue = generatedCards[i].oreMiningBonus * 2;
        gameMenu.SetActive(true);
        pauseMenu.SetActive(false);
        misteriousManMenu.SetActive(false);
        Time.timeScale = 1f;
        camSize.CamSize(mainCam.orthographicSize / 3f, 5f);
        Invoke("ChangeScene", 5f);
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
