using DG.Tweening;
using Serfe.CollectablesSystems;
using Serfe.PlayerSystems;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Magnete : Collectables
{
    protected override void ActivateCollectablesAction(Collider other)
    {
        if (other.gameObject.layer == GameConstants.PLAYER_LAYER)
            if (other.TryGetComponent(out PlayerEnvironmentHandler playerEnvironmentHandler))
            {
                playerEnvironmentHandler.StartCoroutine(ActivateMagnite(playerEnvironmentHandler.transform, playerEnvironmentHandler));
                gameObject.SetActive(false);
            }
    }

    private IEnumerator ActivateMagnite(Transform playerTranfrom, PlayerEnvironmentHandler playerEnvironmentHandler)
    {
        float magneteDuration = 0;
        while (magneteDuration < 10)
        {
            Collider[] colliders = Physics.OverlapSphere(playerTranfrom.position, 5, 1 << GameConstants.COIN_LAYER);
            if (colliders.Length > 0)
            {
                foreach (Collider collider in colliders)
                {
                    if (collider.GetComponent<Coin>().IsStartMooving == false)
                        playerEnvironmentHandler.StartCoroutine(CoinMoveCoroutine(collider, playerTranfrom));
                }
            }
            yield return new WaitForSeconds(0.5f);
            magneteDuration += 0.5f;
        }
    }

    private IEnumerator CoinMoveCoroutine(Collider coinCollider, Transform playerTransform)
    {
        coinCollider.GetComponent<Coin>().IsStartMooving = true;
        Vector3 initCoinPosition = coinCollider.transform.position;
        coinCollider.transform.DOMove(playerTransform.position, 0.3f);
        yield return new WaitForSeconds(0.3f);
        coinCollider.transform.position = initCoinPosition;
        coinCollider.gameObject.SetActive(false);
    }


}
