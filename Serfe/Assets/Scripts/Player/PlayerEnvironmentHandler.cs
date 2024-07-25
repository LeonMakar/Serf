using Serfe.Bonuses.Negative.Abstraction;
using Serfe.Models;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Serfe.PlayerSystems
{
    public class PlayerEnvironmentHandler : MonoBehaviour, IRunningDataConnector
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private PlayerMovement _playerMovement;
        private RunningData _runningData;
        private bool _isRolling;

        public RunningData GetRunningData() => _runningData;

        public void RollingEnded() => _isRolling = false;
        public void RollingStarted() => _isRolling = true;

        [Inject]
        private void Construct(RunningData data)
        {
            _runningData = data;
        }
        private void Start()
        {
            _runningData.ResetData();
        }

        public IEnumerator ActivateTrampline()
        {
            if (!_runningData.IsTramplineActivated)
            {
                float cachedJumpHight = _playerMovement.JumpHight;
                _playerMovement.JumpHight += 3;
                _runningData.IsTramplineActivated = true;
                Debug.Log(_runningData.TramplineDuration);
                yield return new WaitForSeconds(_runningData.TramplineDuration);
                _playerMovement.JumpHight = cachedJumpHight;
                _runningData.IsTramplineActivated = false;
            }
        }

     
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IObstacle obstacle))
            {
                if (obstacle.WhenNotBeDamagedType != Bonuses.Negative.WhenNotBeDamagedType.Rolling)
                    obstacle.ApllyDamage(this);
                else
                {
                    if (!_isRolling)
                        obstacle.ApllyDamage(this);
                }
            }
        }
    }
}
