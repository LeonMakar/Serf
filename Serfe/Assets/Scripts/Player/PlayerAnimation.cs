using UnityEngine;
using Zenject;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;



    private int _isRunning = Animator.StringToHash("isRunning");
    private int _isJump = Animator.StringToHash("isJump");


    [Inject]
    private void Construct(EventBus eventBus)
    {
        eventBus.OnJumpMoveSignalStarted += Jump;
    }

    private void Jump()
    {
        _animator.SetTrigger(_isJump);
    }

    private void Start()
    {
        //_animator.SetBool().
    }
}
