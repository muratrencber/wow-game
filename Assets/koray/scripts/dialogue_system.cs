using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class dialogue_system : MonoBehaviour
{
    public TextMeshProUGUI text_display;
    public string[] tea1,tea2,tea3,tea4,papers1,papers2;



    public void Start(){
    }
   public IEnumerator type_dialouge(string[] textnode){

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
