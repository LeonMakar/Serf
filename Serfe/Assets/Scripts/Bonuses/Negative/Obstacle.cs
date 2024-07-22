using Serfe.Bonuses.Negative.Abstraction;
using Serfe.PlayerSystems;
using UnityEngine;

namespace Serfe.Bonuses.Negative
{
    public class Obstacle : MonoBehaviour, IObstacle
    {
       [SerializeField] private WhenNotBeDamagedType _whenNotBeDamagedType;
        public WhenNotBeDamagedType WhenNotBeDamagedType { get => _whenNotBeDamagedType; set { } }

        public void ApllyDamage(IRunningDataConnector connector)
        {
            connector.GetRunningData()?.MinusHealth();
        }

    }
}