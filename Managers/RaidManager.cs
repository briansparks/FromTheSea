using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RaidManager : MonoBehaviour, IManager
{
    public BoatSpawnPosition BoatSpawnPosition;
    public string PathToBoatPrefabs = "Boats/";
    public string DockMessageOnLootCartLoaded = "Press [e] to enter ship";
    public string EventToEmitOnPlayerEnteredShip = "PlayerEnteredShip";
    
    [SerializeField]
    private float pathToDockDuration;

    [SerializeField]
    private float pathToEscapeDuration;

    private IPlayerView playerView;

    private bool playerHasExitedShip;

    [SerializeField]
    private GameDataManager gameDataManager;

    [SerializeField]
    private BoatManager boatManager;

    [SerializeField]
    private CharacterManager characterManager;

    [SerializeField]
    private LootCartView lootCartView;

    [SerializeField]
    private LootCartPath lootCartPath;

    private BoatView boatView;

    private DOTweenPath pathToDock;
    private DOTweenPath pathToEscape;

    private LootCartPathNode currentLootCartPathNodeDestination;
    private DockLabelView dockLabelView;

    GameObject dockPortObj;

    // Start is called before the first frame update
    public void Initialize()
    {
        var currentSave = gameDataManager.GetCurrentlyLoadedSave();

        //boatView = SpawnActiveBoatInScene();

        boatView = GameObject.FindObjectOfType<BoatView>();

        boatView.InitializeForRaid(pathToDockDuration);

        //SpawnBoatTroops(boatView, currentSave.ActiveRaid.TroopAssignments);

        pathToDock = GameObject.FindObjectOfType<PathToDock>().GetComponent<DOTweenPath>();
        pathToEscape = GameObject.FindObjectOfType<PathToEscape>().GetComponent<DOTweenPath>();
        dockPortObj = GameObject.FindObjectOfType<DockingPort>().gameObject;

        currentLootCartPathNodeDestination = lootCartPath.GetPathNodes().First(n => n.PathType == LootCartPathType.Start);

        dockLabelView = dockPortObj.GetComponentInChildren<DockLabelView>();
        playerView = GameObject.FindGameObjectWithTag(Constants.PLAYER_TAG).GetComponent<IPlayerView>();

        //TODO: testing
        OnLootCartPushed();

        //StartRaid();
    }

    private BoatView SpawnActiveBoatInScene()
    {
        var activeBoat = boatManager.GetActiveBoat();

        var fullPath = PathToBoatPrefabs + activeBoat.PrefabName;
        var boatPrefab = Resources.Load<GameObject>(fullPath);

        boatManager.TrySpawnBoat(boatPrefab, BoatSpawnPosition.transform.position, null, out var boatView);
        return boatView;
    }

    private void SpawnBoatTroops(BoatView boatView, IEnumerable<TroopAssignmentData> troopAssignments)
    {
        foreach (var troopAssignment in troopAssignments)
        {
            boatView.TryFindSeatById(troopAssignment.SeatId, out var seatView);
            characterManager.TryGetCharacterDataById(troopAssignment.CharacterId, out var characterData);

            var spawnPos = seatView.GetCharacterSpawnPosition();
            var spawnRot = seatView.GetCharacterSpawnRotation();

            var activeRaidCharacterSpawnRequest = new ActiveRaidCharacterSpawnRequest()
            {
                AssignedSeat = seatView.gameObject,
                CharacterData = characterData,
                Parent = null,
                Position = spawnPos,
                Rotation = spawnRot
            };

            EventManager.TriggerEvent("SpawnActiveRaidCharacter", activeRaidCharacterSpawnRequest);
        }
    }

    private void StartRaid()
    {
        boatView.BeginPath(pathToDock, pathToDockDuration);
        //boatView.UpdateCurrentAction(() => boatView.LookAtNextWaypoint());
    }

    public void OnDockingStarted()
    {
        var endOfDockPoint = dockPortObj.GetComponentInChildren<EndOfDockPoint>();
        var boatLookAtPoint = dockPortObj.GetComponentInChildren<BoatLookAtDockPoint>();

        boatView.UpdateCurrentAction(() => boatView.DockBoat(endOfDockPoint.gameObject, boatLookAtPoint.gameObject));
    }

    public void OnLootCartPushed()
    {
        lootCartView.OnCartPush(currentLootCartPathNodeDestination.transform.position);
    }

    public void OnPlayerEnterShip()
    {
        var foundOpenSeat = boatView.TryFindNextOpenSeat(out var seatView);

        if (foundOpenSeat)
        {
            playerView.AssignedSeat = seatView.gameObject;
            playerView.SitDownOnSeat();

            dockLabelView.enabled = false;
        }
        else
        {
            Debug.LogError($"Unable to find open seat for player!", this);
        }

        boatView.BeginPath(pathToEscape, pathToEscapeDuration);
    }
    public void OnLootCartReachedDestination()
    {
        currentLootCartPathNodeDestination = currentLootCartPathNodeDestination.NextNode;

        if (currentLootCartPathNodeDestination == null)
        {
            // We are at the end of the path, load it into boat, and start the escape
            EventManager.TriggerEvent("LootCartLoaded");
        }
        else
        {
            Debug.Log("Moving to next loot cart path node...");
            lootCartView.OnCartPush(currentLootCartPathNodeDestination.transform.position);
        }
    }

    public void OnLootCartStopped()
    {
        lootCartView.OnCartStopped();
    }
    public void OnBoatDocked()
    {
    }

    public void OnLootCartLoaded()
    {
        var loot = lootCartView.GetComponentInChildren<Loot>();
        loot.transform.SetParent(boatView.gameObject.transform);

        GameObject.Destroy(lootCartView.gameObject);

        var lootPosition = boatView.GetComponentInChildren<ShipLootPosition>();
        loot.transform.position = lootPosition.transform.position;

        dockLabelView.UpdateText(DockMessageOnLootCartLoaded);
        dockLabelView.UpdateEventToEmit(EventToEmitOnPlayerEnteredShip);
        dockLabelView.enabled = true;
    }

    public void OnExitShip()
    {
        var player = GameObject.FindGameObjectWithTag(Constants.PLAYER_TAG);
        var shipExitPoint = dockPortObj.GetComponentInChildren<ShipExitPoint>();

        characterManager.MoveCharacterToPosition(shipExitPoint.transform.position, player.gameObject);

        dockLabelView.enabled = false;
        playerHasExitedShip = true;
    }
    //public void OnBoatBoardedForEscape()
    //{
    //    boatView.UpdateCurrentAction(() => boatView.BeginPath(pathToEscape, pathToEscapeDuration));
    //}
    public void OnAttackersEscaped()
    {

    }
}
