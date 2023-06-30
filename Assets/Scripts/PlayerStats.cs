using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "GameData/Player Stats")]
public class PlayerStats : ScriptableObject
{
    public float maxHealth;
    public float currentHealth;
    public int experience;
    public int level;
    public float bestTime;
    public int mostKills;
    public int mostGoldEarned;
    public int gold;
    public int gravity;
}
