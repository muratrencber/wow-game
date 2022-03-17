using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DadNotification : MonoBehaviour
{
    public static bool IsBusy {get {return (instance.transform.localScale.x > 0.01 && instance.transform.localScale.x < 0.99f) || showing;}}
    public static bool showing = false;
    static DadNotification instance;
    const float SHOW_SPEED = 10;
    [SerializeField] float _showTime;
    [SerializeField] TextMeshProUGUI _text;
    float timer;
    int currentLineIndex = 0;
    DadLine[] lines;

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
                ShowNextLine();
            }
        }
        Vector3 target = showing ? Vector3.one : Vector3.zero;
        transform.localScale = Vector3.Lerp(transform.localScale, target, Time.deltaTime *  SHOW_SPEED);
    }

    public static void Show(string text, float duration)
    {
        DadLine[] lines = {new DadLine(text, duration)};
        instance?.ShowP(lines);
    }

    public static void Show(DadLine line)
    {
        if(line == null)
            return;
        DadLine[] lines = {line};
        instance?.ShowP(lines);
    }

    public static void Show(DadLine[] lines)
    {
        instance?.ShowP(lines);
    }

    public static void Hide()
    {
        instance.timer = 0;
        showing = false;
    }

    void ShowP(DadLine[] lines)
    {
        this.lines = lines;
        currentLineIndex = -1;
        showing = true;
        ShowNextLine();
    }

    void ShowNextLine()
    {
        currentLineIndex++;
        if(lines.Length - 1 < currentLineIndex)
        {
            timer = 0;
            lines = null;
            showing = false;
        }
        else
        {
            transform.localScale = Vector3.zero;
            showing = true;
            _text.text = lines[currentLineIndex].line;
            timer = lines[currentLineIndex].duration;
        }
    }

}
