using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teapot : MonoBehaviour
{

    int layerMask = 1 << 6;
    public ParticleSystem tea_spill;
    public Animator anim;
    public float turnprogress = 0;
    public bool leaning;
    public bool spilling;
    Camera maincam;

    public GameObject teacup;
    public void Start()
    {
        maincam = Camera.main;
        teacup = FindObjectOfType<teacup>().gameObject;

    }

    public void Update()
    {

    }


    void OnMouseDown()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    void OnMouseDrag()
    {
        Debug.Log("evv");
        Vector2 mousepos = maincam.ScreenToWorldPoint(Input.mousePosition);
         checkforcup();
        transform.position = mousepos;
    }

    void OnMouseUp()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        Debug.Log("tf");
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
            Debug.Log("tf");
        leaning = false;
        spilling = false;
        anim.SetBool("leaning", false);
        turnprogress = 0;
        tea_spill.Stop();
        }


    }
}
