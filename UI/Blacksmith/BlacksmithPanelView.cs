using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlacksmithPanelView : AbstractStationPanelView
{
    private List<CharacterData> availableTroops;

    [SerializeField]
    private CharacterManager CharacterManager;

    [SerializeField]
    private GridLayoutGroup availableTroopsGrid;

    [SerializeField]
    private GameObject availableTroopGridItemPrefab;

    [SerializeField]
    private GameObject characterSpawnPositionAndRotation;

    [SerializeField]
    private GameObject equipmentItemsParent;

    [SerializeField]
    private EquipmentCostGrid equipmentCostGrid;

    private CharacterData currentTroopPreview;
    private GameObject currentActiveGridItem;

    private Dictionary<EquipmentSlotType, EquipmentSlotDetails> equipmentSlotDetailsDict;

    // Start is called before the first frame update
    void Start()
    {
        availableTroops = CharacterManager.GetAvailableTroopData();

        PopulateAvailableTroopsList(availableTroops);

        var equipmentItems = equipmentItemsParent.GetComponentsInChildren<AbstractEquipmentItem>().ToList();
        var equipmentSlots = gameObject.GetComponentsInChildren<EquipmentSlot>().ToList();

        InitializeEquipmentSlotDetailsDict(equipmentItems, equipmentSlots);
    }

    private void PopulateAvailableTroopsList(IEnumerable<CharacterData> availableTroopData)
    {
        foreach (var availableTroop in availableTroopData)
        {
            var gridItemInstance = GameObject.Instantiate(availableTroopGridItemPrefab);
            gridItemInstance.transform.SetParent(availableTroopsGrid.transform);

            var troopButton = gridItemInstance.GetComponentInChildren<Button>();

            var selectedTroop = availableTroop;
            troopButton.onClick.AddListener(() => { UpdateSelectedTroop(gridItemInstance, selectedTroop); });

            var tmp = gridItemInstance.GetComponentInChildren<TextMeshProUGUI>();
            tmp.text = availableTroop.Name;
        }
    }

    private void SpawnTroop(CharacterData characterData)
    {
        var spawnPos = characterSpawnPositionAndRotation.transform.position;
        var spawnRot = characterSpawnPositionAndRotation.transform.rotation;

        var characterPreviewSpawnRequest = new CharacterSpawnRequest()
        {
            CharacterData = characterData,
            Parent = StationGameObject,
            Position = spawnPos,
            Rotation = spawnRot
        };

        EventManager.TriggerEvent("SpawnCharacterPreview", characterPreviewSpawnRequest);     
    }

    private void UpdateSelectedTroop(GameObject selectedGridItem, CharacterData characterData)
    {
        var textMesh = selectedGridItem.GetComponentInChildren<TextMeshProUGUI>();
        textMesh.color = Color.yellow;

        if (currentTroopPreview != null)
        {
            currentActiveGridItem.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            EventManager.TriggerEvent("DestroyCharacter", currentTroopPreview.Id);
        }

        SpawnTroop(characterData);
        currentTroopPreview = characterData;
        currentActiveGridItem = selectedGridItem;
    }

    private void InitializeEquipmentSlotDetailsDict(List<AbstractEquipmentItem> equipmentItems, List<EquipmentSlot> equipmentSlots)
    {
        equipmentSlotDetailsDict = new Dictionary<EquipmentSlotType, EquipmentSlotDetails>();

        foreach (var equipmentSlot in equipmentSlots)
        {
            var equipmentListForSlot = equipmentItems.Where(ei => ei.EquipmentSlotType == equipmentSlot.EquipmentSlotType);

            var equipmentSlotDetails = new EquipmentSlotDetails();
            equipmentSlotDetails.CurrentIndex = 0;
            equipmentSlotDetails.EquipmentSlotInstance = equipmentSlot;
            equipmentSlotDetails.EquipmentItems = new List<AbstractEquipmentItem>(equipmentListForSlot);

            equipmentSlotDetailsDict[equipmentSlot.EquipmentSlotType] = equipmentSlotDetails;
        }
    }

    public void OnEquipmentSlotRightArrowClick(int slotType)
    {
        var foundSlotDetails = equipmentSlotDetailsDict.TryGetValue((EquipmentSlotType)slotType, out var equipmentSlotDetails);

        if (foundSlotDetails)
        {
            equipmentSlotDetails.CurrentIndex++;

            if (equipmentSlotDetails.CurrentIndex == equipmentSlotDetails.EquipmentItems.Count)
            {
                equipmentSlotDetails.CurrentIndex = 0;
            }

            var equipmentItem = equipmentSlotDetails.EquipmentItems[equipmentSlotDetails.CurrentIndex];

            equipmentSlotDetails.EquipmentSlotInstance.UpdateSlotSelection(equipmentItem);

            UpdateEquipmentCostGrid(equipmentItem.WoodCost, equipmentItem.IronCost, equipmentItem.LeatherCost);
        }
    }

    public void OnEquipmentSlotLeftArrowClick(int slotType)
    {
        var foundSlotDetails = equipmentSlotDetailsDict.TryGetValue((EquipmentSlotType)slotType, out var equipmentSlotDetails);

        if (foundSlotDetails)
        {
            equipmentSlotDetails.CurrentIndex--;

            if (equipmentSlotDetails.CurrentIndex < 0)
            {
                equipmentSlotDetails.CurrentIndex = equipmentSlotDetails.EquipmentItems.Count - 1;
            }

            var equipmentItem = equipmentSlotDetails.EquipmentItems[equipmentSlotDetails.CurrentIndex];

            equipmentSlotDetails.EquipmentSlotInstance.UpdateSlotSelection(equipmentItem);

            UpdateEquipmentCostGrid(equipmentItem.WoodCost, equipmentItem.IronCost, equipmentItem.LeatherCost);
        }
    }

    private void UpdateEquipmentCostGrid(int woodCost, int ironCost, int leatherCost)
    {
        var currentIronCost = int.Parse(equipmentCostGrid.IronCost.text);
        var currentWoodCost = int.Parse(equipmentCostGrid.WoodCost.text);
        var currentLeatherCost = int.Parse(equipmentCostGrid.LeatherCost.text);

        // TODO: Calculate this using the current full loadout that is selected
        var newIronCost = currentIronCost += ironCost;
        var newWoodCost = currentWoodCost += woodCost;
        var newLeatherCost = currentLeatherCost += leatherCost;

        equipmentCostGrid.IronCost.text = newIronCost.ToString();
        equipmentCostGrid.WoodCost.text = newWoodCost.ToString();
        equipmentCostGrid.LeatherCost.text = newLeatherCost.ToString();
    }
}
