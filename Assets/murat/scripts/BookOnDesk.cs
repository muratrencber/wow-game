using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookOnDesk : MonoBehaviour, IDraggable, IDadItem
{
    const float RETURN_SPEED = 10f;
    [SerializeField] string _bookKey;
    public int index;
    public string Key {get {return _bookKey;}}
    public bool AvailableForConsumption {get {return Dad.CurrentNeed == "book" && Dad.WaitingForNeed;;}}
    public bool AvailableForDrag {get {return gameObject.activeSelf && (index == BookManager.CurrentBookCount - 1 || _bookKey != "book");}}
    bool dragging = false;
    public void OnDragStart() {dragging = true;}
    public void OnDragFinish() {dragging = false;}
    Vector3 initalPosition;

    void Start()
    {
        initalPosition = transform.position;
    }

    public void OnDrag(Vector2 delta)
    {
        Vector3 pos = DrawScreens.RightCamera.ScreenToWorldPoint(Input.mousePosition);
        pos.z = transform.position.z;
        transform.position = pos;
    }
    public void OnConsumption()
    {
        //Destroy(gameObject);
    }

    public void OnConsumptionFinish() {
        if(_bookKey != "book")
            gameObject.SetActive(true);
    }

    void Update()
    {
        if(!dragging)
        {
            Vector3 targetPosition = _bookKey == "book" ? BookManager.bookPositions[index] : initalPosition;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * RETURN_SPEED);
        }
    }
}
