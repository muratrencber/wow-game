using UnityEngine;

public class DSConfessFeelings : DadState
{
    public override DadStateType Type {get{return DadStateType.CONFESS;}}
    [SerializeField] DadLine[] lines;
    bool didTalk = false;

    public override void OnStateStarted()
    {
        if(didTalk)
        {
            dad.ChangeState(DadStateType.WAIT);
            return;
        }
        Slider.AddForce(Dad.LOCK_PUSH_FORCE, true);
        DadNotification.Show(lines);
    }

    public override void OnStateUpdate()
    {
        if(!DadNotification.IsBusy)
            dad.ChangeState(DadStateType.WAIT);
    }

    public override void OnStateFinished()
    {
        didTalk = true;
        Slider.Unlock();
        Slider.AddForce(-Dad.LOCK_PUSH_FORCE);
    }
}
