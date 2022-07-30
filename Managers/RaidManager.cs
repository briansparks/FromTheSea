using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class RaidManager : MonoBehaviour, IManager
{
    public BoatSpawnPosition BoatSpawnPosition;
    public string PathToBoatPrefabs = "Boats/";

    private bool playerHasExitedShip;

    [SerializeField]
    private GameDataManager gameDataManager;

    [SerializeField]
    private BoatManager boatManager;

    [SerializeField]
    private CharacterManager characterManager;

    private BoatView boatView;

    private DOTweenPath pathToDock;
    private DOTweenPath pathToEscape;

    GameObject dockPortObj;

    // Start is called before the first frame update
    public void Initialize()
    {
        var currentSave = gameDataManager.GetCurrentlyLoadedSave();

        boatView = SpawnActiveBoatInScene();
        boatView.InitializeForRaid();

        SpawnBoatTroops(boatView, currentSave.ActiveRaid.TroopAssignments);

        pathToDock = GameObject.FindObjectOfType<PathToDock>().GetComponent<DOTweenPath>();
        pathToEscape = GameObject.FindObjectOfType<PathToEscape>().GetComponent<DOTweenPath>();
        dockPortObj = GameObject.FindObjectOfType<DockingPort>().gameObject;

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
        boatView.BeginPath(pathToDock);
        //boatView.UpdateCurrentAction(() => boatView.LookAtNextWaypoint());
    }

    public void OnDockingStarted()
    {
        var endOfDockPoint = dockPortObj.GetComponentInChildren<EndOfDockPoint>();
        var boatLookAtPoint = dockPortObj.GetComponentInChildren<BoatLookAtDockPoint>();

        boatView.UpdateCurrentAction(() => boatView.DockBoat(endOfDockPoint.gameObject, boatLookAtPoint.gameObject));
    }
    public void OnBoatDocked()
    {
    }

    public void OnLootCartLoaded()
    {

    }

    public void OnExitShip()
    {
        var player = GameObject.FindGameObjectWithTag(Constants.PLAYER_TAG);
        var shipExitPoint = dockPortObj.GetComponentInChildren<ShipExitPoint>();
        var labelView = dockPortObj.GetComponentInChildren<DockLabelView>();

        characterManager.MoveCharacterToPosition(shipExitPoint.transform.position, player.gameObject);

        labelView.UpdateTextOnShipExit();
        playerHasExitedShip = true;
    }
    public void OnBoatBoardedForEscape()
    {
        boatView.UpdateCurrentAction(() => boatView.BeginPath(pathToEscape));
    }
    public void OnAttackersEscaped()
    {

    }
}
