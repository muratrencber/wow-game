using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FinishLoader : MonoBehaviour
{

    public bool isTriggered = false;


    void Update()
    {
        if (isTriggered == true)
        {
            LoadNextLevel();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {

        isTriggered = true;
        
        

    }
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}