using UnityEngine;

public class DSWait : DadState
{
    public override DadStateType Type {get {return DadStateType.WAIT;}}
    [SerializeField] MinMax _waitBetweenInitial, _waitBetweenMaxTolerance, _waitBetweenMaxNegativeTolerance;
    float waitTimer;

    public override void OnStateStarted()
    {
        waitTimer = GetWaitTime();
    }

    public override void OnStateUpdate()
    {
        if(waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
            if(waitTimer <= 0)
            {
                dad.ChangeState(DadStateType.SET_NEED);
                return;
            }
        }
    }

    float GetWaitTime()
    {
        float wbi = _waitBetweenInitial.GetRandom();
        float wbmt = _waitBetweenMaxTolerance.GetRandom();
        float wbmnt = _waitBetweenMaxNegativeTolerance.GetRandom();

        float w1 = Mathf.Lerp(wbi,wbmt, Dad.Tolerance);
        float w2 = Mathf.Lerp(wbi, wbmnt, Dad.NegativeTolerance);

        return w2 > w1 ? w2 : w1;
    }
}
