using Serfe.PlayerSystems;
using UnityEngine;

namespace Serfe.Bonuses.Negative.Abstraction
{
    public interface IObstacle
    {
        WhenNotBeDamagedType WhenNotBeDamagedType { get; set; }
        void ApllyDamage(IRunningDataConnector connector);
    }
}