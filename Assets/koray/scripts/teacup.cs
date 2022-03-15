using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class teacup : MonoBehaviour
{
    public float fillrate=0;
    public Image filler;
    public mainloop ml;
    // Start is called before the first frame update

public void Start(){
    ml=FindObjectOfType<mainloop>();
}

    public void fillrate_rise(){
        fillrate+=Time.deltaTime*0.25f;

        filler.fillAmount=fillrate;

        if(fillrate>=1){
            taskacomplished();
        }
    }


    public void taskacomplished(){

    }
}
