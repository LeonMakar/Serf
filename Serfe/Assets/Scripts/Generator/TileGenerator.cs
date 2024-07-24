using Serfe.EventBusSystem;
using Serfe.EventBusSystem.Signals;
using Serfe.TileContainer;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _tileParent;

    private int _lastTilePosition = 0;
    private EventBus _eventBus;
    private int _sameTileGenerationCount = 6;

    private Dictionary<TileType, CustomePool<FoundationTile>> _foundationPoolDictionary = new Dictionary<TileType, CustomePool<FoundationTile>>();
    private bool _isGameOver;
    private TileFoundationContainer _tileFoundationContainer;

    [Inject]
    private void Construct(EventBus eventBus, [Inject(Id = TileType.Forest)] CustomePool<FoundationTile> forestFoundationPool,
        TileFoundationContainer tileFoundationContainer)
    {
        _eventBus = eventBus;
        _eventBus.Subscrube<OnGameOverSignal>(StartSpawning);
        _foundationPoolDictionary.Add(TileType.Forest, forestFoundationPool);
        _tileFoundationContainer = tileFoundationContainer;
    }


    public void GenerateNewTile()
    {
        Array tileTypeValues = Enum.GetValues(typeof(TileType));
        TileType randomTileType = (TileType)tileTypeValues.GetValue(UnityEngine.Random.Range(0, tileTypeValues.Length));


        var tile = _foundationPoolDictionary[randomTileType].GetFromPool();
        _tileFoundationContainer.AddNewFoundationTile(_lastTilePosition, tile);
        tile.RandomizeSidesOfNode(this);
        tile.transform.SetParent(_tileParent.transform);
        tile.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, _lastTilePosition);


        var tileBonusPattern = tile.TilePattern;
        var bonusTile = tileBonusPattern.BonusTiles[UnityEngine.Random.Range(0, tileBonusPattern.BonusTiles.Count)];
        bonusTile.gameObject.SetActive(true);


        if (bonusTile.MoovingBonusesList.Count > 0)
        {
            foreach (var item in bonusTile.MoovingBonusesList)
            {
                MoovingBonus moovingBonus = item.GetComponent<MoovingBonus>();
                switch (moovingBonus.BonusPosition)
                {
                    case BonusPosition.Left:
                        moovingBonus.StartSplineAnimate(tile._leftSpline);
                        break;
                    case BonusPosition.Middle:
                        moovingBonus.StartSplineAnimate(tile._midlleSpline);
                        break;
                    case BonusPosition.Right:
                        moovingBonus.StartSplineAnimate(tile._rightSpline);
                        break;
                }
            }
        }
        _lastTilePosition += 20;
    }

    private void StartSpawning(OnGameOverSignal signal)
    {
        _tileFoundationContainer.ResetAllFoundation();
        _isGameOver = signal.IsGameOver;
        _lastTilePosition = 0;
        if (!signal.IsGameOver)
        {
            GenerateEmptyTile();
            for (int i = 0; i < 4; i++)
                GenerateNewTile();
        }
    }

    private void GenerateEmptyTile()
    {
        Array tileTypeValues = Enum.GetValues(typeof(TileType));
        TileType randomTileType = (TileType)tileTypeValues.GetValue(UnityEngine.Random.Range(0, tileTypeValues.Length));


        var tile = _foundationPoolDictionary[randomTileType].GetFromPool();
        tile.RandomizeSidesOfNode(this);
        tile.transform.SetParent(_tileParent.transform);
        tile.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, _lastTilePosition);

        _tileFoundationContainer.AddNewFoundationTile(_lastTilePosition, tile);
        _lastTilePosition += 20;
    }
}
