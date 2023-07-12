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
    public int mostKills;
    public int mostGoldEarned;
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
    public Vector3 shipPosition;
}
