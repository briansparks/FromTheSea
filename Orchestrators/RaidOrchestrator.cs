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

        var lootCartBeingPushedEvent = new UnityAction(HandleLootCartBeingPushed);
        EventManager.StartListening("LootCartBeingPushed", lootCartBeingPushedEvent);

        var lootCartReachedDestinationEvent = new UnityAction(HandleLootCartReachedDestination);
        EventManager.StartListening("LootCartReachedDestination", lootCartReachedDestinationEvent);

        var lootCartLoadedEvent = new UnityAction(HandleLootCartLoaded);
        EventManager.StartListening("LootCartLoaded", lootCartLoadedEvent);

        var playerEnteredShipEvent = new UnityAction(HandlePlayerEnterShip);
        EventManager.StartListening("PlayerEnteredShip", playerEnteredShipEvent);
    }

    private void HandleExitShip()
    {
        raidManager.OnExitShip();
    }

    private void HandlePlayerEnterShip()
    {
        raidManager.OnPlayerEnterShip();
    }
    private void HandleLootCartReachedDestination()
    {
        raidManager.OnLootCartReachedDestination();
    }

    private void HandleBoatInRangeOfDock()
    {
        raidManager.OnDockingStarted();
    }
    private void HandleBoatDocked()
    {
        raidManager.OnBoatDocked();
    }

    private void HandleLootCartBeingPushed()
    {
        raidManager.OnLootCartPushed();
    }

    private void HandleLootCartStopped()
    {

    }
    private void HandleLootCartLoaded()
    {
        raidManager.OnLootCartLoaded();
    }

    private void HandleTroopsBeginEscape()
    {

    }
}
