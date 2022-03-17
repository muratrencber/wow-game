using UnityEngine;
using UnityEngine.SceneManagement;


public class FinishLoader : MonoBehaviour
{
    public static bool Failed;
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
        if(Failed)
            return;
        if(Rival.ReachedCount <= 0)
            SceneManager.LoadScene("GoodEnding");
        else
        {
            Dad.OnFailedClass();
            Failed = true;
        }
    }
}
