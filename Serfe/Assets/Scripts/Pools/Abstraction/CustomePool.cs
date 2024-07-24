using Serfe.Factory;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomePool<T> where T : MonoBehaviour
{
    protected List<T> _gameObjcetsList;

    protected IFactory _factory;


    public void InitPool(IFactory factory, int startCountOfObjects)
    {
        _gameObjcetsList = new List<T>();
        _factory = factory;
        for (int i = 0; i < startCountOfObjects; i++)
            CreateGameObjectForPool();

    }
    public void RemooveAllObject()
    {
        var gameObjecs = _gameObjcetsList.FindAll(x => x.isActiveAndEnabled);
        foreach (var gameObj in gameObjecs)
        {
            DropBackToPool(gameObj);
        }
    }

    public T GetFromPool()
    {
        var gameObject = _gameObjcetsList.FirstOrDefault(x => !x.isActiveAndEnabled);
        if (gameObject == null)
            gameObject = CreateGameObjectForPool();
        gameObject.gameObject.SetActive(true);
        return gameObject;
    }

    public void DropBackToPool(T gameObject) => gameObject.gameObject.SetActive(false);

    protected virtual T CreateGameObjectForPool()
    {

        var prefabGameObject = _factory.Create();
        prefabGameObject.SetActive(false);
        _gameObjcetsList.Add(prefabGameObject.GetComponent<T>());
        return prefabGameObject.GetComponent<T>();
    }
}
