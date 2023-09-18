using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipPartsDatabase", menuName = "GameData/Ship Parts Database")]
public class ShipPartsDatabase : ScriptableObject
{
    public ShipPart[] shipParts;

    public void Reset()
    {
        for(int i=0; i<shipParts.Length; i++)
        {
            shipParts[i].ownedAmount = 0;
        }
        shipParts[0].ownedAmount = 2;
        shipParts[1].ownedAmount = 1;
    }
}
