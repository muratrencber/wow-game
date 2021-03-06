using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawScreens : MonoBehaviour
{
    public static Camera LeftCamera, RightCamera;

    [SerializeField] GameObject _leftPlane, _rightPlane;
    [SerializeField] Camera _leftCamera, _rightCamera;
    [SerializeField] bool recenter;

    Vector2 lastSize;
    Camera mainCamera {get {return Camera.main;}}
    Vector2 lastScreenSizes = Vector2.zero;
    RenderTexture leftTexture, rightTexture;
    Material leftMaterial, rightMaterial;

    void Start()
    {
        LeftCamera = _leftCamera;
        RightCamera = _rightCamera;
        Draw(Slider.LeftRatio);
    }

    void Update()
    {
        Draw(Slider.LeftRatio);
    }

    Vector2 Get2DCameraSize(Camera cm)
    {
        float height = cm.orthographicSize * 2;
        float width = height * cm.aspect;
        return new Vector2(width, height);
    }

    void Draw(float leftRatio)
    {
        Vector2 currentScreenSizes = new Vector2(Screen.width, Screen.height);
        if(leftMaterial == null || rightMaterial == null)
        {
            leftMaterial = new Material(Shader.Find("Unlit/Texture"));
            rightMaterial = new Material(Shader.Find("Unlit/Texture"));

            _leftPlane.GetComponent<MeshRenderer>().sharedMaterial = leftMaterial;
            _rightPlane.GetComponent<MeshRenderer>().sharedMaterial = rightMaterial;
        }
        if(currentScreenSizes != lastScreenSizes)
        {
            leftTexture = new RenderTexture((int)currentScreenSizes.x, (int)currentScreenSizes.y, -1, UnityEngine.Experimental.Rendering.DefaultFormat.HDR);
            rightTexture = new RenderTexture((int)currentScreenSizes.x, (int)currentScreenSizes.y, -1, UnityEngine.Experimental.Rendering.DefaultFormat.HDR);
            
            _leftCamera.targetTexture = leftTexture;
            _rightCamera.targetTexture = rightTexture;
            
            leftMaterial.mainTexture = leftTexture;
            rightMaterial.mainTexture = rightTexture;
            lastScreenSizes = currentScreenSizes;  
        }

        lastSize = Get2DCameraSize(mainCamera);
        Vector2 leftSize = new Vector2(lastSize.x * leftRatio, lastSize.y);
        Vector2 rightSize = new Vector2(lastSize.x - leftSize.x, lastSize.y);

        leftMaterial.mainTextureScale = new Vector2(leftRatio, 1);
        rightMaterial.mainTextureScale = new Vector2((1-leftRatio), 1);
        if(recenter)
        {
            leftMaterial.mainTextureOffset = new Vector2((1-leftRatio) *.5f, 0);
            rightMaterial.mainTextureOffset = new Vector2(leftRatio *.5f, 0);
        }
        else
        {
            rightMaterial.mainTextureOffset = new Vector2(leftRatio, 0);
        }

        _leftPlane.transform.position = mainCamera.transform.position + mainCamera.transform.forward;
        _rightPlane.transform.position = _leftPlane.transform.position;

        _leftPlane.transform.position += mainCamera.transform.right * (leftSize.x - lastSize.x) * .5f;
        _rightPlane.transform.position += mainCamera.transform.right * (lastSize.x - rightSize.x) * .5f;

        _leftPlane.transform.localScale = new Vector3(leftSize.x, 0, leftSize.y) * .1f;
        _rightPlane.transform.localScale = new Vector3(rightSize.x, 0, rightSize.y) * .1f;
    }
}
