using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainloop : MonoBehaviour
{
    public GameObject teacup,teapot;
    public GameObject tempcup,temppot;
    public dialogue_system ds;
    

    public void Start(){

    }

    public void askfortea_confirmation(){
        //ver verme butonu 
        //ver derse askfor tea çalıştır
    }
    public IEnumerator askfortea(){
        
        yield return null;
        //
        //spawn action
        //tea ve cup 'ı belirli yerlerde spawnla
        //belirli bir süre geçerse kızma sinyali gödner
        //en sonda yok edicek bunları geri
    }


    public void askfortea_finished(){
        Destroy(tempcup);
        Destroy(temppot);
    }
}
