using UnityEngine;

public class SchoolPlatform : MonoBehaviour
{
    public bool StartFromLeft;
    public Transform LeftPoint, RightPoint;
    public Vector3 position {get {return transform.position;}}
    public GameObject bookObject, winObject;
}
