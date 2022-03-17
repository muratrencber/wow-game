using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaOptions : MonoBehaviour
{
    public static bool WillChoose;
    static EvaOptions instance;
    static System.Action[] Methods;
    
    [SerializeField] Transform _shownPosition, _hiddenPosition;
    [SerializeField] EvaOption _singleOption, _optionOne, _optionTwo;
    [SerializeField] GameObject _twoOptionsContainer, _oneOptionContainer;
    [SerializeField] float _moveSpeed;
    float timer;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if(WillChoose)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
                Hide();
        }
        Vector3 pos = WillChoose ? _shownPosition.position : _hiddenPosition.position;
        pos.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * _moveSpeed);
    }

    public static void Set(string[] options, System.Action[] methods, float duration)
    {
        WillChoose = true;
        for(int i = 0; i < options.Length; i++)
        {
            if(i > 1)
                continue;
            if(i == 0)
            {
                instance._singleOption.Set(instance, options[i], i);
                instance._optionOne.Set(instance, options[i], i);
            }
            else
                instance._optionTwo.Set(instance, options[i], i);
        }
        instance.timer = duration;
        Methods = methods;
        instance._oneOptionContainer.SetActive(options.Length < 2);
        instance._twoOptionsContainer.SetActive(options.Length >= 2);
    }

    public static void Hide()
    {
        WillChoose = false;
    }

    public void Register(int index)
    {
        Methods[index].Invoke();
        _oneOptionContainer.SetActive(false);
        _twoOptionsContainer.SetActive(false);
        Hide();
    }
}
