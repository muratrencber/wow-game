using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamSlider : MonoBehaviour
{
    public GameObject Cam1;
    public float CamValue, testx;
    [SerializeField]Camera cam1, cam2;
   public CinemachineVirtualCamera cam1Machine, cam2Machine;

    static CamSlider instance;

    private void Awake()
    {
        instance = this;
    }

    public static void SetCameras(float leftRatio)
    {
        if(!instance)
            return;
        instance.cam1.rect = new Rect(0, 0, leftRatio, 1);
        instance.cam2.rect = new Rect(leftRatio, 0, 1 - leftRatio, 1);
        float offset = (leftRatio - 1);
        CinemachineTransposer cft = instance.cam1Machine.GetCinemachineComponent<CinemachineTransposer>();
        offset *= instance.cam1Machine.m_Lens.OrthographicSize * 2;
        Vector3 offsetVec = cft.m_FollowOffset;
        offsetVec.x = offset;
        cft.m_FollowOffset = offsetVec;

        float otherOffset = leftRatio;
        otherOffset *= instance.cam2Machine.m_Lens.OrthographicSize * 2;
        CinemachineTransposer cft2 = instance.cam2Machine.GetCinemachineComponent<CinemachineTransposer>();
        Vector3 offsetVec2 = cft2.m_FollowOffset;
        offsetVec2.x = otherOffset;
        cft2.m_FollowOffset = offsetVec2;
    }
}
