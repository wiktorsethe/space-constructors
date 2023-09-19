using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMan : MonoBehaviour
{
    [Header("Other Scripts")]
    private MenuMisteriousPlace menuMisteriousPlace;
    private WalkerMovement walkerMovement;
    [Space(20f)]
    [Header("Other")]
    private GameObject player;
    private bool isMenuOpened = false;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        menuMisteriousPlace = GameObject.FindObjectOfType(typeof(MenuMisteriousPlace)) as MenuMisteriousPlace;
        walkerMovement = GameObject.FindObjectOfType(typeof(WalkerMovement)) as WalkerMovement;
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 10f && !isMenuOpened)
        {
            menuMisteriousPlace.ChangeManMenu();
            walkerMovement.enabled = false;
            Time.timeScale = 0f;
            isMenuOpened = true;
        }
    }
}
