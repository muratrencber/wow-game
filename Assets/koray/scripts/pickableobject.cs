using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickableobject : MonoBehaviour
{
     void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag=="Player"){
        Pickup();

        }
    }


    public virtual void Pickup(){
        Destroy(this.gameObject);
    }
}
