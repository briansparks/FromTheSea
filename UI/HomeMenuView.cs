using Cinemachine;
using UnityEngine;

public class HomeMenuView : MonoBehaviour
{
    public GameObject Workshop;
    public GameObject Blacksmith;
    public GameObject Garrison;

    public CinemachineSmoothPath WorkshopDollyTrack;
    public CinemachineSmoothPath BlacksmithDollyTrack;
    public CinemachineSmoothPath GarrisonDollyTrack;
    public CinemachineDollyCart DollyCart;
    public CinemachineVirtualCamera WorkshopVirtualCamera;
    public CinemachineVirtualCamera BlacksmithVirtualCamera;
    public CinemachineVirtualCamera GarrisonVirtualCamera;

    public HomeSceneSettings Settings;

    public void OnWorkshopButtonClick()
    {
        ResestDollyCart(WorkshopDollyTrack);
        ResetVirtualCamera(WorkshopVirtualCamera, Workshop.transform);
    }

    public void OnBlacksmithButtonClick()
    {
        ResestDollyCart(BlacksmithDollyTrack);
        ResetVirtualCamera(BlacksmithVirtualCamera, Blacksmith.transform);
    }

    public void OnGarrisonButtonClick()
    {
        ResestDollyCart(GarrisonDollyTrack);
        ResetVirtualCamera(GarrisonVirtualCamera, Garrison.transform);
    }

    private void ResestDollyCart(CinemachineSmoothPath targetDollyTrack)
    {
        DollyCart.m_Path = targetDollyTrack;
        DollyCart.m_Position = 0;
        DollyCart.m_Speed = Settings.DOLLY_TRACK_SPEED;
    }

    private void ResetVirtualCamera(CinemachineVirtualCamera cinemachineVirtualCamera, Transform lookAtTarget)
    {
        ResetAllCameraPriorities();
        cinemachineVirtualCamera.LookAt= lookAtTarget;
        cinemachineVirtualCamera.Priority++;
    }

    private void ResetAllCameraPriorities()
    {
        WorkshopVirtualCamera.Priority = 0;
        BlacksmithVirtualCamera.Priority = 0;
        GarrisonVirtualCamera.Priority = 0;
    }
}
