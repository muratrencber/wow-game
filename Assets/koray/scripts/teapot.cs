using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teapot : MonoBehaviour, IDraggable
{
    public bool AvailableForDrag {get {return cup.fillrate < 1;}}
    int layerMask = 1 << 6;
    public ParticleSystem tea_spill;
    public Animator anim;
    public float turnprogress = 0;
    public bool leaning;
    public bool spilling;
    Camera maincam {get {return DrawScreens.RightCamera;}}

    [SerializeField] Transform _initalPosition;
    [SerializeField] float _returnSpeed;
    [SerializeField] teacup cup;

    bool dragging;

    public GameObject teacup;
    public void Start()
    {
        teacup = FindObjectOfType<teacup>().gameObject;

    }

    void Update()
    {
        if(!dragging)
        {
            transform.position = Vector2.Lerp(transform.position, _initalPosition.position, Time.deltaTime * _returnSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * _returnSpeed);
        }
    }


    public void OnDragStart()
    {
        dragging = true;
        GetComponent<BoxCollider>().enabled = false;
    }

    public void OnDrag(Vector2 drag)
    {
        dragging = true;
        Vector2 mousepos = maincam.ScreenToWorldPoint(Input.mousePosition);
         checkforcup();
        transform.position = mousepos;
    }

    public void OnDragFinish()
    {
        dragging = false;
        GetComponent<BoxCollider>().enabled = true;
        leaning = false;
        spilling = false;
        anim.SetBool("leaning", false);
        turnprogress = 0;
        tea_spill.Stop();
    }


    public void checkforcup()
    {
        if (transform.position.y - teacup.transform.position.y > 0 && Mathf.Abs(transform.position.x - teacup.transform.position.x) < 1.5f)
        {
            if(!leaning)

            leaning = true;
            anim.SetBool("leaning", true);
            turnprogress+=Time.deltaTime;

            if(turnprogress>=1&&!spilling){
                tea_spill.Play();
                spilling=true;
            }
            if(spilling){
                teacup.GetComponent<teacup>().fillrate_rise();
            }



        }
        else
        {
        leaning = false;
        spilling = false;
        anim.SetBool("leaning", false);
        turnprogress = 0;
        tea_spill.Stop();
        }


    }
}
