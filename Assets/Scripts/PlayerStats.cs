using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "GameData/Player Stats")]
public class PlayerStats : ScriptableObject
{
    public float shipMaxHealth;
    public float shipCurrentHealth;
    public float shipMaxShield;
    public float shipCurrentShield;
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
    public float poisonGunCollisionDamageValue;
    public float poisonGunBetweenDamageValue;
    public float flameGunCollisionDamageValue;
    public float flameGunBetweenDamageValue;
    public float bombGunDamageValue;
    public float homingGunDamageValue;
    public float stunningGunDamageValue;

    public float normalGunAttackSpeedValue;
    public float laserGunAttackSpeedValue;
    public float doubleGunAttackSpeedValue;
    public float bigGunAttackSpeedValue;
    public float poisonGunCollisionAttackSpeedValue;
    public float poisonGunBetweenAttackSpeedValue;
    public int poisonGunDurationValue;
    public float flameGunCollisionAttackSpeedValue;
    public float flameGunDurationValue;
    public float bombGunAttackSpeedValue;
    public float homingGunAttackSpeedValue;
    public float stunningGunAttackSpeedValue;
    public float stunDurationValue;

    public float shipSpeedValue;
    public float oreMiningBonusValue;
    public Vector3 shipPosition;
    public Challenge[] challenges;
    public string loginTime;
    public int dailyRewardsDay;
    public bool firstBonus;
    public string lastDateBonus;
    public int selectedSkin;
    public int refreshKey;

    public void Reset()
    {
        shipMaxHealth = 100;
        shipCurrentHealth = 100;
        shipCurrentShield = 0;
        shipMaxShield = 0;
        shipSpeedValue = 50;
        level = 1;
        experience = 0;
        shipGravity = 1;
        ore = 50;
        screw = 5;
        bestTime = 0;
        mostKills = 0;
        mostGoldEarned = 0;

        normalGunDamageValue = 3;
        laserGunDamageValue = 2;
        doubleGunDamageValue = 2;
        bigGunDamageValue = 5;
        poisonGunCollisionDamageValue = 4;
        poisonGunBetweenDamageValue = 1;
        flameGunCollisionDamageValue = 4;
        flameGunBetweenDamageValue = 1;
        bombGunDamageValue = 7;
        homingGunDamageValue = 4;
        stunningGunDamageValue = 0;

        normalGunAttackSpeedValue = 4;
        laserGunAttackSpeedValue = 4;
        doubleGunAttackSpeedValue = 3;
        bigGunAttackSpeedValue = 6;
        poisonGunCollisionAttackSpeedValue = 8;
        poisonGunBetweenAttackSpeedValue = 2;
        poisonGunDurationValue = 3;
        flameGunCollisionAttackSpeedValue = 8;
        flameGunDurationValue = 2;
        bombGunAttackSpeedValue = 7;
        homingGunAttackSpeedValue = 3;
        stunningGunAttackSpeedValue = 8;
        stunDurationValue = 2;

        oreMiningBonusValue = 1;
        shipPosition = new Vector3(0f, 0f, 0f);
    }
    public void ResetRestStats()
    {
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
        dailyRewardsDay = 0;
        firstBonus = false;
        lastDateBonus = "10.10.2010 10:10:10";
        selectedSkin = 0;
        refreshKey = 3;
        gold = 500; 
        todayMostKills = 0;
        todayMostGoldEarned = 0;
        todayBestTime = 0;
    }
}
