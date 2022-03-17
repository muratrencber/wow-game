using UnityEngine;

public class DragController : MonoBehaviour
{
    [SerializeField] LayerMask _validLayers;
    IDraggable draggable;
    Camera cam {get {return DrawScreens.RightCamera;}}
    Vector2 lastMousePos;
    void Update()
    {
        Vector2 currentMousePos = Input.mousePosition;
        if(currentMousePos.x < Screen.width * Slider.LeftRatio + 10)
        {
            if(draggable != null)
                draggable.OnDragFinish();
            draggable = null;
            return;
        }
        if(draggable != null && !draggable.AvailableForDrag)
        {
            draggable.OnDragFinish();
            draggable = null;
        }
        if(Input.GetButtonDown("Fire1"))
        {
            IDraggable lastDraggable = draggable;
            draggable = null;
            Ray r = new Ray(cam.ScreenToWorldPoint(Input.mousePosition), cam.transform.forward);
            if(Physics.Raycast(r,  out RaycastHit inf, Mathf.Infinity, _validLayers, QueryTriggerInteraction.Ignore))
            {
                draggable = inf.collider.GetComponent<IDraggable>();
                if(draggable != null && draggable != lastDraggable)
                    draggable.OnDragStart();
                lastMousePos = Input.mousePosition;
            }
        }
        else if(Input.GetButton("Fire1") && draggable != null)
        {
            Vector2 delta = (Vector2)Input.mousePosition - lastMousePos;
            draggable.OnDrag(delta);
            lastMousePos = Input.mousePosition;
        }
        else if(Input.GetButtonUp("Fire1"))
        {
            if(draggable != null)
                draggable.OnDragFinish();
            draggable = null;
        }
    }
}
