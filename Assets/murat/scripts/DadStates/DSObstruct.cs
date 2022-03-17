using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSObstruct : DadState
{
    public override DadStateType Type {get{return DadStateType.OBSTRUCT;}}
    
    [SerializeField] float _responseWaitTime, _pushForce;
    [SerializeField] DadLine[] _lines;
    [SerializeField] string[] _apologyResponses, _defyResponses;
    float timer;

    public override void OnStateStarted()
    {
        DadNotification.Show(DadLine.GetOptimalLine(_lines));
        string[] responseLines = {_apologyResponses[Random.Range(0, _apologyResponses.Length)], _defyResponses[Random.Range(0, _defyResponses.Length)]};
        System.Action[] responseOutcomes = {OnApologized, OnDefied};
        EvaOptions.Set(responseLines, responseOutcomes, _responseWaitTime);

        Slider.AddForce(_pushForce, true);
        timer = _responseWaitTime;
    }

    void OnApologized(){}

    void OnDefied()
    {
        dad.ChangeState(DadStateType.BE_MORE_NEGATIVE);
    }

    public override void OnStateUpdate()
    {
        if(Dad.CurrentNeed == "tv" && TV.IsOn)
        {
            dad.ChangeState(DadStateType.CONSUME_TV);
        }
    }

    public override void OnReceivedItem(IDadItem item)
    {
        if(item.Key == "tea")
            dad.ChangeState(DadStateType.CONSUME_TEA);
        if(item.Key == "magazine" || item.Key == "book")
            dad.ChangeState(DadStateType.CONSUME_READABLE);
    }

    public override void OnStateFinished()
    {
        Slider.Unlock();
        Slider.AddForce(Dad.UNLOCK_SHOW_FORCE);
    }
}
