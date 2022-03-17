using UnityEngine;

public class PlatformerCreator : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] GameObject _deskPlatform, _bookPlatform;
    [SerializeField] MinMax _yOffsets;
    [SerializeField] MinMax _xOffsets;
    [SerializeField] float _width, _yToAddCheckPoint, _yToCreatePlatform;
    [SerializeField] SchoolPlatform startPlatform;
    [SerializeField] Transform _platformContainer;
    [SerializeField] Transform _checkPointContainer;

    Transform lastCheckpoint;
    SchoolPlatform lastPlatform;
    bool finished;

    void Start()
    {
        finished = false;
        CreateCheckPoint(_player.position);
        CreatePlatformWaypoints(startPlatform);
        lastPlatform = startPlatform;
    }

    void Update()
    {
        if(finished)
            return;
        if(lastPlatform == null)
            CreateNewPlatform();
        while(lastPlatform.position.y - GetHighestCompetitor().position.y < _yToCreatePlatform)
        {
            CreateNewPlatform();
        }
    }

    Transform GetHighestCompetitor()
    {
        if(Rival.HighestRival)
            return Rival.HighestRival.transform.position.y > _player.transform.position.y ? Rival.HighestRival.transform : _player;
        return _player;
    }

    void CreateNewPlatform()
    {
        GameObject platformToSpawn = Random.Range(1,11) > 9 ? _bookPlatform : _deskPlatform;
        float maxX = Mathf.Min(_xOffsets.max, startPlatform.position.x + _width - lastPlatform.position.x);
        float minX = Mathf.Max(_xOffsets.min, startPlatform.position.x - _width - lastPlatform.position.x);
        Vector3 spawnPosition = lastPlatform.position + Vector3.right * Random.Range(minX, maxX) + Vector3.up * _yOffsets.GetRandom();
        GameObject platformInstance = Instantiate(platformToSpawn, spawnPosition, Quaternion.identity, _platformContainer);
        SchoolPlatform sp = platformInstance.GetComponent<SchoolPlatform>();
        sp.bookObject.SetActive(Random.Range(1, 101) > 95);
        CreatePlatformWaypoints(sp);
        lastPlatform = sp;
        if(sp.bookObject.transform.position.y - lastCheckpoint.position.y > _yToAddCheckPoint)
            CreateCheckPoint(sp.bookObject.transform.position);
        if(ShowClosestRivalAndTimer.TimerFinished && Rival.HighestRival.transform.position.y - _player.transform.position.y < 30)
        {
            sp.bookObject.SetActive(false);
            sp.winObject.SetActive(true);
            finished = true;
        }
    }

    void CreateCheckPoint(Vector3 position)
    {
        GameObject cp = new GameObject();
        cp.tag = "Checkpoint";
        cp.transform.position = position;
        cp.transform.parent = _checkPointContainer;
        lastCheckpoint = cp.transform;
    }

    void CreatePlatformWaypoints(SchoolPlatform p)
    {
        if(lastPlatform != null)
        {
            bool isOfLeft = p.position.x < lastPlatform.position.x;
            if(isOfLeft == lastPlatform.StartFromLeft)
                RivalPath.RemoveLastPathPoint();
            Vector3 start = isOfLeft ? lastPlatform.LeftPoint.position : lastPlatform.RightPoint.position;
            Vector3 end = isOfLeft ? p.RightPoint.position : p.LeftPoint.position;
            Vector3 mid = (start + end) * .5f;
            mid.y = Mathf.Max(start.y, end.y);
            RivalPath.PathPoint jp =  new RivalPath.PathPoint(start, mid, end, 0);
            RivalPath.GetLastPathPoint().willJumpNext = true;
            RivalPath.AddPathPoint(jp);
        }
        Vector3 pstart = p.StartFromLeft ? p.LeftPoint.position : p.RightPoint.position;
        Vector3 pend = p.StartFromLeft ? p.RightPoint.position : p.LeftPoint.position;
        RivalPath.PathPoint pp =  new RivalPath.PathPoint(pstart, pend, 0);
        RivalPath.AddPathPoint(pp);
    }

}
