using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundchecker : MonoBehaviour
{


    public bool grounded=false;
   

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.tag=="Ground") grounded=true;
    }

     void OnTriggerExit2D(Collider2D col)
    {

    }
}
