using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject[] listOfPages;
    [SerializeField] private GameObject gameMenu;
    public PlayerStats playerStats;

    private void Awake()
    {
        if(!playerStats.isAfterTutorial)
        {
            Time.timeScale = 0f;
            gameMenu.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    public void NextPage(int number)
    {
        listOfPages[number].SetActive(true);
        listOfPages[number-1].SetActive(false);
    }
    public void PrevPage(int number)
    {
        listOfPages[number].SetActive(false);
        listOfPages[number - 1].SetActive(true);
    }
    public void EndTutorial()
    {
        Time.timeScale = 1f;
        gameMenu.SetActive(true);
        playerStats.isAfterTutorial = true;
        gameObject.SetActive(false);
    }
}
