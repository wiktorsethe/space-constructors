using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Challenge
{
    public string task;
    public Loot reward;
    public int amount;

    public float bestTimeTask;
    public float mostGoldEarnedTask;
    public float mostKillsTask;

    public bool isDone;
}
