using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject[] listOfPages;
    [SerializeField] private GameObject gameMenu;
    public PlayerStats playerStats;
    [SerializeField] private AudioSource buttonSound;
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
    private void Start()
    {
        buttonSound = GameObject.Find("Button").GetComponent<AudioSource>();
    }
    public void NextPage(int number)
    {
        listOfPages[number].SetActive(true);
        listOfPages[number-1].SetActive(false);
        buttonSound.Play();
    }
    public void PrevPage(int number)
    {
        listOfPages[number].SetActive(false);
        listOfPages[number - 1].SetActive(true);
        buttonSound.Play();
    }
    public void EndTutorial()
    {
        Time.timeScale = 1f;
        gameMenu.SetActive(true);
        playerStats.isAfterTutorial = true;
        gameObject.SetActive(false);
        buttonSound.Play();
    }
}
