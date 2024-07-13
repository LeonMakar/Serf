using UnityEngine;

public class Coin : Collectables
{
    [SerializeField] private int _moneyGiveAmmount;
    protected override void ActivateCollectablesAction(Collider other)
    {
        if (other.TryGetComponent(out PlayerBonusesHandler playerBonusesHandler))
        {
            playerBonusesHandler.AddMoney(_moneyGiveAmmount);
        }
        gameObject.SetActive(false);
    }

}
