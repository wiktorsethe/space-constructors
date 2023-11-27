using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Skin
{
    public Sprite skinSpriteMain;
    public Sprite skinSpriteCorridor;
    public Sprite skinSpriteNormalGun;
    public RuntimeAnimatorController animatorNormalGun;
    public Sprite skinSpriteDoubleGun;
    public RuntimeAnimatorController animatorDoubleGun;
    public Sprite skinSpriteLaserGun;
    public RuntimeAnimatorController animatorLaserGun;
    public Sprite skinSpriteBigGun;
    public RuntimeAnimatorController animatorBigGun;
    public Sprite skinSpriteHealFragment;
    public RuntimeAnimatorController animatorHealFragment;
    public int cost;
    public bool isPurchased;
}
