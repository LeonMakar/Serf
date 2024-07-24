using Serfe.Generator;
using System.Collections.Generic;
using UnityEngine;

public class BonusTile : MonoBehaviour
{
    [field: SerializeField] public TileBonusType BonusTileType { get; private set; }
    [field: SerializeField] public List<GameObject> BonusesList { get; private set; } = new List<GameObject>();
    [field: SerializeField] public List<GameObject> MoovingBonusesList { get; private set; } = new List<GameObject>();
}
