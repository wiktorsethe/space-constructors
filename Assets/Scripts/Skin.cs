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
    public Sprite skinSpriteFlameGun;
    public RuntimeAnimatorController animatorFlameGun;
    public Sprite skinSpritePoisonGun;
    public RuntimeAnimatorController animatorPoisonGun;
    public Sprite skinSpriteStunningGun;
    public RuntimeAnimatorController animatorStunningGun;
    public Sprite skinSpriteBombGun;
    public RuntimeAnimatorController animatorBombGun;
    public Sprite skinSpriteDashFragment;
    public RuntimeAnimatorController animatorDashFragment;
    public Sprite skinSpriteHomingGun;
    public RuntimeAnimatorController animatorHomingGun;
    public Sprite skinSpriteShieldFragment;
    public RuntimeAnimatorController animatorShieldFragment;
    public int cost;
    public bool isPurchased;
}
