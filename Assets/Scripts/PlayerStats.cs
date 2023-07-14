using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "GameData/Player Stats")]
public class PlayerStats : ScriptableObject
{
    public float shipMaxHealth;
    public float shipCurrentHealth;
    public int experience;
    public int level;
    public float bestTime;
    public float todayBestTime;
    public int mostKills;
    public int todayMostKills;
    public int mostGoldEarned;
    public int todayMostGoldEarned;
    public int gold;
    public float shipGravity;
    public int ore;
    public int screw;
    public float normalGunDamageValue;
    public float laserGunDamageValue;
    public float doubleGunDamageValue;
    public float bigGunDamageValue;
    public float normalGunAttackSpeedValue;
    public float laserGunAttackSpeedValue;
    public float doubleGunAttackSpeedValue;
    public float bigGunAttackSpeedValue;
    public float shipSpeedValue;
    public float oreMiningBonusValue;
    public Vector3 shipPosition;
    public Challenge[] challenges;
    public string loginTime;

    public void Reset()
    {
        shipMaxHealth = 100;
        shipCurrentHealth = 100;
        shipSpeedValue = 10;
        level = 1;
        experience = 0;
        shipGravity = 1;
        gold = 500;
        ore = 50;
        screw = 5;
        bestTime = 0;
        todayBestTime = 0;
        mostKills = 0;
        todayMostKills = 0;
        mostGoldEarned = 0;
        todayMostGoldEarned = 0;
        normalGunDamageValue = 1;
        laserGunDamageValue = 1;
        doubleGunDamageValue = 1;
        bigGunDamageValue = 1;
        normalGunAttackSpeedValue = 4;
        laserGunAttackSpeedValue = 4;
        doubleGunAttackSpeedValue = 3;
        bigGunAttackSpeedValue = 6;
        oreMiningBonusValue = 1;
        shipPosition = new Vector3(0f, 0f, 0f);
        for (int i = 0; i < challenges.Length; i++)
        {
            challenges[i].task = "";
            challenges[i].reward = null;
            challenges[i].amount = 0;
            challenges[i].bestTimeTask = 0;
            challenges[i].mostGoldEarnedTask = 0;
            challenges[i].mostKillsTask = 0;
            challenges[i].isDone = false;
        }
        loginTime = "10.10.2010 10:10:10";
    }
}
