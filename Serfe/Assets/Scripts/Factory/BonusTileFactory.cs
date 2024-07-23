using Serfe.Factory;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BonusTileFactory : IFactory
{
    public Dictionary<Enum, TileBonusPattern> PrefabsOfTileWhithType { get; private set; } = new Dictionary<Enum, TileBonusPattern>();

    public BonusTileFactory(List<TileBonusPattern> tileBonusPatterns)
    {
        foreach (var tileAdjuster in tileBonusPatterns)
            PrefabsOfTileWhithType.Add(tileAdjuster.BonusTileType, tileAdjuster);
    }
    public GameObject Create()
    {
        throw new NotImplementedException();
    }

    public GameObject Create(Enum tileType)
    {
        GameObject copyOfObject = GameObject.Instantiate(PrefabsOfTileWhithType[tileType].gameObject);
        return copyOfObject;
    }
}
