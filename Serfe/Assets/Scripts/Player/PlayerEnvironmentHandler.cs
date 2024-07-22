using Serfe.Bonuses.Negative.Abstraction;
using Serfe.Models;
using UnityEngine;
using Zenject;

namespace Serfe.PlayerSystems
{
    public class PlayerEnvironmentHandler : MonoBehaviour, IRunningDataConnector
    {
        [SerializeField] private Collider _collider;
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

        //private void OnCollisionEnter(Collision collision)
        //{
        //    if (collision.gameObject.TryGetComponent(out IObstacle obstacle))
        //    {
        //        obstacle.ApllyDamage(this);
        //    }
        //}
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
