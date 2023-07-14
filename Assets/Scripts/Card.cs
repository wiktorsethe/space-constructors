using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Card
{
    public Sprite image;
    public string description;
    public int normalGunDamageValue;
    public int laserGunDamageValue;
    public int doubleGunDamageValue;
    public int bigGunDamageValue;
    public int normalGunAttackSpeedValue;
    public int laserGunAttackSpeedValue;
    public int doubleGunAttackSpeedValue;
    public int bigGunAttackSpeedValue;
    public int shipSpeedValue;
    public float oreMiningBonus;
}
