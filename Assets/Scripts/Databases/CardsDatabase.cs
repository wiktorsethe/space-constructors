using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardsDatabase", menuName = "GameData/Cards Database")]
public class CardsDatabase : ScriptableObject
{
    public Card[] cards;
}
