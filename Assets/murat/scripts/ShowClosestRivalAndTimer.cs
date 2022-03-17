using UnityEngine;
using TMPro;

public class ShowClosestRivalAndTimer : MonoBehaviour
{
    public static bool TimerFinished;
    [SerializeField] Transform _player;
    [SerializeField] float _time, _distanceMultiplier, _lerpSpeed;
    [SerializeField] Vector2 _offset;
    [SerializeField] int _totalDays;
    [SerializeField] GameObject _indicator, _daysIndicator;
    [SerializeField] TextMeshProUGUI _text, _distanceText;

    float timer;
    void Start()
    {
        TimerFinished = false;
        timer = _time;
        _indicator.SetActive(true);
        _text.text = "";
    }

    void Update()
    {
        
        float bottom = DrawScreens.LeftCamera.transform.position.y - DrawScreens.LeftCamera.orthographicSize + _offset.y;
        float top = DrawScreens.LeftCamera.transform.position.y + DrawScreens.LeftCamera.orthographicSize - _offset.y;
        float left = DrawScreens.LeftCamera.transform.position.x - (DrawScreens.LeftCamera.orthographicSize * DrawScreens.LeftCamera.aspect) + _offset.x;
        float right = left + DrawScreens.LeftCamera.orthographicSize * DrawScreens.LeftCamera.aspect * Slider.LeftRatio - 2*_offset.x;
        Vector3 daysPos = new Vector3(left, bottom, _daysIndicator.transform.position.z);
        _daysIndicator.transform.position = daysPos;
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            timer = Mathf.Max(0, timer);
            int days = Mathf.CeilToInt((timer / _time) * _totalDays);
            _text.text = days.ToString();
            if(timer <= 0)
                TimerFinished = true;
        }
        Rival r = Rival.HighestRival;
        if(r == null)
            _indicator.SetActive(false);
        else
        {
            if(r.transform.position.y > bottom && r.transform.position.y < top)
                _indicator.SetActive(false);
            else
            {
                float x = Mathf.Clamp(r.transform.position.x, left, right);
                float y = Mathf.Clamp(r.transform.position.y, bottom, top);
                Vector3 pos = new Vector3(x, y, _indicator.transform.position.z);
                _indicator.SetActive(true);
                _indicator.transform.position = Vector3.Lerp(_indicator.transform.position, pos, _lerpSpeed * Time.deltaTime);
                _distanceText.text = Mathf.RoundToInt(Vector2.Distance(_player.position, r.transform.position) * _distanceMultiplier).ToString();
            }
        }
    }
}
