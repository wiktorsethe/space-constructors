using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinsDatabase", menuName = "GameData/Skins Database")]
public class SkinsDatabase : ScriptableObject
{
    public Skin[] skins;
}
