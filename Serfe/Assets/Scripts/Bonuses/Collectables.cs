using UnityEngine;

public abstract class Collectables : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ActivateCollectablesAction(other);
    }

    protected abstract void ActivateCollectablesAction(Collider other);
}
