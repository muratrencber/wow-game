using UnityEngine;

public class DSConsumeTV : DadState
{
    public override DadStateType Type {get {return DadStateType.CONSUME_TV;}}
    [SerializeField] MinMax _tvWatchTime;
    [SerializeField] int _helpfulChannel;
    [SerializeField] DadLine[] _lines;

    float timer;
    public override void OnStateStarted()
    {
        timer = _tvWatchTime.GetRandom();
    }

    public override void OnStateUpdate()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            int channel = TV.CurrentChannel;
            TV.Close();
            if(channel == _helpfulChannel)
            {
                dad.ChangeState(DadStateType.BE_MORE_TOLERANT);
            }
            else
            {
                DadNotification.Show(DadLine.GetOptimalLine(_lines));
                dad.ChangeState(DadStateType.WAIT);
            }
        }
    }
}
