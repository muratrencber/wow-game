using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawScreens : MonoBehaviour
{
    [SerializeField] GameObject _leftPlane, _rightPlane;
    [SerializeField] Material _leftMaterial, _rightMaterial;
    
    [SerializeField] float leftRatio = .5f;
    Vector2 lastSize;
    Camera mainCamera {get {return Camera.main;}}

    void Start()
    {
        Draw();
    }

    void Update()
    {
        Draw();
    }

    Vector2 Get2DCameraSize(Camera cm)
    {
        float height = cm.orthographicSize * 2;
        float width = height * cm.aspect;
        return new Vector2(width, height);
    }

    void Draw()
    {
        lastSize = Get2DCameraSize(mainCamera);
        Vector2 leftSize = new Vector2(lastSize.x * leftRatio, lastSize.y);
        Vector2 rightSize = new Vector2(lastSize.x - leftSize.x, lastSize.y);

        _leftMaterial.mainTextureScale = new Vector2(leftRatio, 1);
        _leftMaterial.mainTextureOffset = new Vector2((1-leftRatio) *.5f, 0);
        _rightMaterial.mainTextureScale = new Vector2((1-leftRatio), 1);
        _rightMaterial.mainTextureOffset = new Vector2(leftRatio *.5f, 0);

        _leftPlane.transform.position = mainCamera.transform.position + mainCamera.transform.forward;
        _rightPlane.transform.position = _leftPlane.transform.position;

        _leftPlane.transform.position += mainCamera.transform.right * (leftSize.x - lastSize.x) * .5f;
        _rightPlane.transform.position += mainCamera.transform.right * (lastSize.x - rightSize.x) * .5f;

        _leftPlane.transform.localScale = new Vector3(leftSize.x, 0, leftSize.y) * .1f;
        _rightPlane.transform.localScale = new Vector3(rightSize.x, 0, rightSize.y) * .1f;
    }
}
