using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipPartsDatabase", menuName = "GameData/Ship Parts Database")]
public class ShipPartsDatabase : ScriptableObject
{
    public ShipPart[] shipParts;
}
