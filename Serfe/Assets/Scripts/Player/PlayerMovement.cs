using DG.Tweening;
using Serfe.EventBusSystem;
using Serfe.EventBusSystem.Signals;
using Serfe.Models;
using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    public float JumpHight;

    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private float _jumpDuration;
    private float _cachedSpeed;

    private float _currentPosition;
    private Vector3 _moovingDirection = Vector3.forward;
    private Coroutine _cashedCoroutine;
    private RunningData _runningData;
    private EventBus _eventBus;
    private WaitForSeconds _delay = new WaitForSeconds(1);
    private OnScoreChangeSignal _scoreChangeSignal = new OnScoreChangeSignal();
    private OnCheckPlayerPosition _playerPositionSignal = new OnCheckPlayerPosition();
    private bool _gameIsActive = false;

    [Inject]
    private void Construct(EventBus eventBus, RunningData runningData)
    {
        _runningData = runningData;
        _eventBus = eventBus;
        _eventBus.Subscrube<OnUpMoveSignal>(OnJump);
        _eventBus.Subscrube<OnDownMoveSignal>(OnMoveDown);
        _eventBus.Subscrube<OnHorizontalMoveSignal>(OnHorizontalMove);
        _eventBus.Subscrube<OnGameOverSignal>(OnGameOver);
    }
    private void OnDestroy()
    {
        _eventBus.Unsubscribe<OnUpMoveSignal>(OnJump);
        _eventBus.Unsubscribe<OnDownMoveSignal>(OnMoveDown);
        _eventBus.Unsubscribe<OnHorizontalMoveSignal>(OnHorizontalMove);
        _eventBus.Unsubscribe<OnGameOverSignal>(OnGameOver);

    }
    private void Start()
    {
        StartCoroutine(SendScoreCoroutine());
        StartCoroutine(SendPlayerPosition());
    }

    private IEnumerator SendPlayerPosition()
    {
        while (true)
        {
            if (_gameIsActive)
            {
                _playerPositionSignal.Init(transform.position.z);
                _eventBus?.Invoke<OnCheckPlayerPosition>(_playerPositionSignal);
                yield return _delay;
            }
            yield return _delay;
        }

    }

    private void FixedUpdate()
    {
        if (_gameIsActive)
        {
            _rigidBody.MovePosition(_rigidBody.position + (_moovingDirection * (_runningData.RunningSpeed * Time.fixedDeltaTime)));
            _rigidBody.MoveRotation(Quaternion.identity);
        }
    }

    private void OnGameOver(OnGameOverSignal gameOverSignal)
    {
        if (gameOverSignal.IsGameOver)
        {
            _gameIsActive = false;
            _rigidBody.isKinematic = true;
        }
        else
        {
            transform.position = Vector3.zero;
            _gameIsActive = true;
            _rigidBody.isKinematic = false;
        }

    }
    private void OnHorizontalMove(OnHorizontalMoveSignal signal)
    {
        switch (signal.MovementType)
        {
            case MovementType.Left:
                switch (_currentPosition)
                {
                    case GameConstants.MIDDLE_POSITION:
                        MoveRigidBodyAroundX(GameConstants.LEFT_POSITION);
                        break;
                    case GameConstants.RIGHT_POSITION:
                        MoveRigidBodyAroundX(GameConstants.MIDDLE_POSITION);
                        break;
                }
                break;
            case MovementType.Right:
                switch (_currentPosition)
                {
                    case GameConstants.MIDDLE_POSITION:
                        MoveRigidBodyAroundX(GameConstants.RIGHT_POSITION);
                        break;
                    case GameConstants.LEFT_POSITION:
                        MoveRigidBodyAroundX(GameConstants.MIDDLE_POSITION);
                        break;
                }
                break;
        }
    }

    private void MoveRigidBodyAroundX(float positionXToMove)
    {
        _rigidBody.DOMoveX(positionXToMove, 0.3f).SetEase(Ease.Linear);
        _currentPosition = positionXToMove;
    }



    private void OnMoveDown(OnDownMoveSignal onDownMoveSignal)
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 15))
        {
            if (_cashedCoroutine != null)
                StopCoroutine(_cashedCoroutine);
            _rigidBody.DOMoveY(hitInfo.transform.position.y, _jumpDuration);
        }
    }

    private void OnJump(OnUpMoveSignal onUpMoveSignal)
    {
        if (_cashedCoroutine != null)
            StopCoroutine(_cashedCoroutine);
        _cashedCoroutine = StartCoroutine(SetJumpHightCoroutine());
    }

    private IEnumerator SetJumpHightCoroutine()
    {
        float time = 0;
        float currentHight = _rigidBody.position.y;
        while (time < _jumpDuration)
        {
            float jumpProgress = time / _jumpDuration;

            _rigidBody.position = new Vector3(_rigidBody.position.x, currentHight + _jumpCurve.Evaluate(jumpProgress) * JumpHight, _rigidBody.position.z);
            time += Time.deltaTime;
            yield return null;
        }
    }
    private IEnumerator SendScoreCoroutine()
    {
        while (true)
        {
            if (_gameIsActive)
            {
                float lastZPosition = transform.position.z;
                yield return _delay;
                float currentZPosition = transform.position.z;
                int scoreToAdd = Mathf.FloorToInt(currentZPosition - lastZPosition);
                _scoreChangeSignal.Init(scoreToAdd);
                _eventBus.Invoke(_scoreChangeSignal);
            }
            else
                yield return _delay;
        }
    }
}
