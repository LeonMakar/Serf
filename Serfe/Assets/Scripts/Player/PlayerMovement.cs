using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _speed;

    private float _currentPosition;
    private float _leftPosition = -1.5f;
    private float _midllePosition = 0;
    private float _rightPosition = 1.5f;
    private Vector3 _moovingDirection = Vector3.forward;






    private void Update()
    {
        _characterController.SimpleMove(_moovingDirection * _speed);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            switch (_currentPosition)
            {
                case 0:
                    transform.DOMoveX(_leftPosition, 0.3f).SetEase(Ease.Linear);
                    _currentPosition = _leftPosition;
                    break;
                case -1.5f:
                    break;
                case 1.5f:
                    transform.DOMoveX(_midllePosition, 0.3f).SetEase(Ease.Linear);
                    _currentPosition = _midllePosition;
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            switch (_currentPosition)
            {
                case 0:
                    transform.DOMoveX(_rightPosition, 0.3f).SetEase(Ease.Linear);
                    _currentPosition = _rightPosition;
                    break;
                case -1.5f:
                    transform.DOMoveX(_midllePosition, 0.3f).SetEase(Ease.Linear);
                    _currentPosition = _midllePosition;
                    break;
                case 1.5f:
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine(SetJumpHightCoroutine());
        }
    }

    private IEnumerator SetJumpHightCoroutine()
    {
        Debug.Log("Старт коротин");
        float time = 0;
        while (time < _jumpDuration)
        {
            float jumpProgress = time / _jumpDuration;
            Debug.Log(jumpProgress);
            transform.position = new Vector3(transform.position.x, _jumpCurve.Evaluate(jumpProgress), transform.position.z);
            time += Time.deltaTime;
            yield return null;
        }

    }
}
