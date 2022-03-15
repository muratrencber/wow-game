using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MinMax
{
    public float min;
    public float max;
    public MinMax(float min, float max)
    {
        this.min = min;
        this.max = max;
    }

    public float GetRandom()
    {
        return Random.Range(min, max);
    }
}

public class MinMaxInt
{
    
    public int min;
    public int max;
    public MinMaxInt(int min, int max)
    {
        this.min = min;
        this.max = max;
    }

    public float GetRandom()
    {
        return Random.Range(min, max + 1);
    }
}

public class Dad : MonoBehaviour
{
    static Dad instance;

    public static string CurrentNeed {get; private set;}
    public static int Tolerance {get; private set;} = 0;
    public static int NegativeTolerance {get; private set;} = 0;
    public static void OnReceivedItem(IDadItem item) => instance?.OnReceivedItemP(item);
    public static void OnFinishedTea() => instance?.OnFinishedTeaP();
    public static void OnTVOpened() => instance?.OnTVOpenedP();

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

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        timer = GetWaitTime();
        onTimerFinished = SetNeed;
    }

    void Update()
    {
        DEBUG_NEGTOL = NegativeTolerance;
        DEBUG_TOL = Tolerance;
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                onTimerFinished?.Invoke();
            }
        }
    }

    void OnReceivedItemP(IDadItem item)
    {
        currentItem = item;
        CurrentNeed = "";
        if(item.Key == "book" || item.Key == "magazine")
        {
            BookOnDesk bod = item as BookOnDesk;
            bod.gameObject.SetActive(false);
            timer = _bookReadTime.GetRandom();
            if(item.Key == "book")
            {
                BookManager.RemovedLastBook();
                _bookOnHand.SetActive(true);
                onTimerFinished = OnReadBook;
            }
            else
            {
                _magazineOnHand.SetActive(true);
                onTimerFinished = OnReadMagazine;
            }
        }
        else if(item.Key == "tea")
        {
            currentCup = item as teacup;
            timer = _waitBetweenSips.GetRandom();
            onTimerFinished = Sip;
        }
        if(Slider.Locked)
        {
            Slider.Unlock();
            Slider.AddForce(100);
        }
    }

    void Sip()
    {
        if(currentCup)
        {
            timer = _waitBetweenSips.GetRandom();
            onTimerFinished = Sip;
            currentCup.fillrate_fall(_sipValues.GetRandom());
        }
        else
        {
            timer = GetWaitTime();
            onTimerFinished = SetNeed;
        }
    }

    void OnReadMagazine()
    {
        currentItem.OnConsumptionFinish();
        _magazineOnHand.SetActive(false);
        timer = GetWaitTime();
        onTimerFinished = SetNeed;
    }

    void OnReadBook()
    {
        currentItem.OnConsumptionFinish();
        Tolerance += _bookToleranceBoost;
        _bookOnHand.SetActive(false);
        //show dialogue
        timer = GetWaitTime();
        onTimerFinished = SetNeed;
    }

    void OnFinishedTeaP()
    {
        currentCup = null;
        timer = GetWaitTime();
        onTimerFinished = SetNeed;
    }

    void OnTVOpenedP()
    {
        if(CurrentNeed != "tv")
            return;
        CurrentNeed = "";
        timer = _bookReadTime.GetRandom();
        onTimerFinished = WatchedTV;
        if(Slider.Locked)
        {
            Slider.Unlock();
            Slider.AddForce(100);
        }
    }

    void WatchedTV()
    {
        TV.Close();
        if(TV.CurrentChannel == _tvValidChannel)
        {
            Tolerance += _bookToleranceBoost;
            //show dialogue
        }
        timer = GetWaitTime();
        onTimerFinished = SetNeed;
    }

    float GetWaitTime()
    {
        float wbi = _waitBetweenInitial.GetRandom();
        float wbmt = _waitBetweenMaxTolerance.GetRandom();
        float wbmnt = _waitBetweenMaxNegativeTolerance.GetRandom();
        float tolRatio = Tolerance/(float)_fullyTolerantToleranceThreshold;
        float negTolRatio = NegativeTolerance/(float)_convNegativeToleranceThreshold;

        float w1 = Mathf.Lerp(wbi,wbmt, tolRatio);
        float w2 = Mathf.Lerp(wbi, wbmnt, negTolRatio);

        negativeBiased = w2 > w1;
        return w2 > w1 ? w2 : w1;
    }

    void SetNeed()
    {
        if(!d_HadConversation && NegativeTolerance >= _convNegativeToleranceThreshold)
        {
            //SaySomethings
        }
        if(Tolerance >= _fullyTolerantToleranceThreshold || NegativeTolerance >= _convNegativeToleranceThreshold)
            return;
        string[] needs = {"tea", "book", "tv"};
        CurrentNeed = needs[Random.Range(0, needs.Length)];
        print("Dad: I need some "+CurrentNeed+".");
        timer = _waitTimeAfterWanting.GetRandom();
        onTimerFinished = DidNotGetWanted;
    }

    void DidNotGetWanted()
    {
        Slider.AddForce(_pushForce, true);
        currentMistakes++;
    }

    void SaySomethings(string[] things)
    {

    }
}
