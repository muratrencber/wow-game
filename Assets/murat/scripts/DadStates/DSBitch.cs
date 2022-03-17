using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSBitch : DadState
{
    public override DadStateType Type {get{return DadStateType.BITCH;}}
    [SerializeField] DadLine[] lines;
    [SerializeField] MinMax _pushForce;
    [SerializeField] float _negativeToleranceEffect;

    public override void OnStateStarted()
    {
        DadLine l = DadLine.GetOptimalLine(lines);
        dad.IncreaseNegativeTolerance(_negativeToleranceEffect);
        Slider.AddForce(_pushForce.GetRandom());
        DadNotification.Show(l);
        dad.ChangeState(DadStateType.WAIT);
    }

}
