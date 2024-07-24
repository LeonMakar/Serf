using Serfe.TileContainer;
using UnityEngine;

namespace Serfe.Factory
{
    public interface IFactory
    {
        GameObject Create();
        GameObject Create(TileType tileType);
    }
}