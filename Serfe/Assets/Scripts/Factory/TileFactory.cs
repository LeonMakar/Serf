using Serfe.Factory;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TileFactory : IFactory
{
    public Dictionary<Enum, TileAdjuster> PrefabsOfTileWhithType { get; private set; } = new Dictionary<Enum, TileAdjuster>();

    public TileFactory(List<TileAdjuster> prefabsList)
    {
        foreach (var tileAdjuster in prefabsList)
            PrefabsOfTileWhithType.Add(tileAdjuster.TileType, tileAdjuster);
    }

    public GameObject Create(Enum tileType)
    {
        GameObject copyOfObject = GameObject.Instantiate(PrefabsOfTileWhithType[tileType].gameObject);
        return copyOfObject;
    }

    public GameObject Create()
    {
        throw new System.NotImplementedException();
    }
}
