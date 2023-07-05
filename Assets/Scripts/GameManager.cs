using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [Header("Lists and Objects")]
    public Camera mainCamera;
    public PlayerStats playerStats;
    [Space(20f)]

    [Header("Variables")]
    public float maxDistance = 5f;
    public float delay = 2f;
    public float trashTimer = 0f;
    public float bestTimeTimer = 0f;
    public int kills = 0;
    public int goldEarned = 0;
    public TMP_Text sceneNameText;
    public string sceneName;
    private void Start()
    {
        sceneNameText.text = sceneName;
        FadeInSceneNameText();
        Invoke("FadeOutSceneNameText", 3f);
    }
    private void Update()
    {
        bestTimeTimer += Time.deltaTime;
        if(bestTimeTimer > playerStats.bestTime)
        {
            playerStats.bestTime = bestTimeTimer;
        }
        if(kills > playerStats.mostKills)
        {
            playerStats.mostKills = kills;
        }
        if(goldEarned > playerStats.mostGoldEarned)
        {
            playerStats.mostGoldEarned = goldEarned;
        }
    }

    private void FadeInSceneNameText()
    {
        sceneNameText.DOFade(1f, 2f);
    }
    private void FadeOutSceneNameText()
    {
        sceneNameText.DOFade(0f, 2f);
        Destroy(sceneNameText.gameObject, 2f);
    }
}
