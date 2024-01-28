using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraShake : MonoBehaviour
{
    public void ShakeCamera(float duration, float strength, int vibrato)
    {
        transform.DOShakePosition(duration, strength, vibrato);
    }
}
