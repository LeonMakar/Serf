using Serfe.Bonuses.Negative;
using Serfe.Bonuses.Negative.Abstraction;
using Serfe.PlayerSystems;
using UnityEngine;
using UnityEngine.Splines;

public abstract class MoovingBonus : MonoBehaviour, IObstacle
{
    [SerializeField] private WhenNotBeDamagedType _whenNotBeDamagedType;
    public BonusPosition BonusPosition;
    public SplineAnimate SplineAnimate;

    public WhenNotBeDamagedType WhenNotBeDamagedType { get => _whenNotBeDamagedType; set { } }

    public void ApllyDamage(IRunningDataConnector connector)
    {
        connector.GetRunningData()?.MinusHealth();
    }

    public void StartSplineAnimate(SplineContainer splineContainer)
    {
        SplineAnimate.ElapsedTime = 0;
        SplineAnimate.NormalizedTime = 0;
        SplineAnimate.Container = splineContainer;
        SplineAnimate.Play();

    }

}
public enum BonusPosition
{
    Left,
    Right,
    Middle
}
