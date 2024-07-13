using UnityEngine;

public class NodeEnd : MonoBehaviour
{
    private NodeGenerator _nodeGenerator;
    [SerializeField] TileAdjuster _tileAdjuster;

    public void Init(NodeGenerator nodeGenerator) => _nodeGenerator = nodeGenerator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
            _nodeGenerator.GenerateNewTile();
        else if (other.gameObject.layer == 7)
        {
            Debug.Log("Машина вошла в тригер");

            if (other.transform.parent.TryGetComponent(out MoovingBonus movingBonus))
            {
                switch (movingBonus.BonusPosition)
                {
                    case BonusPosition.Left:
                        movingBonus.StartSplineAnimate(_tileAdjuster._leftSpline);
                        break;
                    case BonusPosition.Middle:
                        movingBonus.StartSplineAnimate(_tileAdjuster._midlleSpline);
                        break;
                    case BonusPosition.Right:
                        movingBonus.StartSplineAnimate(_tileAdjuster._rightSpline);
                        break;
                }
            }
        }

    }
}
