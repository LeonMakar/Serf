using UnityEngine;

public class NodeEnd : MonoBehaviour
{
    private TileGenerator _nodeGenerator;
    [SerializeField] FoundationTile _tileAdjuster;

    public void Init(TileGenerator nodeGenerator) => _nodeGenerator = nodeGenerator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == GameConstants.PLAYER_LAYER)
            _nodeGenerator.GenerateNewTile();
        else if (other.gameObject.layer == GameConstants.MOOVING_BONUS_LAYER)
        {

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
