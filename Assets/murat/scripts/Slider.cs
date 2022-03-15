using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slider : MonoBehaviour, IDragHandler
{
    public static float LeftRatio {get; private set;} = .5f;
    static Slider instance;

    [SerializeField] RectTransform _sliderUI;
    [SerializeField] float _forceDamp, _forceScaling, DEBUG_FORCE;
    float currentForce;

    void Awake()
    {
        instance = this;
        RefreshSliderPosition();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            AddForce(DEBUG_FORCE);
        }
        if(currentForce != 0)
        {
            if(Mathf.Abs(currentForce) < _forceDamp * Time.deltaTime)
                currentForce = 0;
            else
            {
                currentForce -= Mathf.Sign(currentForce) * Time.deltaTime * _forceDamp;
                SetUIFromRatio(LeftRatio + currentForce * Time.deltaTime * _forceScaling);
            }
        }
    }

    static void SetRatio(float newRatio)
    {
        LeftRatio = Mathf.Clamp01(newRatio);
        if(newRatio > 1 || newRatio < 0)
            instance.currentForce = 0;
    }

    static void AddForce(float force)
    {
        instance.currentForce += force;
    }

    static void RefreshSliderPosition()
    {
        float xPos = Screen.width * LeftRatio;
        Vector3 pos = instance._sliderUI.transform.position;
        pos.x = xPos;
        instance._sliderUI.transform.position = pos;
    }

    public static void SetUIFromRatio(float newRatio)
    {
        SetRatio(newRatio);
        RefreshSliderPosition();
    }

    public void OnDrag(PointerEventData ped)
    {
        SetRatio(ped.position.x / Screen.width);
        RefreshSliderPosition();
    }
}
