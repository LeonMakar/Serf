using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    #region Events
    public event Action<Vector2, float> OnStartTouchEvent;
    public event Action<Vector2, float> OnEndTouchEvent;

    #endregion
    private StandartInput _standartInput;

    private void Awake()
    {
        _standartInput = new StandartInput();

    }
    private void OnEnable()
    {
        _standartInput.Enable();
        _standartInput.Standart.Enable();
        Debug.Log("Тач включеё");
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
        Debug.Log("StartTouch");

    }
}
