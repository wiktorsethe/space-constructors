using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Challenges Database", menuName = "GameData/Challenges Database")]
public class ChallengesDatabase : ScriptableObject
{
    public Challenge[] challenges;
}
