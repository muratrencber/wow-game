using UnityEngine;
using TMPro;

public class EvaOption : MonoBehaviour, IDraggable
{
    public bool AvailableForDrag {get{return EvaOptions.WillChoose;}}
    [SerializeField] TextMeshProUGUI _textUI;
    int index;
    EvaOptions parent;
    public void Set(EvaOptions parent, string text, int index)
    {
        this.parent = parent;
        this.index = index;
        _textUI.text = text;
    }

    public void OnDragStart()
    {
        parent.Register(index);
    }

    public void OnDrag(Vector2 delta) {}
    public void OnDragFinish() {}
}
