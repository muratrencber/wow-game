using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
  

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) || Input.anyKey )
        {
            Application.Quit();
            Debug.Log("Oyun bitti");
        }


    }
}