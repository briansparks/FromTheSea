using Cinemachine;
using UnityEngine;

public class HomeMenuView : MonoBehaviour
{
    public GameObject Workshop;
    public GameObject Blacksmith;
    public GameObject Garrison;
    public GameObject RaidSetup;

    public CinemachineSmoothPath WorkshopDollyTrack;
    public CinemachineSmoothPath BlacksmithDollyTrack;
    public CinemachineSmoothPath GarrisonDollyTrack;
    public CinemachineSmoothPath RaidSetupDollyTrack;

    public CinemachineDollyCart DollyCart;
    public CinemachineVirtualCamera WorkshopVirtualCamera;
    public CinemachineVirtualCamera BlacksmithVirtualCamera;
    public CinemachineVirtualCamera GarrisonVirtualCamera;
    public CinemachineVirtualCamera RaidSetupVirtualCamera;

    public HomeSceneSettings Settings;
    public GameObject HomeMenuPanel;
    public GameObject SelectedStationMenu;

    private GameObject activeStationPanel;
    private void Start()
    {
        HideSelectedStationMenu();
    }
    public void OnWorkshopButtonClick()
    {
        var eventEmitter = WorkshopDollyTrack.GetComponent<DollyTrackEventEmitter>();
        eventEmitter.enabled = true;

        ResestDollyCart(WorkshopDollyTrack);
        ResetVirtualCamera(WorkshopVirtualCamera, Workshop.transform);
        HideMainMenu();
    }

    public void OnBlacksmithButtonClick()
    {
        var eventEmitter = BlacksmithDollyTrack.GetComponent<DollyTrackEventEmitter>();
        eventEmitter.enabled = true;

        ResestDollyCart(BlacksmithDollyTrack);
        ResetVirtualCamera(BlacksmithVirtualCamera, Blacksmith.transform);
        HideMainMenu();
    }

    public void OnGarrisonButtonClick()
    {
        var eventEmitter = GarrisonDollyTrack.GetComponent<DollyTrackEventEmitter>();
        eventEmitter.enabled = true;

        ResestDollyCart(GarrisonDollyTrack);
        ResetVirtualCamera(GarrisonVirtualCamera, Garrison.transform);
        HideMainMenu();
    }

    public void OnRaidButtonClick()
    {
        var eventEmitter = RaidSetupDollyTrack.GetComponent<DollyTrackEventEmitter>();
        eventEmitter.enabled = true;

        ResestDollyCart(RaidSetupDollyTrack);
        ResetVirtualCamera(RaidSetupVirtualCamera, RaidSetup.transform);
        HideMainMenu();
    }

    public void OnBackButtonClick()
    {
        SetDollyCartToDefaults();
        ResetVirtualCamera(GarrisonVirtualCamera, Garrison.transform);
        DisplayMainMenu();
        HideSelectedStationMenu();
        activeStationPanel.SetActive(false);
        ResetEventEmitters();
    }

    public void EnterStationView(GameObject stationPanel)
    {
        stationPanel.SetActive(true);
        DisplaySelectedStationMenu();
        activeStationPanel = stationPanel;
    }

    private void ResetEventEmitters()
    {
        WorkshopDollyTrack.GetComponent<DollyTrackEventEmitter>().ResetToDefaults();
        BlacksmithDollyTrack.GetComponent<DollyTrackEventEmitter>().ResetToDefaults();
        GarrisonDollyTrack.GetComponent<DollyTrackEventEmitter>().ResetToDefaults();
        RaidSetupDollyTrack.GetComponent<DollyTrackEventEmitter>().ResetToDefaults();
    }
    private void SetDollyCartToDefaults()
    {
        DollyCart.m_Position = 0;
        DollyCart.m_Speed = 0;
        // TODO: this should be a config
        DollyCart.transform.position = new Vector3(166.912155f, 26.9616795f, 71.6189423f);
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
        cinemachineVirtualCamera.LookAt = lookAtTarget;
        cinemachineVirtualCamera.Priority++;
    }

    private void ResetAllCameraPriorities()
    {
        WorkshopVirtualCamera.Priority = 0;
        BlacksmithVirtualCamera.Priority = 0;
        GarrisonVirtualCamera.Priority = 0;
        RaidSetupVirtualCamera.Priority = 0;
    }
    private void DisplaySelectedStationMenu()
    {
        SelectedStationMenu.SetActive(true);
    }

    private void HideSelectedStationMenu()
    {
        SelectedStationMenu.SetActive(false);
    }

    private void HideMainMenu()
    {
        HomeMenuPanel.SetActive(false);
    }

    private void DisplayMainMenu()
    {
        HomeMenuPanel.SetActive(true);
    }
}
