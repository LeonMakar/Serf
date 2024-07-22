using Serfe.PlayerSystems;
using UnityEngine;

namespace Serfe.CollectablesSystems
{
    public class Coin : Collectables
    {
        [SerializeField] private int _moneyGiveAmmount;
        protected override void ActivateCollectablesAction(Collider other)
        {
            if (other.TryGetComponent(out IRunningDataConnector runningDataConnector))
            {
                runningDataConnector.GetRunningData()?.AddMoney(_moneyGiveAmmount);
            }
            gameObject.SetActive(false);
        }

    }
}