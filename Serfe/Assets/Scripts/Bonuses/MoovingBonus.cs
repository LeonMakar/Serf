using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public abstract class MoovingBonus : MonoBehaviour
{
    public BonusPosition BonusPosition;
    public SplineAnimate SplineAnimate;


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
