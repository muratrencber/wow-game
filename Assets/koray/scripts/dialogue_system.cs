using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class dialogue_system : MonoBehaviour
{
    public string[] tea1,tea2,tea3,tea4,papers1,papers2;

    public GameObject dialogbox;
    public Canvas maincanvas;


    public void startdialogue(Vector3 pos,string[] textnode){
      GameObject dialogbox_obj= Instantiate(dialogbox,Vector3.zero,Quaternion.identity,maincanvas.transform);
      dialogbox_obj.transform.localPosition=pos;
     StartCoroutine( type_dialouge( textnode,dialogbox_obj.GetComponentInChildren<TextMeshProUGUI>()));
    }

    public void Start(){

        startdialogue(Vector3.zero,tea1);

    }
   public IEnumerator type_dialouge(string[] textnode,TextMeshProUGUI dialogue_text){

        foreach(string s in textnode){
             Coroutine co;
             co= StartCoroutine(type(s,dialogue_text));
            yield return new WaitForSeconds(0.1f);
            while (true)
            {
                if(Input.GetMouseButtonDown(0)){
                    dialogue_text.text="";
                    StopCoroutine(co);
                    break;
                }
                yield return null;
            }
        
        }
       Destroy(dialogue_text.transform.parent.gameObject);
    }

    IEnumerator type(string text,TextMeshProUGUI dialogue_text){
        if(text.Substring(0,1)=="Q"){
                dialogue_text.text+="Father: ";
                text=text.Remove(0,1);
            }
            else if(text.Substring(0,1)=="A"){
                dialogue_text.text+="Ava: ";
                text=text.Remove(0,1);

            }

        foreach(char c in text){

            dialogue_text.text+=c;

        yield return new WaitForSeconds(0.1f);

        }
    }
}
