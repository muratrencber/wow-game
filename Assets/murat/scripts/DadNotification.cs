using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DadNotification : MonoBehaviour
{
    public static bool IsBusy {get {return instance.transform.localScale.x > 0.01 && instance.transform.localScale.x < 0.99f;}}
    public static bool showing = false;
    static DadNotification instance;
    const float SHOW_SPEED = 10;
    [SerializeField] float _showTime;
    [SerializeField] TextMeshProUGUI _text;
    float timer;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                showing = false;
            }
        }
        Vector3 target = showing ? Vector3.one : Vector3.zero;
        transform.localScale = Vector3.Lerp(transform.localScale, target, Time.deltaTime *  SHOW_SPEED);
    }

    public static void Show(string text)
    {
        instance?.ShowP(text);
    }

    public static void Hide()
    {
        instance.timer = 0;
        showing = false;
    }

    void ShowP(string text)
    {
        _text.text = text;
        timer = _showTime;
        showing = true;
    }

}
