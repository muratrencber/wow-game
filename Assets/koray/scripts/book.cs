using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class book : pickableobject
{
   public ParticleSystem starexp;
    public override void Pickup()
    {
        if(BookManager.CurrentBookCount < 5)
        {
            base.Pickup();
            BookManager.AddedNewBook();
        }
    }

   
}
