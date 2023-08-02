using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Random = UnityEngine.Random;

public class DailyChallenges : MonoBehaviour
{
    public ChallengesDatabase challengesDB;
    public PlayerStats playerStats;
    [SerializeField] private GameObject[] challengesObjects;
    //public SaveManager save;
    private List<int> except = new List<int>();
    private void Start()
    {
        DateTime currentDate = DateTime.Now;
        DateTime loginTime = DateTime.Parse(playerStats.loginTime);
        double elapsedHours = (currentDate - loginTime).TotalHours;
        if (elapsedHours >= 20f)
        {
            Array.Clear(playerStats.challenges, 0, playerStats.challenges.Length);
            playerStats.todayBestTime = 0;
            playerStats.todayMostGoldEarned = 0;
            playerStats.todayMostKills = 0;
            playerStats.loginTime = DateTime.Now.ToString();
            int i = 0;
            foreach (GameObject challenge in challengesObjects)
            {
                int randIndex = RandomRangeExcept(0, challengesDB.challenges.Length);
                challenge.transform.Find("TaskText").GetComponent<TMP_Text>().text = challengesDB.challenges[randIndex].task;
                challenge.transform.Find("RewardText").GetComponent<TMP_Text>().text = challengesDB.challenges[randIndex].amount.ToString();
                challenge.transform.Find("RewardIcon").GetComponent<Image>().sprite = challengesDB.challenges[randIndex].reward.lootSprite;
                challengesDB.challenges[randIndex].isDone = false;
                if (challengesDB.challenges[randIndex].bestTimeTask <= playerStats.todayBestTime
                    && challengesDB.challenges[randIndex].mostGoldEarnedTask <= playerStats.todayMostGoldEarned
                    && challengesDB.challenges[randIndex].mostKillsTask <= playerStats.todayMostKills)
                {
                    challenge.GetComponent<Button>().interactable = true;
                }
                else
                {
                    challenge.GetComponent<Button>().interactable = false;
                }
                except.Add(randIndex);
                playerStats.challenges[i] = challengesDB.challenges[randIndex];
                i++;
            }
        }
        else
        {
            int i = 0;
            foreach (GameObject challenge in challengesObjects)
            {
                if (!playerStats.challenges[i].isDone)
                {
                    challenge.transform.Find("TaskText").GetComponent<TMP_Text>().text = playerStats.challenges[i].task; //challengesDB.challenges[randIndex].task;
                    challenge.transform.Find("RewardText").GetComponent<TMP_Text>().text = playerStats.challenges[i].amount.ToString();
                    challenge.transform.Find("RewardIcon").GetComponent<Image>().sprite = playerStats.challenges[i].reward.lootSprite;
                    if (playerStats.challenges[i].bestTimeTask <= playerStats.todayBestTime
                        && playerStats.challenges[i].mostGoldEarnedTask <= playerStats.todayMostGoldEarned
                        && playerStats.challenges[i].mostKillsTask <= playerStats.todayMostKills)
                    {
                        challenge.GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        challenge.GetComponent<Button>().interactable = false;
                    }

                }
                else
                {
                    challenge.transform.Find("TaskText").GetComponent<TMP_Text>().text = "Task done";
                    challenge.transform.Find("RewardText").GetComponent<TMP_Text>().text = "";
                    challenge.transform.Find("RewardIcon").GetComponent<Image>().enabled = false;
                    challenge.GetComponent<Button>().interactable = false;

                }
                i++;
            }
        }


    }
    int RandomRangeExcept(int min, int max)
    {
        int number;
        do
        {
            number = Random.Range(min, max);
        } while (except.Contains(number));
        return number;
    }
    public void CollectReward(int index)
    {
        if (playerStats.challenges[index].reward.lootName == "Gold")
        {
            playerStats.gold += playerStats.challenges[index].amount;
        }
        challengesObjects[index].GetComponent<Button>().interactable = false;

        playerStats.challenges[index].isDone = true;

        for (int i = 0; i < challengesObjects.Length; i++)
        {
            challengesObjects[index].transform.Find("TaskText").GetComponent<TMP_Text>().text = "Task done";
            challengesObjects[index].transform.Find("RewardText").GetComponent<TMP_Text>().text = "";
            challengesObjects[index].transform.Find("RewardIcon").GetComponent<Image>().enabled = false;
            challengesObjects[index].GetComponent<Button>().interactable = false;
        }
        //save.LocalSavePlayerStats();
    }
}
