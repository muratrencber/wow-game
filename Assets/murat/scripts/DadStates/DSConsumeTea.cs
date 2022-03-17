using UnityEngine;

public class DSConsumeTea : DadState
{
    public override DadStateType Type {get {return DadStateType.CONSUME_TEA;}}
    [SerializeField] MinMax _waitBetweenSips, _sipValues;
    [SerializeField] DadLine[] _lines;
    teacup cup;
    float timer;

    public override void OnStateStarted()
    {
        cup = GameObject.FindObjectOfType<teacup>();
        timer = _waitBetweenSips.GetRandom();
    }

    public override void OnStateUpdate()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
                Sip();
        }
    }

    void Sip()
    {
        timer = _waitBetweenSips.GetRandom();
        cup.fillrate_fall(_sipValues.GetRandom());
        if(cup.fillrate <= 0)
        {
            DadNotification.Show(DadLine.GetOptimalLine(_lines));
            dad.ChangeState(DadStateType.WAIT);
        }

    }
}
