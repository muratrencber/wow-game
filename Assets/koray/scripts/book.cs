using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class book : pickableobject
{
   

    public override void Pickup()
    {
        base.Pickup();
        BookManager.AddedNewBook();
    }

   
}
