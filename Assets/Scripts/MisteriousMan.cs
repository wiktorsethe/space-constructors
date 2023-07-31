using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisteriousMan : MonoBehaviour
{
    private GameObject player;
    private MenuMisteriousPlace menuMisteriousPlace;
    private bool isMenuOpened = false;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        menuMisteriousPlace = GameObject.FindObjectOfType(typeof(MenuMisteriousPlace)) as MenuMisteriousPlace;
    }
    private void Update()
    {
        if(Vector2.Distance(transform.position, player.transform.position) < 10f && !isMenuOpened)
        {
            menuMisteriousPlace.MisteriousManMenu();
            Time.timeScale = 0f;
            isMenuOpened = true;
        }
    }
}
