using Serfe.TileContainer;
using System;
using UnityEngine;

namespace Serfe.Factory
{
    public interface IFactory
    {
        GameObject Create();
        GameObject Create(Enum tileType);
    }
}