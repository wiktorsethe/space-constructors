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
            shipParts[i].produceAmount = 0;
            shipParts[i].neededToProduceAmount = 10;
        }
        //part 0
        shipParts[0].ownedAmount = 2;
        shipParts[0].oreCost = 10;
        shipParts[0].screwCost = 10;
        shipParts[0].gravityBonus = 2;
        shipParts[0].hpBonus = 20;
        shipParts[0].isOwned = true;

        //part 1
        shipParts[1].ownedAmount = 1;
        shipParts[1].oreCost = 20;
        shipParts[1].screwCost = 15;
        shipParts[1].gravityBonus = 6;
        shipParts[1].hpBonus = 50;
        shipParts[1].isOwned = true;

        //part 2
        shipParts[2].ownedAmount = 0;
        shipParts[2].oreCost = 15;
        shipParts[2].screwCost = 15;
        shipParts[2].gravityBonus = 1;
        shipParts[2].hpBonus = 10;
        shipParts[2].isOwned = false;

        //part 3
        shipParts[3].ownedAmount = 0;
        shipParts[3].oreCost = 15;
        shipParts[3].screwCost = 15;
        shipParts[3].gravityBonus = 1;
        shipParts[3].hpBonus = 10;
        shipParts[3].isOwned = false;

        //part 4
        shipParts[4].ownedAmount = 0;
        shipParts[4].oreCost = 15;
        shipParts[4].screwCost = 15;
        shipParts[4].gravityBonus = 1;
        shipParts[4].hpBonus = 10;
        shipParts[4].isOwned = false;
        
        //part 5
        shipParts[5].ownedAmount = 0;
        shipParts[5].oreCost = 15;
        shipParts[5].screwCost = 15;
        shipParts[5].gravityBonus = 1;
        shipParts[5].hpBonus = 10;
        shipParts[5].isOwned = false;

        //part 6
        shipParts[6].ownedAmount = 1;
        shipParts[6].oreCost = 15;
        shipParts[6].screwCost = 15;
        shipParts[6].gravityBonus = 1;
        shipParts[6].hpBonus = 10;
        shipParts[6].isOwned = true;

        //part 7
        shipParts[7].ownedAmount = 0;
        shipParts[7].oreCost = 15;
        shipParts[7].screwCost = 15;
        shipParts[7].gravityBonus = 1;
        shipParts[7].hpBonus = 10;
        shipParts[7].isOwned = false;

        //part 8
        shipParts[8].ownedAmount = 0;
        shipParts[8].oreCost = 15;
        shipParts[8].screwCost = 15;
        shipParts[8].gravityBonus = 1;
        shipParts[8].hpBonus = 10;
        shipParts[8].isOwned = false;

        //part 9
        shipParts[9].ownedAmount = 0;
        shipParts[9].oreCost = 15;
        shipParts[9].screwCost = 15;
        shipParts[9].gravityBonus = 1;
        shipParts[9].hpBonus = 10;
        shipParts[9].isOwned = false;

        //part 10
        shipParts[10].ownedAmount = 0;
        shipParts[10].oreCost = 15;
        shipParts[10].screwCost = 15;
        shipParts[10].gravityBonus = 1;
        shipParts[10].hpBonus = 10;
        shipParts[10].isOwned = false;

        //part 11
        shipParts[11].ownedAmount = 0;
        shipParts[11].oreCost = 15;
        shipParts[11].screwCost = 15;
        shipParts[11].gravityBonus = 1;
        shipParts[11].hpBonus = 10;
        shipParts[11].isOwned = false;

        //part 12
        shipParts[12].ownedAmount = 0;
        shipParts[12].oreCost = 15;
        shipParts[12].screwCost = 15;
        shipParts[12].gravityBonus = 1;
        shipParts[12].hpBonus = 10;
        shipParts[12].isOwned = false;

        //part 13
        shipParts[13].ownedAmount = 0;
        shipParts[13].oreCost = 15;
        shipParts[13].screwCost = 15;
        shipParts[13].gravityBonus = 1;
        shipParts[13].hpBonus = 10;
        shipParts[13].isOwned = false;
    }
}
