using UnityEngine;

public class DSBeMoreTolerant : DadState
{
    public override DadStateType Type {get{return DadStateType.BE_MORE_TOLERANT;}}
    
    [SerializeField] float _bookToleranceBoost, _tvToleranceBoost;
    [SerializeField] float _conversationThreshold;
    [SerializeField] DadLine[] _lines;

    public override void OnStateStarted()
    {
        if(Dad.CurrentItem == null)
            dad.ChangeState(DadStateType.WAIT);
        else
        {
            float increase = Dad.CurrentItem.Key == "book" || Dad.CurrentItem.Key == "magazine" ? _bookToleranceBoost : _tvToleranceBoost;
            dad.IncreaseTolerance(increase);
            if(Dad.Tolerance == 1)
            {
                dad.ChangeState(DadStateType.DO_FINAL_TALK);
            }
            else if(Dad.Tolerance > _conversationThreshold)
            {
                dad.ChangeState(DadStateType.DO_TOLERANT_TALK);
            }
            else
            {
                DadNotification.Show(DadLine.GetOptimalLine(_lines));
                dad.ChangeState(DadStateType.WAIT);
            }
        }
    }
}
