using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportBack : MonoBehaviour
{
    private GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            PlayerPrefs.SetFloat("BestTimeTimer", gameManager.bestTimeTimer);
            PlayerPrefs.SetInt("Kills", gameManager.kills);
            PlayerPrefs.SetInt("GoldEarned", gameManager.goldEarned);
            SceneManager.LoadScene("Universe");
        }
    }
}
