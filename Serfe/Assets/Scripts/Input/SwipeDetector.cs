using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;

    private Vector2 _swipeStartPosition;
    private Vector2 _swipeEndPosition;

    void Start()
    {

    }

    private void OnEnable()
    {
        _inputManager.OnStartTouchEvent += SwipeStart;
        _inputManager.OnEndTouchEvent += SwipeEnd;
    }

    private void SwipeEnd(Vector2 vector, float arg2)
    {
        _swipeEndPosition = vector;
        CalculateSwipeDirection();
    }


    private void SwipeStart(Vector2 vector, float arg2)
    {
        _swipeStartPosition = vector;
    }
    private void CalculateSwipeDirection()
    {
        Vector2 direction = _swipeEndPosition - _swipeStartPosition;
        float directionThreshold = 0.9f;
        if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
            Debug.Log("SwipeUp");
        else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
            Debug.Log("SwipeDown");
        else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
            Debug.Log("SwipeRight");
        else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
            Debug.Log("SwipeLeft");

    }

}
