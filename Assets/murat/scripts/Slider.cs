using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slider : MonoBehaviour, IDragHandler
{
    public static float LeftRatio {get; private set;} = .5f;
    public static bool Locked {get; private set;} = false;
    static Slider instance;

    [SerializeField] RectTransform _sliderUI;
    [SerializeField] float _forceDamp, _forceScaling, _reachingSpeed;
    float currentForce;
    Vector2 lastScreenSize;
    bool lockAfterForce;
    bool reachingTargetRatio;
    float targetRatio;

    void Awake()
    {
        instance = this;
        RefreshSliderPosition();
    }

    void Update()
    {
        Vector2 currentScreenSize = new Vector2(Screen.width, Screen.height);
        if(lastScreenSize != currentScreenSize)
            RefreshSliderPosition();
        if(currentForce != 0)
        {
            if(Mathf.Abs(currentForce) < _forceDamp * Time.deltaTime)
                currentForce = 0;
            else
            {
                currentForce -= Mathf.Sign(currentForce) * Time.deltaTime * _forceDamp;
                SetUIFromRatio(LeftRatio + currentForce * Time.deltaTime * _forceScaling);
                if(lockAfterForce && (LeftRatio == 0 || LeftRatio == 1))
                    Lock();
            }
        }
        else if(Locked == false)
            lockAfterForce = false;
        if(reachingTargetRatio)
        {
            float currentRatio = LeftRatio;
            float newRatio = Mathf.Lerp(currentRatio, targetRatio, Time.deltaTime * _reachingSpeed);
            if(Mathf.Sign(LeftRatio - newRatio) != Mathf.Sign(LeftRatio - currentRatio) || LeftRatio == targetRatio)
            {
                newRatio = targetRatio;
                reachingTargetRatio = false;
            }
            SetUIFromRatio(newRatio);
        }

    }

    public static void Lock()
    {
        Locked = true;
    }

    public static void Unlock()
    {
        instance.lockAfterForce = false;
        Locked = false;
    }

    static void SetRatio(float newRatio)
    {
        if(!Locked)
            LeftRatio = Mathf.Clamp01(newRatio);
        if(newRatio > 1 || newRatio < 0)
            instance.currentForce = 0;
    }

    public static void AddForce(float force, bool lockAfterForce = false)
    {
        instance.lockAfterForce = lockAfterForce;
        instance.currentForce += force;
    }

    public static void SlideUntilRatio(float target, bool rightOriented = true)
    {
        if(LeftRatio == target || LeftRatio < target == rightOriented)
            return;
        instance.targetRatio = target;
        instance.reachingTargetRatio = true;
    }

    static void RefreshSliderPosition()
    {
        float xPos = Screen.width * LeftRatio;
        Vector3 pos = instance._sliderUI.transform.position;
        pos.x = xPos;
        instance._sliderUI.transform.position = pos;
        CamSlider.SetCameras(LeftRatio);
        instance.lastScreenSize = new Vector2(Screen.width, Screen.height);
    }

    public static void SetUIFromRatio(float newRatio)
    {
        SetRatio(newRatio);
        RefreshSliderPosition();
    }

    public void OnDrag(PointerEventData ped)
    {
        if(Locked)
            return;
        SetRatio(ped.position.x / Screen.width);
        RefreshSliderPosition();
    }
}
