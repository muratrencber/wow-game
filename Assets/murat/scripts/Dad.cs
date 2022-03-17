using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dad : MonoBehaviour
{
    static Dad instance;
    public const float LOCK_PUSH_FORCE = -250f;
    public const float UNLOCK_SHOW_FORCE = 50f;
    public static string CurrentNeed {get; private set;}
    public static float Tolerance {get; private set;} = 0;
    public static float NegativeTolerance {get; private set;} = 0;
    public static void OnReceivedItem(IDadItem item)
    {
        instance.currentItem = item;
        CurrentState.OnReceivedItem(item);
    } 
    public static bool WaitingForNeed {get {return CurrentStateType == DadStateType.WAIT_FOR_NEED || CurrentStateType == DadStateType.OBSTRUCT;}}
    public static DadState CurrentState {get {return instance?.currentState;}}
    public static DadStateType CurrentStateType {get {return instance.currentState.Type;}}
    public static IDadItem CurrentItem {get {return instance?.currentItem;}}
    public static bool InConversation {get {return CurrentStateType == DadStateType.CONFESS
    || CurrentStateType == DadStateType.DO_FINAL_TALK
    || CurrentStateType == DadStateType.DO_TOLERANT_TALK
    || CurrentStateType == DadStateType.RESPOND_TO_DECLINE;}}

    [SerializeField] MinMax _waitTimeAfterWanting;
    [SerializeField] MinMax _waitBetweenInitial, _waitBetweenMaxTolerance, _waitBetweenMaxNegativeTolerance;
    [SerializeField] MinMax _waitBetweenSips, _sipValues;
    [SerializeField] MinMax _bookReadTime;
    [SerializeField] int _mistakeCountToTolerate;
    [SerializeField] int _binNegativeToleranceThreshold;
    [SerializeField] int _binEndNegativeToleranceThreshold;
    [SerializeField] int _convNegativeToleranceThreshold;
    [SerializeField] int _toleranceHintToleranceThreshold;
    [SerializeField] int _fullyTolerantToleranceThreshold;
    [SerializeField] int _bookToleranceBoost, _tvValidChannel;
    [SerializeField] float _pushForce = -250f;
    [SerializeField] GameObject _bookOnHand, _magazineOnHand;

    bool d_HadConversation = false;
    bool negativeBiased;
    int currentMistakes = 0;
    bool inDialogue;

    teacup currentCup = null;
    IDadItem currentItem;

    float timer;
    System.Action onTimerFinished;

    [SerializeField] float DEBUG_NEGTOL, DEBUG_TOL;

    [SerializeField] DadState[] _states;

    Dictionary<DadStateType, DadState> statesDictionary = new Dictionary<DadStateType, DadState>();
    DadState currentState;

    void Awake()
    {
        instance = this;
        InitializeStates();
        ChangeState(DadStateType.WAIT);
    }

    void InitializeStates()
    {
        for(int i = 0; i < _states.Length; i++)
        {
            DadState ds = _states[i];
            if(statesDictionary.ContainsKey(ds.Type))
                continue;
            statesDictionary.Add(ds.Type, ds);
            ds.SetDad(this);
        }
    }

    public void ChangeState(DadStateType newState)
    {
        currentState?.OnStateFinished();
        currentState = statesDictionary[newState];
        currentState.OnStateStarted();
    }

    public void SetCurrentNeed(string need) => CurrentNeed = need;
    public void IncreaseTolerance(float newValue) => Tolerance = Mathf.Clamp01(Tolerance + newValue);
    public void IncreaseNegativeTolerance(float newValue) => NegativeTolerance = Mathf.Clamp01(NegativeTolerance + newValue);

    void Update()
    {
        DEBUG_NEGTOL = NegativeTolerance;
        DEBUG_TOL = Tolerance;
        currentState.OnStateUpdate();
    }
}
