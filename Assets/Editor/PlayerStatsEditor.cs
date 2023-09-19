using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerStats))]
public class PlayerStatsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PlayerStats playerStats = (PlayerStats)target;
        EditorGUILayout.Space();
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 16;
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("RESET TO DEFAULT", buttonStyle, GUILayout.Height(40), GUILayout.Width(200)))
        {
            playerStats.Reset();
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        EditorGUILayout.Space();
        GUILayout.Label("       SHIP STATS", EditorStyles.boldLabel);
        playerStats.shipMaxHealth = EditorGUILayout.FloatField("Ship Max Health", playerStats.shipMaxHealth);
        playerStats.shipCurrentHealth = EditorGUILayout.FloatField("Ship Current Health", playerStats.shipCurrentHealth);
        playerStats.shipMaxShield = EditorGUILayout.FloatField("Ship Max Shield", playerStats.shipMaxShield);
        playerStats.shipCurrentShield = EditorGUILayout.FloatField("Ship Current Shield", playerStats.shipCurrentShield);
        playerStats.shipSpeedValue = EditorGUILayout.FloatField("Ship Speed", playerStats.shipSpeedValue);
        playerStats.shipGravity = EditorGUILayout.FloatField("Gravity", playerStats.shipGravity);
        playerStats.oreMiningBonusValue = EditorGUILayout.FloatField("Ore Mining Bonus", playerStats.oreMiningBonusValue);
        playerStats.shipPosition = EditorGUILayout.Vector3Field("Ship Position", playerStats.shipPosition);
        //playerStats.shipSave = EditorGUILayout.ObjectField("Ship Save", playerStats.shipSave); 
        EditorGUILayout.Space();
        GUILayout.Label("       GUNS DAMAGE BONUS / PROCENTAGE", EditorStyles.boldLabel);
        playerStats.normalGunDamageValue = EditorGUILayout.FloatField("Normal Gun Damage", playerStats.normalGunDamageValue);
        playerStats.laserGunDamageValue = EditorGUILayout.FloatField("Laser Gun Damage", playerStats.laserGunDamageValue);
        playerStats.bigGunDamageValue = EditorGUILayout.FloatField("Big Gun Damage", playerStats.bigGunDamageValue);
        playerStats.doubleGunDamageValue = EditorGUILayout.FloatField("Double Gun Damage", playerStats.doubleGunDamageValue);
        playerStats.poisonGunCollisionDamageValue = EditorGUILayout.FloatField("Poison Collision Gun Damage", playerStats.poisonGunCollisionDamageValue);
        playerStats.poisonGunBetweenDamageValue = EditorGUILayout.FloatField("Poison Between Gun Damage", playerStats.poisonGunBetweenDamageValue);
        playerStats.flameGunCollisionDamageValue = EditorGUILayout.FloatField("Flame Collision Gun Damage", playerStats.flameGunCollisionDamageValue);
        playerStats.bombGunDamageValue = EditorGUILayout.FloatField("Bomb Gun Damage", playerStats.bombGunDamageValue);
        playerStats.homingGunDamageValue = EditorGUILayout.FloatField("Homing Gun Damage", playerStats.homingGunDamageValue);
        playerStats.stunningGunDamageValue = EditorGUILayout.FloatField("Stunning Gun Damage", playerStats.stunningGunDamageValue);
        EditorGUILayout.Space();
        GUILayout.Label("       GUNS ATTACK SPEED / DURATION", EditorStyles.boldLabel);
        playerStats.normalGunAttackSpeedValue = EditorGUILayout.FloatField("Normal Gun Attack Speed", playerStats.normalGunAttackSpeedValue);
        playerStats.laserGunAttackSpeedValue = EditorGUILayout.FloatField("Laser Gun Attack Speed", playerStats.laserGunAttackSpeedValue);
        playerStats.bigGunAttackSpeedValue = EditorGUILayout.FloatField("Big Gun Attack Speed", playerStats.bigGunAttackSpeedValue);
        playerStats.doubleGunAttackSpeedValue = EditorGUILayout.FloatField("Double Gun Attack Speed", playerStats.doubleGunAttackSpeedValue);
        playerStats.poisonGunCollisionAttackSpeedValue = EditorGUILayout.FloatField("Poison Gun Collision Attack Speed", playerStats.poisonGunCollisionAttackSpeedValue);
        playerStats.poisonGunBetweenAttackSpeedValue = EditorGUILayout.FloatField("Poison Gun Between Attack Speed", playerStats.poisonGunBetweenAttackSpeedValue);
        playerStats.poisonGunDurationValue = EditorGUILayout.IntField("Poison Gun Duration", playerStats.poisonGunDurationValue);
        playerStats.flameGunCollisionAttackSpeedValue = EditorGUILayout.FloatField("Flame Gun Collision Attack Speed", playerStats.flameGunCollisionAttackSpeedValue);
        playerStats.flameGunDurationValue = EditorGUILayout.FloatField("Flame Gun Duration", playerStats.flameGunDurationValue);
        playerStats.bombGunAttackSpeedValue = EditorGUILayout.FloatField("Bomb Gun Attack Speed", playerStats.bombGunAttackSpeedValue);
        playerStats.homingGunAttackSpeedValue = EditorGUILayout.FloatField("Homing Gun Attack Speed", playerStats.homingGunAttackSpeedValue);
        playerStats.stunningGunAttackSpeedValue = EditorGUILayout.FloatField("Stunning Gun Attack Speed", playerStats.stunningGunAttackSpeedValue);
        playerStats.stunDurationValue = EditorGUILayout.FloatField("Stun Duration", playerStats.stunDurationValue);
        EditorGUILayout.Space();
        GUILayout.Label("       LEVEL", EditorStyles.boldLabel);
        playerStats.level = EditorGUILayout.IntField("Level", playerStats.level);
        playerStats.experience = EditorGUILayout.IntField("Experience", playerStats.experience);
        EditorGUILayout.Space();
        GUILayout.Label("       PLAYER STATS", EditorStyles.boldLabel);
        playerStats.gold = EditorGUILayout.IntField("Gold", playerStats.gold);
        playerStats.ore = EditorGUILayout.IntField("Ore", playerStats.ore);
        playerStats.screw = EditorGUILayout.IntField("Screw ", playerStats.screw);
        playerStats.selectedSkin = EditorGUILayout.IntField("Selected Skin", playerStats.selectedSkin);
        playerStats.refreshKey = EditorGUILayout.IntField("Refresh Keys", playerStats.refreshKey);
        EditorGUILayout.Space();
        GUILayout.Label("       DEATH SCREEN STATS", EditorStyles.boldLabel);
        playerStats.bestTime = EditorGUILayout.FloatField("Best Time Stat", playerStats.bestTime);
        playerStats.todayBestTime = EditorGUILayout.FloatField("Today Best Time", playerStats.todayBestTime);
        playerStats.mostKills = EditorGUILayout.IntField("Most Kills Stat", playerStats.mostKills);
        playerStats.todayMostKills = EditorGUILayout.IntField("Today Kills", playerStats.todayMostKills);
        playerStats.mostGoldEarned = EditorGUILayout.IntField("Most Gold Earned Stat", playerStats.mostGoldEarned);
        playerStats.todayMostGoldEarned = EditorGUILayout.IntField("Today Most Gold Earned", playerStats.todayMostGoldEarned);
        EditorGUILayout.Space();
        GUILayout.Label("       DAILY CHALLENGES", EditorStyles.boldLabel);
        playerStats.loginTime = EditorGUILayout.TextField("Login Time", playerStats.loginTime);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("challenges"), true);
        serializedObject.ApplyModifiedProperties();
        EditorGUILayout.Space();
        GUILayout.Label("       DAILY REWARDS", EditorStyles.boldLabel);
        playerStats.dailyRewardsDay = EditorGUILayout.IntField("Daily Rewards Day", playerStats.dailyRewardsDay);
        playerStats.firstBonus = EditorGUILayout.Toggle("First Bonus", playerStats.firstBonus);
        playerStats.lastDateBonus = EditorGUILayout.TextField("Last Date Bonus", playerStats.lastDateBonus);
    }
}
