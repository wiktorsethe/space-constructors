using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScrollingBackground : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float speed;
    private RectTransform rectTransform;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        // Initialize the background image position.
        Vector2 startPos = rectTransform.anchoredPosition;
        Vector2 endPos = new Vector2(startPos.x - 800f, startPos.y);

        // Create a looping animation using DoTween.
        rectTransform.DOAnchorPos(endPos, 10f / speed)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }

}
