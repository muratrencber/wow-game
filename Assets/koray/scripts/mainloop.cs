using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainloop : MonoBehaviour
{
    public GameObject teacup,teapot;
    public GameObject tempcup,temppot;
    public dialogue_system ds;
    
    public GameObject paperchoice;
    public GameObject temppaperchoice;

    public bool CurrentlyOnEvent=false;
    public float eventcountdown=0;

    public bool satisfied=false;

    public void Start(){

    }

    public void FixedUpdate(){
        if(!CurrentlyOnEvent) eventcountdown+=Random.Range(0,10f)*Time.deltaTime;
        
        if(eventcountdown>=100){
            eventcountdown=0;
            CurrentlyOnEvent=true;
            int rnd=Random.Range(0,2);
            switch(rnd){
                case 0:    StartCoroutine(askforpapers_confirmation());
                    break;

                     case 1:     askfortea_confirmation();
                    break;
            }
        }
    }


    public IEnumerator askforpapers_confirmation(){
        //notification cıkıcak işte

        yield return new WaitForSeconds(10);
        if(!satisfied){
        Instantiate(paperchoice);

        }
        //paper -- kitap şeyini çıkart
    }

    public void papers_option1(){
        //?
        StartCoroutine(askforpapers());
    }
     public void papers_option2(){
         //?
        StartCoroutine(askforpapers());
    }

    public IEnumerator askforpapers(){


        Destroy(temppaperchoice);
        yield return null;
        //diyalog felan
        //efeqt
        CurrentlyOnEvent=false;

    }

    public void askfortea_confirmation(){
        //ver verme butonu 
        //ver derse askfor tea çalıştır

    }


    public void tea_option1(){

    }

    public void tea_option2(){
        
    }
    public IEnumerator askfortea(){
        
        yield return null;
        //
        //spawn action
        //tea ve cup 'ı belirli yerlerde spawnla
        //belirli bir süre geçerse kızma sinyali gödner
        //en sonda yok edicek bunları geri
        CurrentlyOnEvent=false;
    }


    public void askfortea_finished(){
        Destroy(tempcup);
        Destroy(temppot);
    }
}
