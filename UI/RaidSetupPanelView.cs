
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RaidSetupPanelView : MonoBehaviour
{
    public RaidSetupZone RaidSetupZone;
    public CharacterManager CharacterManager;

    private BoatView boatView;
    private List<CharacterData> availableTroops;

    [SerializeField]
    private GridLayoutGroup availableTroopsGrid;

    [SerializeField]
    private GridLayoutGroup assignedCrewGrid;

    [SerializeField]
    private GameObject availableTroopGridItemPrefab;

    [SerializeField]
    public GameObject raidMapPanel;
    private void Start()
    {
        boatView = RaidSetupZone.GetComponentInChildren<BoatView>();
        availableTroops = CharacterManager.GetAvailableTroopData();

        PopulateAvailableTroopsList(availableTroops);
    }

    private void PopulateAvailableTroopsList(IEnumerable<CharacterData> availableTroopData)
    {
        foreach (var availableTroop in availableTroopData)
        {
            var gridItemInstance = GameObject.Instantiate(availableTroopGridItemPrefab);
            gridItemInstance.transform.SetParent(availableTroopsGrid.transform);

            var troopButton = gridItemInstance.GetComponentInChildren<Button>();

            var selectedTroop = availableTroop;
            troopButton.onClick.AddListener(() => { AssignTroop(gridItemInstance, selectedTroop); });

            var tmp = gridItemInstance.GetComponentInChildren<TextMeshProUGUI>();
            tmp.text = availableTroop.Name;
        }
    }

    private void AssignTroop(GameObject availableTroopItemInstance, CharacterData characterData)
    {
        SpawnTroop(characterData);

        var gridItemInstance = GameObject.Instantiate(availableTroopGridItemPrefab);
        var tmp = gridItemInstance.GetComponentInChildren<TextMeshProUGUI>();
        tmp.text = characterData.Name;

        gridItemInstance.transform.SetParent(assignedCrewGrid.transform);

        Destroy(availableTroopItemInstance);
    }
    private void SpawnTroop(CharacterData characterData)
    {
        if(boatView.TryFindNextOpenSeat(out var seatView))
        {
            var spawnPos = seatView.GetCharacterSpawnPosition();
            var spawnRot = seatView.GetCharacterSpawnRotation();

            var activeRaidCharacterSpawnRequest = new ActiveRaidCharacterSpawnRequest() 
            { 
                CharacterData = characterData, 
                Parent = boatView.gameObject, 
                Position = spawnPos,
                Rotation = spawnRot,
                AssignedSeat = seatView.gameObject
            };

            seatView.IsOccupied = true;

            EventManager.TriggerEvent("SpawnActiveRaidCharacter", activeRaidCharacterSpawnRequest);
        }
        else
        {
            Debug.LogError($"No open seat found, unable to spawn character!", this);
        }
    }

    public void OnResetButtonClick()
    {
        DestroyTroopEntriesInGrid(assignedCrewGrid);
        DestroyTroopEntriesInGrid(availableTroopsGrid);

        var npcViews = boatView.GetComponentsInChildren<NPCView>();
        foreach (var npcView in npcViews)
        {
            EventManager.TriggerEvent("DestroyCharacter", npcView.Id);
        }

        CharacterManager.ResetActiveRaidCharacters();

        boatView.ResetAllSeats();
        PopulateAvailableTroopsList(availableTroops);
    }

    public void OnBeginRaidClick()
    {
        raidMapPanel.SetActive(true);
    }

    private void DestroyTroopEntriesInGrid(GridLayoutGroup grid)
    {
        var troopEntries = grid.GetComponentsInChildren<TroopEntry>();
        foreach (var entry in troopEntries)
        {
            Destroy(entry.gameObject);
        }
    }
}
