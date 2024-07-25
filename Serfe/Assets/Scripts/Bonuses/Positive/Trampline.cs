using Serfe.PlayerSystems;
using UnityEngine;

public class Trampline : Collectables
{
    protected override void ActivateCollectablesAction(Collider other)
    {
        if (other.gameObject.layer == GameConstants.PLAYER_LAYER)
        {
            if (other.TryGetComponent(out PlayerEnvironmentHandler playerEnvironmentHandler))
            {
                gameObject.SetActive(false);
                playerEnvironmentHandler.StartCoroutine(playerEnvironmentHandler.ActivateTrampline());
            }
        }
    }
}