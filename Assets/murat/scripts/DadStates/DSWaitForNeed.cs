using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSWaitForNeed : DadState
{
    public override DadStateType Type {get {return DadStateType.WAIT_FOR_NEED;}}
    [SerializeField] MinMax _waitTimeAfterWanting;

    float timer;
    bool isNeedSatisfied;

    public override void OnStateStarted()
    {
        timer = _waitTimeAfterWanting.GetRandom();
    }

    public override void OnReceivedItem(IDadItem item)
    {
        if(item.Key == "tea")
            dad.ChangeState(DadStateType.CONSUME_TEA);
        if(item.Key == "magazine" || item.Key == "book")
            dad.ChangeState(DadStateType.CONSUME_READABLE);
    }

    public override void OnStateUpdate()
    {
        if(Dad.CurrentNeed == "tv" && TV.IsOn)
        {
            dad.ChangeState(DadStateType.CONSUME_TV);
        }
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                if(Dad.Tolerance == 1 || Dad.NegativeTolerance == 1)
                {
                    dad.ChangeState(DadStateType.WAIT);
                }
                else
                {
                    dad.ChangeState(DadStateType.OBSTRUCT);
                }
            }
        }
    }
}
