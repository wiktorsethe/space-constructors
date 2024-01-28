using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SwipeInConstructionMenu : MonoBehaviour
{
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private Vector2 swipeDirection;
    public float swipeThreshold = 50f;
    public float swipeSpeed = 5f;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = touch.position;
                    break;

                case TouchPhase.Moved:
                    touchEndPos = touch.position;
                    swipeDirection = touchEndPos - touchStartPos;

                    if (swipeDirection.magnitude > swipeThreshold)
                    {
                        Vector3 newPosition = transform.position - new Vector3(swipeDirection.x, swipeDirection.y, 0f) * swipeSpeed;
                        transform.DOMove(newPosition, 0.1f).SetUpdate(UpdateType.Normal, true);
                    }
                    break;

                case TouchPhase.Ended:
                    swipeDirection = Vector2.zero;
                    break;

            }
        }
    }
}
