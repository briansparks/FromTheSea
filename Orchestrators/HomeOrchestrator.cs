using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HomeOrchestrator : MonoBehaviour
{
    [SerializeField]
    private HomeSceneSettings homeSceneSettings;

    public HomeManager HomeManager;
    public HomeMenuView HomeMenuView;
    public GameObject GarrisonPanel;
    public GameObject BlacksmithPanel;
    public GameObject WorkshopPanel;
    public GameObject RaidSetupPanel;

    // Start is called before the first frame update
    void Start()
    {
        var garrisonAction = new UnityAction(() => { StartCoroutine(DisplayOverlayAfterDelay(GarrisonPanel)); });
        EventManager.StartListening("CameraReachedGarrison", garrisonAction);

        var workshopAction = new UnityAction(() => { StartCoroutine(DisplayOverlayAfterDelay(WorkshopPanel)); });
        EventManager.StartListening("CameraReachedWorkshop", workshopAction);

        var blacksmithAction = new UnityAction(() => { StartCoroutine(DisplayOverlayAfterDelay(BlacksmithPanel)); });
        EventManager.StartListening("CameraReachedBlacksmith", blacksmithAction);

        var raidSetupAction = new UnityAction(() => { StartCoroutine(DisplayOverlayAfterDelay(RaidSetupPanel)); });
        EventManager.StartListening("CameraReachedRaidSetup", raidSetupAction);

        var startRaidAction = new UnityAction<RaidLocationDto>((raidLocation) => { HandleRaidStart(raidLocation); });
        EventManager.StartListening("StartRaid", startRaidAction);
    }

    private IEnumerator DisplayOverlayAfterDelay(GameObject displayOverlayObj)
    {
        yield return new WaitForSeconds(homeSceneSettings.COROUTINE_DISPLAY_OVERLAY_TIME);
        HomeMenuView.EnterStationView(displayOverlayObj);
    }

    private void HandleRaidStart(RaidLocationDto raidLocation)
    {
        HomeManager.HandleRaidStart(raidLocation);
    }
}
