using UnityEngine;

public class DSSetNeed : DadState
{
    public override DadStateType Type {get {return DadStateType.SET_NEED;}}
    [SerializeField] MinMax _bitchThreshold;
    [SerializeField] DadLine[] _lines;
    public override void OnStateStarted()
    {
        string[] needs = {"tea", "book", "tv"};
        dad.SetCurrentNeed(needs[Random.Range(0, needs.Length)]);
        if(FinishLoader.Failed)
        {
            DadNotification.Show(DadLine.GetOptimalLine(_lines));
            dad.ChangeState(DadStateType.WAIT_FOR_NEED);
        }
        else if(Dad.NegativeTolerance == 1 || Dad.Tolerance == 1)
            dad.ChangeState(DadStateType.WAIT_FOR_NEED);
        else if(Dad.NegativeTolerance > Dad.Tolerance && Dad.NegativeTolerance > _bitchThreshold.min && Dad.NegativeTolerance < _bitchThreshold.max)
            dad.ChangeState(DadStateType.BITCH);
        else
        {
            Slider.SlideUntilRatio(0.75f);
            DadNotification.Show(DadLine.GetOptimalLine(_lines));
            dad.ChangeState(DadStateType.WAIT_FOR_NEED);
        }
    }
}
