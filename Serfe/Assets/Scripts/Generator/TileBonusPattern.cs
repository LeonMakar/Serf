using Serfe.EventBusSystem;
using Serfe.Generator;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileBonusPattern : MonoBehaviour
{
    private EventBus _eventBus;

    [field: SerializeField] public TileBonusType BonusTileType { get; private set; }
    [field: SerializeField] public List<GameObject> BonusesList { get; private set; } = new List<GameObject>();
    [field: SerializeField] public List<GameObject> MoovingBonusesList { get; private set; } = new List<GameObject>();

    [Inject]
    private void Construct(EventBus eventBus)
    {
        _eventBus = eventBus;
        Debug.Log("Сработало");
    }
}
