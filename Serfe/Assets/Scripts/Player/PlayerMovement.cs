using DG.Tweening;
using Serfe.EventBusSystem;
using Serfe.EventBusSystem.Signals;
using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _jumpHight;
    [SerializeField] private float _speed;

    private float _currentPosition;
    private Vector3 _moovingDirection = Vector3.forward;
    private Coroutine _cashedCoroutine;
    private EventBus _eventBus;
    private WaitForSeconds _delay = new WaitForSeconds(1);
    private OnScoreChangeSignal _scoreChangeSignal = new OnScoreChangeSignal();
    private bool _gameIsactive = true;

    [Inject]
    private void Construct(EventBus eventBus)
    {
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
    }
    private void FixedUpdate()
    {
        _rigidBody.MovePosition(_rigidBody.position + (_moovingDirection * (_speed * Time.deltaTime)));
        _rigidBody.MoveRotation(Quaternion.identity);
    }

    private void OnGameOver(OnGameOverSignal gameOverSignal)
    {
        _speed = 0;
        _gameIsactive = false;
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

            _rigidBody.position = new Vector3(_rigidBody.position.x, currentHight + _jumpCurve.Evaluate(jumpProgress) * _jumpHight, _rigidBody.position.z);
            time += Time.deltaTime;
            yield return null;
        }
    }
    private IEnumerator SendScoreCoroutine()
    {
        while (true)
        {
            if (_gameIsactive)
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
