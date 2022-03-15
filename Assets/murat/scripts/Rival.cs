using UnityEngine;

public class Rival : MonoBehaviour
{
    [SerializeField] float _moveSpeed, _jumpSpeed, _waitBeforeJump, _minWaitBeforeJump;
    RivalPath.PathPoint currentPoint = null;
    float currentX = 0;
    bool waiting = false;
    void Start()
    {
        ToNextPoint();
    }
    void Update()
    {
        if(currentPoint == null || waiting)
            return;
        currentX += currentPoint.isBezier ? _jumpSpeed * Time.deltaTime : _moveSpeed * Time.deltaTime;
        float totalX = Mathf.Abs(currentPoint.position.x - currentPoint.endPosition.x);
        float ratio = currentX / totalX;
        Vector3 position = Vector2.Lerp(currentPoint.position, currentPoint.endPosition, ratio);
        if(currentPoint.isBezier)
        {
            Vector2 p1 = Vector2.Lerp(currentPoint.position, currentPoint.bezierPosition, ratio);
            Vector2 p2 = Vector2.Lerp(currentPoint.bezierPosition, currentPoint.endPosition, ratio);
            position = Vector2.Lerp(p1, p2, ratio);
        }
        position.z = transform.position.z;
        transform.position = position;

        if(ratio >= 1)
        {
            if(currentPoint.willJumpNext)
            {
                waiting = true;
                Invoke("ToNextPoint", Random.Range(_minWaitBeforeJump, _waitBeforeJump));
            }
            else
                ToNextPoint();
        }
    }
    void ToNextPoint()
    {
        waiting = false;
        currentX = 0;
        currentPoint = RivalPath.GetPoint(currentPoint != null ? currentPoint.index + 1 : 0);
    }
}
