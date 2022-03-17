using UnityEngine;
using UnityEngine.UI;
public class teacup : MonoBehaviour, IDraggable, IDadItem
{
    public bool AvailableForDrag {get {return fillrate >= 1 && !beingConsumedByDad;}}
    public bool AvailableForConsumption {get {return !beingConsumedByDad && fillrate >= 1 && Dad.CurrentNeed == "tea" && Dad.WaitingForNeed; }}
    public string Key {get; private set;} = "tea";

    [SerializeField] Transform _initialPosition, _dadPosition;
    [SerializeField] float _returnSpeed;
    public float fillrate=0;
    public Image filler;
    public mainloop ml;
    // Start is called before the first frame update

    bool beingConsumedByDad = false;
    bool dragging = false;
    Vector2 targetPosition {get {return beingConsumedByDad ? _dadPosition.position : _initialPosition.position;}}

public void Start(){
    ml=FindObjectOfType<mainloop>();
}

    public void fillrate_rise(){
        fillrate+=Time.deltaTime*0.25f;
        fillrate = Mathf.Clamp01(fillrate);

        filler.fillAmount=fillrate;

        if(fillrate>=1){
            taskacomplished();
        }
    }

    public void fillrate_fall(float delta)
    {
        if(delta < 0 || !beingConsumedByDad)
            return;
        fillrate-=delta;
        fillrate = Mathf.Clamp01(fillrate);

        filler.fillAmount=fillrate;

        if(fillrate<= 0)
        {
            beingConsumedByDad = false;
            //Dad.OnFinishedTea();
        }
        
    }

    void Update()
    {
        if(!dragging)
            transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * _returnSpeed);
    }


    public void taskacomplished(){

    }

    public void OnDragStart()
    {
        dragging = true;
    }

    public void OnDrag(Vector2 delta)
    {
        dragging = true;
        Vector2 position = DrawScreens.RightCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }

    public void OnDragFinish()
    {
        dragging = false;
    }

    public void OnConsumption()
    {
        beingConsumedByDad = true;
    }

    public void OnConsumptionFinish (){}
}
