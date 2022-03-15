using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
    public float xOffset;
    public float yOffset;

	void LateUpdate () {
		//if (target.position.y > transform.position.y)
		{
			Vector3 newPos = new Vector3(target.position.x+xOffset, target.position.y+yOffset, transform.position.z);
			transform.position = newPos;
		}
        
	}
}