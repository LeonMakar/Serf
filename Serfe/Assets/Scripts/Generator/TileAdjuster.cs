using UnityEngine;
using UnityEngine.Splines;

public class TileAdjuster : MonoBehaviour
{
    [SerializeField] private SplineInstantiate _leftSide;
    [SerializeField] private SplineInstantiate _rightSide;
    [field: SerializeField] public SplineContainer _leftSpline { get; private set; } 
    [field: SerializeField] public SplineContainer _midlleSpline { get; private set; } 
    [field: SerializeField] public SplineContainer _rightSpline { get; private set; }

    [SerializeField] private NodeEnd _endNode;

    public void RandomizeSidesOfNode(NodeGenerator nodeGenerator)
    {
        _leftSide.Randomize();
        _rightSide.Randomize();
        _endNode.Init(nodeGenerator);
    }
}
