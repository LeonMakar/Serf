using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePattern : MonoBehaviour
{
    [field: SerializeField] public List<BonusTile> BonusTiles { get; private set; }
}
