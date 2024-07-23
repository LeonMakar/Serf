using Serfe.EventBusSystem;
using Serfe.TileContainer;
using UnityEngine;
using UnityEngine.Splines;
using Zenject;

public class TileAdjuster : MonoBehaviour
{
    public TileType TileType;
    [SerializeField] private SplineInstantiate _leftSide;
    [SerializeField] private SplineInstantiate _rightSide;
    [field: SerializeField] public SplineContainer _leftSpline { get; private set; }
    [field: SerializeField] public SplineContainer _midlleSpline { get; private set; }
    [field: SerializeField] public SplineContainer _rightSpline { get; private set; }

    [SerializeField] private NodeEnd _endNode;
    private EventBus _eventBus;

    [Inject]
    private void Construct(EventBus eventBus)
    {
        _eventBus = eventBus;

    }

    public void RandomizeSidesOfNode(NodeGenerator nodeGenerator)
    {
        _leftSide.Randomize();
        _rightSide.Randomize();
        _endNode.Init(nodeGenerator);
    }
}
