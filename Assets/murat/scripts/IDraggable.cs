using UnityEngine;

public interface IDraggable
{
    public bool AvailableForDrag {get;}
    public GameObject gameObject {get;}
    void OnDrag(Vector2 delta);
    void OnDragStart();
    void OnDragFinish();
}
