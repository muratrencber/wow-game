using System.Collections.Generic;
using UnityEngine;

public class RivalPath : MonoBehaviour
{
    public class PathPoint
    {
        public Vector2 position;
        public Vector2 bezierPosition;
        public Vector2 endPosition;
        public bool isBezier;
        public bool willJumpNext;
        public int index;

        public PathPoint(Vector2 position, Vector2 endPosition, int index)
        {
            this.position = position;
            this.bezierPosition = endPosition;
            this.endPosition = endPosition;
            this.index = index;
            isBezier = false;
            willJumpNext = false;
        }

        public PathPoint(Vector2 position, Vector2 bezierPosition, Vector2 endPosition, int index)
        {
            this.position = position;
            this.bezierPosition = bezierPosition;
            this.endPosition = endPosition;
            this.index = index;
            isBezier = true;
            willJumpNext = false;
        }
    }

    [SerializeField] Transform _pointContainer;
    List<PathPoint> points = new List<PathPoint>();

    static RivalPath instance;
    void Awake()
    {
        ConstructPath();
        instance = this;
    }

    public static PathPoint GetPoint(int i)
    {
        return instance.GetPointPrivate(i);
    }

    PathPoint GetPointPrivate(int i)
    {
        if(points == null || i < 0 || i >= points.Count)
            return null;
        return points[i];
    }

    void ConstructPath()
    {
        points.Clear();
        if(!_pointContainer)
            return;
        for(int i = 0; i < _pointContainer.childCount - 1; i++)
        {
            Transform point = _pointContainer.GetChild(i);
            Transform nextPoint = _pointContainer.GetChild(i+1);
            RivalPathPoint pprops = point.GetComponent<RivalPathPoint>();
            bool isJump = !pprops ? false : pprops.IsJump;
            
            if(isJump)
            {
                if(points.Count > 0)
                    points[points.Count - 1].willJumpNext = true;
                points.Add(new PathPoint(point.position, pprops.GetPeak(nextPoint), nextPoint.position, points.Count));
            }
            else
                points.Add(new PathPoint(point.position, nextPoint.position, points.Count));
        }
    }

    async void OnDrawGizmos()
    {
        if(!_pointContainer)
            return;
        Gizmos.color = Color.cyan;
        for(int i = 0; i < _pointContainer.childCount - 1; i++)
        {
            Transform point = _pointContainer.GetChild(i);
            Transform nextPoint = _pointContainer.GetChild(i+1);
            RivalPathPoint pprops = point.GetComponent<RivalPathPoint>();
            bool isJump = !pprops ? false : pprops.IsJump;
            Vector2[] points = new Vector2[isJump ? 3 : 2];
            points[0] = point.transform.position;
            points[points.Length - 1] = nextPoint.transform.position;
            if(isJump)
                points[1] = pprops.GetPeak(nextPoint);                
            Gizmos.DrawWireSphere(point.transform.position, 0.3f);
            if(i == _pointContainer.childCount - 2)
                Gizmos.DrawWireSphere(nextPoint.transform.position, 0.3f);
            for(int j = 0; j < points.Length - 1; j++)
                Gizmos.DrawLine(points[j], points[j + 1]);
        }
    }
}
