using UnityEngine;

public class RivalPathPoint : MonoBehaviour
{
    [SerializeField] bool _isJumpPoint;
    [SerializeField] Vector2 offset;

    public bool IsJump {get {return _isJumpPoint;}}
    public Vector2 Offset {get {return offset;}}

    public Vector2 GetPeak(Transform target)
    {
        Vector2 peak = target.position + transform.position;
        peak *= .5f;
        peak.y = Mathf.Max(transform.position.y, target.position.y);
        peak += Offset;
        return peak;
    }
}
