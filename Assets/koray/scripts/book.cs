using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class book : pickableobject
{
   public ParticleSystem starexp;

    public override void Pickup()
    {
        FindObjectOfType<inventory>().bookcount++;  
        Instantiate(starexp,transform.position,Quaternion.identity);
        base.Pickup();

    }

   
}
