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
    [SerializeField] private GameObject[] rewardsObjects;
    [SerializeField] private GameObject[] claimButtonObjects;
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

            for(int i=0; i<4; i++)
            {
                int randIndex = RandomRangeExcept(0, challengesDB.challenges.Length);

                challengesObjects[i].transform.Find("MissionTxt").GetComponent<TMP_Text>().text = challengesDB.challenges[randIndex].task;
                rewardsObjects[i].transform.Find("RewardTxt").GetComponent<TMP_Text>().text = challengesDB.challenges[randIndex].amount.ToString();

                if (challengesDB.challenges[randIndex].bestTimeTask <= playerStats.todayBestTime
                    && challengesDB.challenges[randIndex].mostGoldEarnedTask <= playerStats.todayMostGoldEarned
                    && challengesDB.challenges[randIndex].mostKillsTask <= playerStats.todayMostKills)
                {
                    claimButtonObjects[i].GetComponent<Button>().interactable = true;
                }
                else
                {
                    claimButtonObjects[i].GetComponent<Button>().interactable = false;
                }

                challengesDB.challenges[randIndex].isDone = false;
                except.Add(randIndex);
                playerStats.challenges[i] = challengesDB.challenges[randIndex];
            }

            //int i = 0;
            //int j = 0;
            /*
            foreach (GameObject challenge in challengesObjects)
            {
                randIndex = RandomRangeExcept(0, challengesDB.challenges.Length);
                challenge.transform.Find("MissionTxt").GetComponent<TMP_Text>().text = challengesDB.challenges[randIndex].task;
                challengesDB.challenges[randIndex].isDone = false;
                
                except.Add(randIndex);
                playerStats.challenges[i] = challengesDB.challenges[randIndex];
                i++;
            }
            foreach (GameObject reward in rewardsObjects)
            {
                reward.transform.Find("RewardTxt").GetComponent<TMP_Text>().text = challengesDB.challenges[randIndex].amount.ToString();
                challengesDB.challenges[randIndex].isDone = false;
                
                j++;
            }
            foreach(GameObject button in claimButtonObjects)
            {
                if (challengesDB.challenges[randIndex].bestTimeTask <= playerStats.todayBestTime
                    && challengesDB.challenges[randIndex].mostGoldEarnedTask <= playerStats.todayMostGoldEarned
                    && challengesDB.challenges[randIndex].mostKillsTask <= playerStats.todayMostKills)
                {
                    button.GetComponent<Button>().interactable = true;
                }
                else
                {
                    button.GetComponent<Button>().interactable = false;
                }
            }
            */
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                if (!playerStats.challenges[i].isDone)
                {
                    challengesObjects[i].transform.Find("MissionTxt").GetComponent<TMP_Text>().text = playerStats.challenges[i].task;
                    rewardsObjects[i].transform.Find("RewardTxt").GetComponent<TMP_Text>().text = playerStats.challenges[i].amount.ToString();
                    if (playerStats.challenges[i].bestTimeTask <= playerStats.todayBestTime
                    && playerStats.challenges[i].mostGoldEarnedTask <= playerStats.todayMostGoldEarned
                    && playerStats.challenges[i].mostKillsTask <= playerStats.todayMostKills)
                    {
                        claimButtonObjects[i].GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        claimButtonObjects[i].GetComponent<Button>().interactable = false;
                    }
                }
                else
                {
                    challengesObjects[i].transform.Find("MissionTxt").GetComponent<TMP_Text>().text = "Task done";
                    rewardsObjects[i].transform.Find("RewardTxt").GetComponent<TMP_Text>().text = "";
                    claimButtonObjects[i].GetComponent<Button>().interactable = false;
                }
            }

            /*
            int i = 0;
            int j = 0;
            int k = 0;
            foreach (GameObject challenge in challengesObjects)
            {
                if (!playerStats.challenges[i].isDone)
                {
                    challenge.transform.Find("MissionTxt").GetComponent<TMP_Text>().text = playerStats.challenges[i].task; //challengesDB.challenges[randIndex].task;
                    //challenge.transform.Find("RewardText").GetComponent<TMP_Text>().text = playerStats.challenges[i].amount.ToString();
                    //challenge.transform.Find("RewardIcon").GetComponent<Image>().sprite = playerStats.challenges[i].reward.lootSprite;
                }
                else
                {
                    challenge.transform.Find("MissionTxt").GetComponent<TMP_Text>().text = "Task done";
                    //challenge.transform.Find("RewardText").GetComponent<TMP_Text>().text = "";
                    //challenge.transform.Find("RewardIcon").GetComponent<Image>().enabled = false;

                }
                i++;
            }
            foreach (GameObject reward in rewardsObjects)
            {
                if (!playerStats.challenges[j].isDone)
                {
                    //reward.transform.Find("TaskText").GetComponent<TMP_Text>().text = playerStats.challenges[i].task; //challengesDB.challenges[randIndex].task;
                    reward.transform.Find("RewardTxt").GetComponent<TMP_Text>().text = playerStats.challenges[j].amount.ToString();
                    //reward.transform.Find("RewardIcon").GetComponent<Image>().sprite = playerStats.challenges[i].reward.lootSprite;

                }
                else
                {
                    //reward.transform.Find("TaskText").GetComponent<TMP_Text>().text = "Task done";
                    reward.transform.Find("RewardTxt").GetComponent<TMP_Text>().text = "";
                    //reward.transform.Find("RewardIcon").GetComponent<Image>().enabled = false;

                }
                j++;
            }
            foreach(GameObject button in claimButtonObjects)
            {
                if (playerStats.challenges[k].bestTimeTask <= playerStats.todayBestTime
                    && playerStats.challenges[k].mostGoldEarnedTask <= playerStats.todayMostGoldEarned
                    && playerStats.challenges[k].mostKillsTask <= playerStats.todayMostKills)
                {
                    button.GetComponent<Button>().interactable = true;
                }
                else
                {
                    button.GetComponent<Button>().interactable = false;
                }
                k++;
            }
            */
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
        claimButtonObjects[index].GetComponent<Button>().interactable = false;

        playerStats.challenges[index].isDone = true;

        for (int i = 0; i < challengesObjects.Length; i++)
        {
            challengesObjects[index].transform.Find("MissionTxt").GetComponent<TMP_Text>().text = "Task done";
            rewardsObjects[index].transform.Find("RewardTxt").GetComponent<TMP_Text>().text = "";
            //rewardsObjects[index].transform.Find("RewardIcon").GetComponent<Image>().enabled = false;
            claimButtonObjects[index].GetComponent<Button>().interactable = false;
        }
        //save.LocalSavePlayerStats();
    }
}
