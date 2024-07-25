using Serfe.Models;
using Serfe.PlayerSystems;
using System.Collections;
using UnityEngine;

public class Invincible : Collectables
{
    protected override void ActivateCollectablesAction(Collider other)
    {
        if (other.gameObject.layer == GameConstants.PLAYER_LAYER)
        {
            if (other.TryGetComponent(out PlayerEnvironmentHandler playerEnvironmentHandler))
            {
                gameObject.SetActive(false);
                playerEnvironmentHandler.StartCoroutine(SetPlayerIncincible(playerEnvironmentHandler.GetRunningData()));
            }
        }
    }
    private IEnumerator SetPlayerIncincible(RunningData runningData)
    {
        runningData.SetHealthInvincible();

        runningData.RunningSpeed = 15;
        Debug.Log("Invincible = " + runningData.Health);
        yield return new WaitForSeconds(runningData.TramplineDuration);
        runningData.SetHealthStandart();
        runningData.RunningSpeed = 10;
    }
}
