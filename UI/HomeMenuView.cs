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
    public CinemachineVirtualCamera VirtualCamera;

    public HomeSceneSettings Settings;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnWorkshopButtonClick()
    {
        VirtualCamera.LookAt = Workshop.transform;
        DollyCart.m_Speed = Settings.DOLLY_TRACK_SPEED;
    }
}
