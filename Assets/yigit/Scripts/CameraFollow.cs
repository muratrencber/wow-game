using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour {

	[SerializeField] Transform _target;
	[SerializeField] MinMax _cameraSizes, _ratiosToMap;

	void LateUpdate () {
		float ratio = Mathf.Lerp(0, 1, (Slider.LeftRatio - _ratiosToMap.min) / (_ratiosToMap.max - _ratiosToMap.min));

		float orthographicSize = _cameraSizes.GetLerpValue(ratio);
		GetComponent<Camera>().orthographicSize = orthographicSize;
		Vector2 offset = new Vector2(1, 0) * orthographicSize * 2 * (1 - Slider.LeftRatio);

		Vector3 newPos = new Vector3(_target.position.x+offset.x, _target.position.y+offset.y, transform.position.z);
		transform.position = newPos;
        
	}
}