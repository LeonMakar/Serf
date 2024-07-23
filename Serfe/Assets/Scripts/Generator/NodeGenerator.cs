using Serfe.EventBusSystem;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class NodeGenerator : MonoBehaviour
{
    [SerializeField] private List<TileAdjuster> _tiles = new List<TileAdjuster>();

    [SerializeField] private List<TileBonusPattern> _tilesBonuses = new List<TileBonusPattern>();
    [SerializeField] private GameObject _tileParent;

    private int _lastTilePosition = 0;
    private EventBus _eventBus;
    private MemoryPool<TileBonusPattern> _tileBonusMemoryPool;
    private MemoryPool<TileAdjuster> _tileMemoryPool;

    [Inject]
    private void Construct(EventBus eventBus, MemoryPool<TileAdjuster> tileMemoryPool, MemoryPool<TileBonusPattern> tileBonusMemoryPool)
    {
        _eventBus = eventBus;
        _tileBonusMemoryPool = tileBonusMemoryPool;
        _tileMemoryPool = tileMemoryPool;
    }


    public void GenerateNewTile()
    {
        var tile = _tileMemoryPool.GetFromPool(_tiles[Random.Range(0, _tiles.Count)].TileType);
        tile.transform.SetParent(_tileParent.transform);
        var tileBonus = _tileBonusMemoryPool.GetFromPool(_tilesBonuses[Random.Range(0, _tilesBonuses.Count)].BonusTileType);
        tileBonus.transform.SetParent(_tileParent.transform);

        tile.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, _lastTilePosition);
        tile.RandomizeSidesOfNode(this);
        tileBonus.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, _lastTilePosition);
        if (tileBonus.MoovingBonusesList.Count > 0)
        {
            foreach (var item in tileBonus.MoovingBonusesList)
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

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            GenerateNewTile();
        }
    }
}
