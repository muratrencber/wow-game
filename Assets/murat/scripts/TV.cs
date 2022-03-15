using UnityEngine;

public class TV : MonoBehaviour
{
    static TV instance;

    public static int CurrentChannel {get {return instance.currentChannel;}}

    [SerializeField] int _channelCount;
    [SerializeField] Animator _tvAnimator;
    int currentChannel = 1;
    bool isOn = false;

    void Awake()
    {
        instance = this;
    }

    public static void Close()
    {
        instance.isOn = false;
        instance._tvAnimator.SetFloat("channelIndex", 0);
    }

    public void Toggle()
    {
        if(Dad.CurrentNeed == "tv")
            Dad.OnTVOpened();
        isOn = !isOn;
        _tvAnimator.SetFloat("channelIndex", !isOn ? 0 : (float)(currentChannel));
    }

    public void Zap(bool isRight)
    {
        if(!isOn)
            return;
        currentChannel += isRight ? 1 : -1;
        if(currentChannel > _channelCount)
            currentChannel = 1;
        else if(currentChannel < 1)
            currentChannel = _channelCount;
        _tvAnimator.SetFloat("channelIndex", (float)(currentChannel));
    }
}
