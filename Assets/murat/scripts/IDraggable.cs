using UnityEngine;

public interface IDraggable
{
    public bool AvailableForDrag {get;}
    void OnDrag(Vector2 delta);
    void OnDragStart();
    void OnDragFinish();
}
