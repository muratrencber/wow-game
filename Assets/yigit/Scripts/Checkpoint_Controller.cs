using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Checkpoint_Controller : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
            List<GameObject> checkpointsList = checkpoints.ToList();
            checkpointsList.Sort((a,b) => b.transform.position.y.CompareTo(a.transform.position.y));
            foreach(GameObject checkPoint in checkpointsList)
            {
                if(checkPoint.transform.position.y < Player.highestY)
                {
                    other.gameObject.transform.position = checkPoint.transform.position;
                    return;
                }
            }
        }
    }
}
