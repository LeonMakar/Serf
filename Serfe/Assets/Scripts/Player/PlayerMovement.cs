using DG.Tweening;
using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _speed;

    private float _currentPosition;
    private float _leftPosition = -1.5f;
    private float _midllePosition = 0;
    private float _rightPosition = 1.5f;
    private Vector3 _moovingDirection = Vector3.forward;
    private Coroutine _cashedCoroutine;
    private EventBus _eventBus;

    [Inject]
    private void Construct(EventBus eventBus)
    {
        _eventBus = eventBus;
        _eventBus.OnJumpMoveSignalStarted += OnJump;
        _eventBus.OnLeftMoveSignalStarted += OnLeftMove;
        _eventBus.OnRightMoveSignalStarted += OnRightMove;
        _eventBus.OnDownMoveSignalStarted += OnMoveDown;
    }

    private void FixedUpdate()
    {
        _rigidBody.MovePosition(_rigidBody.position + (_moovingDirection * (_speed * Time.deltaTime)));
        _rigidBody.MoveRotation(Quaternion.identity);
    }

    private void OnMoveDown()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 15))
        {
            StopCoroutine(_cashedCoroutine);
            _rigidBody.DOMoveY(hitInfo.transform.position.y, _jumpDuration);
        }
    }

    private void OnJump()
    {
        if (_cashedCoroutine != null)
            StopCoroutine(_cashedCoroutine);
        _cashedCoroutine = StartCoroutine(SetJumpHightCoroutine());
    }
    private void OnRightMove()
    {
        switch (_currentPosition)
        {
            case 0:
                _rigidBody.DOMoveX(_rightPosition, 0.3f).SetEase(Ease.Linear);
                _currentPosition = _rightPosition;
                break;
            case -1.5f:
                _rigidBody.DOMoveX(_midllePosition, 0.3f).SetEase(Ease.Linear);
                _currentPosition = _midllePosition;
                break;
            case 1.5f:
                break;
        }
    }

    private void OnLeftMove()
    {
        switch (_currentPosition)
        {
            case 0:
                _rigidBody.DOMoveX(_leftPosition, 0.3f).SetEase(Ease.Linear);
                _currentPosition = _leftPosition;
                break;
            case -1.5f:
                break;
            case 1.5f:
                _rigidBody.DOMoveX(_midllePosition, 0.3f).SetEase(Ease.Linear);
                _currentPosition = _midllePosition;
                break;
        }
    }

    private IEnumerator SetJumpHightCoroutine()
    {
        float time = 0;
        float currentHight = _rigidBody.position.y;
        while (time < _jumpDuration)
        {
            float jumpProgress = time / _jumpDuration;

            _rigidBody.position = new Vector3(_rigidBody.position.x, currentHight + _jumpCurve.Evaluate(jumpProgress), _rigidBody.position.z);
            time += Time.deltaTime;
            yield return null;
        }

    }

}
