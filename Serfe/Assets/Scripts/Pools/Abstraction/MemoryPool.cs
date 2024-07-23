using Serfe.Factory;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MemoryPool<T> where T : MonoBehaviour
{
    private Dictionary<Enum, T> _memoryPrefabs = new Dictionary<Enum, T>();
    private IFactory _factory;

    public void InitMemoryPool(IFactory factory, Dictionary<Enum, T> dictionary)
    {
        _factory = factory;

        foreach (var item in dictionary)
            CreateGameObjectForPool(item.Key);

    }

    public T GetFromPool(Enum @enum)
    {
        var gameObject = _memoryPrefabs[@enum];
        if (gameObject.isActiveAndEnabled)
            gameObject = CreateGameObjectForPool(@enum);
        gameObject.gameObject.SetActive(true);
        return gameObject;
    }

    private T CreateGameObjectForPool(Enum @enum)
    {
        var prefabGameObject = _factory.Create(@enum);
        prefabGameObject.SetActive(false);
        if (!_memoryPrefabs.ContainsKey(@enum))
            _memoryPrefabs.Add(@enum, prefabGameObject.GetComponent<T>());
        return prefabGameObject.GetComponent<T>();
    }
}
