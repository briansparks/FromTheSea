using UnityEngine;
using UnityEngine.Events;

public class RaidOrchestrator : MonoBehaviour
{
    [SerializeField]
    private RaidManager raidManager;

    // Start is called before the first frame update
    void Start()
    {
        var boatInRangeOfDockEvent = new UnityAction(HandleBoatInRangeOfDock);
        EventManager.StartListening("BoatInRangeOfDock", boatInRangeOfDockEvent);

        var boatDockedEvent = new UnityAction(HandleBoatDocked);
        EventManager.StartListening("BoatDocked", boatDockedEvent);

        var exitShipEvent = new UnityAction(HandleExitShip);
        EventManager.StartListening("ExitShip", exitShipEvent);
    }

    private void HandleExitShip()
    {
        raidManager.OnExitShip();
    }

    private void HandleBoatInRangeOfDock()
    {
        raidManager.OnDockingStarted();
    }
    private void HandleBoatDocked()
    {
        raidManager.OnBoatDocked();
    }

    private void HandleLootCartLoaded()
    {

    }

    private void HandleTroopsBeginEscape()
    {

    }
}
