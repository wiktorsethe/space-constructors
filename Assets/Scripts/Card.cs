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
    public int poisonGunCollisionDamageValue;
    public int poisonGunBetweenDamageValue;
    public int flameGunCollisionDamageValue;
    public int flameGunBetweenDamageValue;
    public int bombGunDamageValue;
    public int homingGunDamageValue;
    public int stunningGunDamageValue;

    public int normalGunAttackSpeedValue;
    public int laserGunAttackSpeedValue;
    public int doubleGunAttackSpeedValue;
    public int bigGunAttackSpeedValue;
    public int poisonGunCollisionAttackSpeedValue;
    public int poisonGunBetweenAttackSpeedValue;
    public int poisonGunDurationValue;
    public int flameGunCollisionAttackSpeedValue;
    public int flameGunDurationValue;
    public int bombGunAttackSpeedValue;
    public int homingGunAttackSpeedValue;
    public int stunDurationValue;

    public int shipSpeedValue;
    public float oreMiningBonus;

    /* 

    public float stunDurationValue;
     */
}
