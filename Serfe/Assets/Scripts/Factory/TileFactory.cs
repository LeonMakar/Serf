using Serfe.TileContainer;
using UnityEngine;
using Zenject;

namespace Serfe.Factory
{
    public class TileFactory : IFactory
    {
        private FoundationTile _foundationTile;
        private DiContainer _container;

        public TileFactory(DiContainer diContainer)
        {
            _container = diContainer;
        }

        public GameObject Create()
        {
            return null;
        }

        public GameObject Create(TileType tileType)
        {
            _foundationTile = _container.ResolveId<FoundationTile>(tileType);
            if (_foundationTile != null)
            {
                FoundationTile foundationTile = GameObject.Instantiate(_foundationTile);
                _container.Inject(foundationTile);
                foreach (var item in foundationTile.TilePattern.BonusTiles)               
                    _container.Inject(item);
                

                _foundationTile = null;
                return foundationTile.gameObject;
            }
            else
            {
                Debug.LogErrorFormat($"Cannot Resolve FoundationTile with Id = {tileType}");
                return null;
            }

        }
    }
}