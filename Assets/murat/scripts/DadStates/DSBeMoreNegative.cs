using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSBeMoreNegative : DadState
{
    public override DadStateType Type {get{return DadStateType.BE_MORE_NEGATIVE;}}
    
    [SerializeField] float _negativeEffect, _punishmentTolerance;
    [SerializeField] DadLine[] _lines;

    public override void OnStateStarted()
    {
        dad.IncreaseNegativeTolerance(_negativeEffect);
        if(Dad.NegativeTolerance == 1)
        {
            dad.ChangeState(DadStateType.CONFESS);
        }
        //else if(Dad.Tolerance > _punishmentTolerance)
        //{
        //    dad.ChangeState(DadStateType.PUNISH);
        //}
        else
        {
            DadNotification.Show(DadLine.GetOptimalLine(_lines));
            dad.ChangeState(DadStateType.WAIT);
        }
    }
}
