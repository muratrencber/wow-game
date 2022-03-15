using UnityEngine;

public class RemoteButton : MonoBehaviour, IDraggable
{
    public enum BUTTON_TYPE
    {
        POWER,
        ZAP_RIGHT,
        ZAP_LEFT
    }
    public bool AvailableForDrag {get {return true;}}
    [SerializeField] BUTTON_TYPE _type;
    [SerializeField] TV _targetTV;

    public void OnDragStart()
    {
        switch(_type)
        {
            case BUTTON_TYPE.POWER:
                _targetTV.Toggle();
                break;
            case BUTTON_TYPE.ZAP_LEFT:
                _targetTV.Zap(false);
                break;
            case BUTTON_TYPE.ZAP_RIGHT:
                _targetTV.Zap(true);
                break;
        }
    }

    public void OnDrag(Vector2 delta) {}
    public void OnDragFinish() {}
}
