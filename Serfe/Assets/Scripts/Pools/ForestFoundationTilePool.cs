using Serfe.TileContainer;

namespace Serfe.Pools
{
    public class ForestFoundationTilePool : CustomePool<FoundationTile>
    {
        protected override FoundationTile CreateGameObjectForPool()
        {
            var prefabGameObject = _factory.Create(TileType.Forest);
            prefabGameObject.SetActive(false);
            _gameObjcetsList.Add(prefabGameObject.GetComponent<FoundationTile>());
            return prefabGameObject.GetComponent<FoundationTile>();
        }
    }
}