
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
    private GameObject availableTroopGridItemPrefab;
    private void Start()
    {
        boatView = SpawnBoat();
        availableTroops = CharacterManager.GetAvailableTroopData();

        PopulateAvailableTroopsList(availableTroops);
    }
    private BoatView SpawnBoat()
    {
        if (RaidSetupZone.TrySpawnBoat(out var boatView))
        {
            return boatView;
        }
        else
        {
            // Display error or try again?
            return null;
        }
    }

    private void PopulateAvailableTroopsList(IEnumerable<CharacterData> availableTroopData)
    {
        foreach (var availableTroop in availableTroopData)
        {
            var gridItemInstance = GameObject.Instantiate(availableTroopGridItemPrefab);
            gridItemInstance.transform.SetParent(availableTroopsGrid.transform);

            var troopButton = gridItemInstance.GetComponentInChildren<Button>();

            var selectedTroop = availableTroop;
            troopButton.onClick.AddListener(() => { SpawnTroop(selectedTroop); });

            var tmp = gridItemInstance.GetComponentInChildren<TextMeshProUGUI>();
            tmp.text = availableTroop.Name;
        }
    }
    private void SpawnTroop(CharacterData characterData)
    {
        if(boatView.TryFindNextOpenSeat(out var seatView))
        {
            var spawnPos = seatView.GetCharacterSpawnPosition();
            var spawnRot = seatView.GetCharacterSpawnRotation();

            var characterSpawnRequest = new CharacterSpawnRequest() 
            { 
                CharacterData = characterData, 
                Parent = boatView.gameObject, 
                Position = spawnPos,
                Rotation = spawnRot,
                AssignedSeat = seatView.gameObject
            };

            seatView.IsOccupied = true;

            EventManager.TriggerEvent("SpawnCharacter", characterSpawnRequest);
        }
        else
        {
            Debug.LogError($"No open seat found, unable to spawn character!", this);
        }
    }
}
