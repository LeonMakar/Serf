using Serfe.EventBusSystem;
using Serfe.EventBusSystem.Signals;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputManager : MonoBehaviour
{
    #region Events
    public event Action<Vector2, float> OnStartTouchEvent;
    public event Action<Vector2, float> OnEndTouchEvent;

    #endregion
    private StandartInput _standartInput;
    private EventBus _eventBus;
    private Vector3 _swipeEndPosition;
    private Vector3 _swipeStartPosition;
    private bool _isTouchStarted;
    private Touch _touch;

    [Inject]
    private void Construct(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    private void Awake()
    {
        _standartInput = new StandartInput();

    }
    private void OnEnable()
    {
        _standartInput.Enable();
        _standartInput.Standart.Enable();
    }
    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began)
        {
            _swipeStartPosition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Ended)
        {

            _swipeEndPosition = Input.GetTouch(0).position;
            Vector2 direction = _swipeEndPosition - _swipeStartPosition;
            direction.Normalize();
            float directionThreshold = 0.9f;

            if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
                _eventBus.Invoke(new OnUpMoveSignal());
            else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
                _eventBus.Invoke(new OnDownMoveSignal());
            else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
                _eventBus.Invoke(new OnHorizontalMoveSignal(MovementType.Right));
            else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
                _eventBus.Invoke(new OnHorizontalMoveSignal(MovementType.Left));

        }


        if (Input.GetKeyDown(KeyCode.LeftArrow))
            _eventBus.Invoke(new OnHorizontalMoveSignal(MovementType.Left));



        if (Input.GetKeyDown(KeyCode.RightArrow))
            _eventBus.Invoke(new OnHorizontalMoveSignal(MovementType.Right));


        if (Input.GetKeyDown(KeyCode.UpArrow))
            _eventBus.Invoke(new OnUpMoveSignal());

        if (Input.GetKeyDown(KeyCode.DownArrow))
            _eventBus.Invoke(new OnDownMoveSignal());

    }

    private IEnumerator TouchCoroutine()
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
        _isTouchStarted = false;
        yield return null;
    }

    private void OnDisable()
    {
        _standartInput.Disable();
    }
    void Start()
    {
        _standartInput.Standart.PrimaryContact.started += StartTouchPrimaryDetection;

        _standartInput.Standart.PrimaryContact.canceled += EndTouchPrimaryDetection;
    }

    private void ReadKey(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValueAsObject());
    }

    private void EndTouchPrimaryDetection(InputAction.CallbackContext context)
    {
        OnEndTouchEvent?.Invoke(context.ReadValue<Vector2>(), (float)context.time);
    }

    private void StartTouchPrimaryDetection(InputAction.CallbackContext context)
    {
        OnStartTouchEvent?.Invoke(context.ReadValue<Vector2>(), (float)context.time);
        _eventBus.Invoke(new OnUpMoveSignal());
        Debug.Log("StartTouch");

    }
}
