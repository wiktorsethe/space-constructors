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
            playerStats.shipMaxHealth = 100;
            playerStats.shipCurrentHealth = 100;
            playerStats.shipSpeedValue = 10;
            playerStats.level = 1;
            playerStats.experience = 0;
            playerStats.shipGravity = 1;
            playerStats.gold = 500;
            playerStats.ore = 50;
            playerStats.screw = 5;
            playerStats.bestTime = 0;
            playerStats.mostKills = 0;
            playerStats.mostGoldEarned = 0;
            playerStats.normalGunDamageValue = 1;
            playerStats.laserGunDamageValue = 1;
            playerStats.doubleGunDamageValue = 1;
            playerStats.bigGunDamageValue = 1;
            playerStats.normalGunAttackSpeedValue = 4;
            playerStats.laserGunAttackSpeedValue = 4;
            playerStats.doubleGunAttackSpeedValue = 3;
            playerStats.bigGunAttackSpeedValue = 6;
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        EditorGUILayout.Space();
        GUILayout.Label("       SHIP STATS", EditorStyles.boldLabel);
        playerStats.shipMaxHealth = EditorGUILayout.FloatField("Ship Max Health", playerStats.shipMaxHealth);
        playerStats.shipCurrentHealth = EditorGUILayout.FloatField("Ship Current Health", playerStats.shipCurrentHealth);
        playerStats.shipSpeedValue = EditorGUILayout.FloatField("Ship Speed", playerStats.shipSpeedValue);
        playerStats.shipGravity = EditorGUILayout.FloatField("Gravity", playerStats.shipGravity);
        //playerStats.shipSave = EditorGUILayout.ObjectField("Ship Save", playerStats.shipSave); 
        EditorGUILayout.Space();
        GUILayout.Label("       GUNS DAMAGE BONUS / PROCENTAGE", EditorStyles.boldLabel);
        playerStats.normalGunDamageValue = EditorGUILayout.FloatField("Normal Gun Damage", playerStats.normalGunDamageValue);
        playerStats.laserGunDamageValue = EditorGUILayout.FloatField("Laser Gun Damage", playerStats.laserGunDamageValue);
        playerStats.bigGunDamageValue = EditorGUILayout.FloatField("Big Gun Damage", playerStats.bigGunDamageValue);
        playerStats.doubleGunDamageValue = EditorGUILayout.FloatField("Double Gun Damage", playerStats.doubleGunDamageValue);
        EditorGUILayout.Space();
        GUILayout.Label("       GUNS ATTACK SPEED", EditorStyles.boldLabel);
        playerStats.normalGunAttackSpeedValue = EditorGUILayout.FloatField("Normal Gun Attack Speed", playerStats.normalGunAttackSpeedValue);
        playerStats.laserGunAttackSpeedValue = EditorGUILayout.FloatField("Laser Gun Attack Speed", playerStats.laserGunAttackSpeedValue);
        playerStats.doubleGunAttackSpeedValue = EditorGUILayout.FloatField("Big Gun Attack Speed", playerStats.doubleGunAttackSpeedValue);
        playerStats.bigGunAttackSpeedValue = EditorGUILayout.FloatField("Double Gun Attack Speed", playerStats.bigGunAttackSpeedValue);
        EditorGUILayout.Space();
        GUILayout.Label("       LEVEL", EditorStyles.boldLabel);
        playerStats.level = EditorGUILayout.IntField("Level", playerStats.level);
        playerStats.experience = EditorGUILayout.IntField("Experience", playerStats.experience);
        EditorGUILayout.Space();
        GUILayout.Label("       PLAYER STATS", EditorStyles.boldLabel);
        playerStats.gold = EditorGUILayout.IntField("Gold", playerStats.gold);
        playerStats.ore = EditorGUILayout.IntField("Ore", playerStats.ore);
        playerStats.screw = EditorGUILayout.IntField("Screw ", playerStats.screw);
        EditorGUILayout.Space();
        GUILayout.Label("       DEATH SCREEN STATS", EditorStyles.boldLabel);
        playerStats.bestTime = EditorGUILayout.FloatField("Best Time Stat", playerStats.bestTime);
        playerStats.mostKills = EditorGUILayout.IntField("Most Kills Stat", playerStats.mostKills);
        playerStats.mostGoldEarned = EditorGUILayout.IntField("Most Gold Earned Stat", playerStats.mostGoldEarned);
    }
}
