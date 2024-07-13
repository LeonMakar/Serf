using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGenerator : MonoBehaviour
{
    [SerializeField] private List<TileAdjuster> _tiles = new List<TileAdjuster>();
    [SerializeField] private List<TileBonusPattern> _tilesBonuses = new List<TileBonusPattern>();
    [SerializeField] private GameObject _tileParent;


    private int _lastTilePosition = 0;
    public void GenerateNewTile()
    {
        var tile = Instantiate(_tiles[Random.Range(0, _tiles.Count)], _tileParent.transform);
        var tileBonus = Instantiate(_tilesBonuses[Random.Range(0, _tilesBonuses.Count)], _tileParent.transform);
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
        for (int i = 0; i < 3; i++)
        {
            GenerateNewTile();

        }
    }
}
