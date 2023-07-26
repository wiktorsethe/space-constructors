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
    public List<Card> generatedCards = new List<Card>();

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
        playerStats.normalGunDamageValue += generatedCards[i].normalGunDamageValue;
        playerStats.laserGunDamageValue += generatedCards[i].laserGunDamageValue;
        playerStats.bigGunDamageValue += generatedCards[i].bigGunDamageValue;
        playerStats.doubleGunDamageValue += generatedCards[i].doubleGunDamageValue;
        playerStats.normalGunAttackSpeedValue += generatedCards[i].normalGunAttackSpeedValue;
        playerStats.laserGunAttackSpeedValue += generatedCards[i].laserGunAttackSpeedValue;
        playerStats.bigGunAttackSpeedValue += generatedCards[i].bigGunAttackSpeedValue;
        playerStats.doubleGunAttackSpeedValue += generatedCards[i].doubleGunAttackSpeedValue;
        playerStats.shipSpeedValue += generatedCards[i].shipSpeedValue;
        playerStats.oreMiningBonusValue = generatedCards[i].oreMiningBonus;
        gameMenu.SetActive(true);
        pauseMenu.SetActive(false);
        misteriousManMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
