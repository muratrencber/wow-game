using UnityEngine;

public enum DadStateType
{
    WAIT,
    SET_NEED,
    WAIT_FOR_NEED,
    CONSUME_TEA,
    CONSUME_READABLE,
    CONSUME_TV,
    BE_MORE_TOLERANT,
    DO_TOLERANT_TALK,
    OBSTRUCT,
    BE_MORE_NEGATIVE,
    RESPOND_TO_DECLINE,
    PUNISH,
    CONFESS,
    DO_FINAL_TALK,
    CHANGE_MIND,
    BITCH
}

[System.Serializable]
public class DadStateAndScript
{
    public DadStateType type;
    public DadState state;
}

public class DadState : MonoBehaviour
{
    public virtual DadStateType Type {get {return _type;}}
    protected Dad dad {get; private set;}
    [SerializeField] DadStateType _type;
    public void SetDad(Dad d) => dad = d;
    public virtual void OnReceivedItem(IDadItem item){}
    public virtual void OnStateStarted(){}
    public virtual void OnStateUpdate(){}
    public virtual void OnStateFinished(){}
}
