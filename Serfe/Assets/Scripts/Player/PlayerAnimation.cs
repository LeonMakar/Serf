using Serfe.EventBusSystem;
using Serfe.EventBusSystem.Signals;
using UnityEngine;
using Zenject;

public class PlayerAnimation
{
    private Animator _animator;

    private int _isRunning = Animator.StringToHash("isRunning");
    private int _isJump = Animator.StringToHash("isJump");
    private int _isRolling = Animator.StringToHash("isRolling");
    private int _isDeath = Animator.StringToHash("isDeath");
    private EventBus _eventBus;

    [Inject]
    private void Construct(EventBus eventBus, Animator animator)
    {
        _animator = animator;
        _eventBus = eventBus;
        eventBus.Subscrube<OnUpMoveSignal>(Jump);
        eventBus.Subscrube<OnDownMoveSignal>(Rolling);
        eventBus.Subscrube<OnGameOverSignal>(Death);
    }

    private void OnDestroy()
    {
        _eventBus.Unsubscribe<OnGameOverSignal>(Death);
        _eventBus.Unsubscribe<OnUpMoveSignal>(Jump);
        _eventBus.Unsubscribe<OnDownMoveSignal>(Rolling);
    }
    private void Death(OnGameOverSignal onGameOverSignal) => _animator.SetTrigger(_isDeath);
    private void Rolling(OnDownMoveSignal onDownMoveSignal) => _animator.SetTrigger(_isRolling);


    private void Jump(OnUpMoveSignal onUpMoveSignal) => _animator.SetTrigger(_isJump);

}
