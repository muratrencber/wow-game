using UnityEngine;

public class Rival : MonoBehaviour
{
    public static int ReachedCount = 0;
    [SerializeField] GameObject[] _sprites;
    [SerializeField] Color _colorOne, _colorTwo;
    [SerializeField] float _moveSpeed, _jumpSpeed, _waitBeforeJump, _minWaitBeforeJump;
    [SerializeField] Vector2 _pointOffset, _jumpBezierOffset;
    [SerializeField] MinMax _jumpXoffset, _startDelay;

    Animator animator;
    SpriteRenderer srenderer;
    RivalPath.PathPoint currentPoint = null;
    Vector2 startPosition, endPosition, bezierPosition;
    float currentX = 0;
    bool waiting = false;
    void Start()
    {
        ReachedCount = 0;
        GameObject selected = _sprites[Random.Range(0, _sprites.Length)];
        selected.SetActive(true);

        animator = selected.GetComponent<Animator>();
        srenderer = selected.GetComponent<SpriteRenderer>();

        Material selectedMat = new Material(Shader.Find("Sprites/Rival"));
        Color c = Color.Lerp(_colorOne, _colorTwo, Random.Range(0.0f, 1.0f));
        selectedMat.color = c;
        srenderer.material = selectedMat;

        Invoke("InvokeStart", _startDelay.GetRandom());
    }
    void Update()
    {
        animator.SetBool("running", currentPoint != null && !waiting);
        if(currentPoint == null || waiting)
            return;
        if(Mathf.Sign(transform.localScale.x) != Mathf.Sign(currentPoint.endPosition.x - currentPoint.position.x))
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        currentX += currentPoint.isBezier ? _jumpSpeed * Time.deltaTime : _moveSpeed * Time.deltaTime;
        float totalX = Vector2.Distance(startPosition, endPosition);
        if(currentPoint.isBezier)
            totalX = Vector2.Distance(startPosition, bezierPosition) + Vector2.Distance(bezierPosition, endPosition);
        float ratio = currentX / totalX;
        Vector3 position = Vector2.Lerp(startPosition, endPosition, ratio);
        if(currentPoint.isBezier)
        {
            Vector2 p1 = Vector2.Lerp(startPosition, bezierPosition, ratio);
            Vector2 p2 = Vector2.Lerp(bezierPosition, endPosition, ratio);
            position = Vector2.Lerp(p1, p2, ratio);
        }
        position.z = transform.position.z;
        transform.position = position;

        if(ratio >= 1)
        {
            if(currentPoint.willJumpNext)
            {
                waiting = true;
                Invoke("InvokeNext", Random.Range(_minWaitBeforeJump, _waitBeforeJump));
            }
            else
                ToNextPoint();
        }
    }

    void InvokeStart()
    {
        ToNextPoint(true);
    }

    void InvokeNext()
    {
        ToNextPoint();
    }

    void ToNextPoint(bool isStart = false)
    {
        waiting = false;
        currentX = 0;
        currentPoint = RivalPath.GetPoint(currentPoint != null ? currentPoint.index + 1 : 0);
        if(currentPoint == null && !isStart)
        {
            ReachedCount++;
            return;
        }
        startPosition = isStart ? currentPoint.position + _pointOffset : (Vector2)transform.position;
        endPosition = currentPoint.endPosition + _pointOffset;
        bezierPosition = currentPoint.bezierPosition + _pointOffset + _jumpBezierOffset;
        if(currentPoint.isBezier)
        {
            endPosition += Vector2.right * _jumpXoffset.GetRandom();
            animator.SetTrigger("jump");
        }
    }
}
