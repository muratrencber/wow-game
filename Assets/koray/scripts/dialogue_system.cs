using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class dialogue_system : MonoBehaviour
{
    public TextMeshProUGUI text_display;
    public string[] dialouge1,dialouge2,dialouge3,dialouge4;



    public void Start(){
        StartCoroutine( type_dialouge(dialouge1));
    }
    IEnumerator type_dialouge(string[] textnode){

        foreach(string s in textnode){
             Coroutine co;
             co= StartCoroutine(type(s));
            yield return new WaitForSeconds(0.1f);
            while (true)
            {
                if(Input.GetMouseButtonDown(0)){
                    text_display.text="";
                    StopCoroutine(co);
                    break;
                }
                yield return null;
            }
        }
    }

    IEnumerator type(string text){
        foreach(char c in text){
            //ilk karakteri ifleyerek narratoru yaz 
            text_display.text+=c;

        yield return new WaitForSeconds(0.1f);

        }
    }
}
