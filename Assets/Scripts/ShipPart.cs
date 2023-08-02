using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ShipPart
{
    public Sprite image;
    public GameObject shipPart;
    public int amount;
    public int oreCost;
    public int screwCost;
    public int gravityBonus;
    public string shipPartType;
}
