using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ResourceEnums;

public class BlacksmithPanelView : AbstractStationPanelView
{
    public AudioSource PurchaseSound;

    [SerializeField]
    private CharacterManager characterManager;

    [SerializeField]
    private ResourceManager resourceManager;

    [SerializeField]
    private GridLayoutGroup availableTroopsGrid;

    [SerializeField]
    private GridLayoutGroup armoryItemsGrid;

    [SerializeField]
    private GameObject availableTroopGridItemPrefab;

    [SerializeField]
    private GameObject armoryGridItemPrefab;

    [SerializeField]
    private GameObject characterSpawnPositionAndRotation;

    [SerializeField]
    private EquipmentAssignmentManager equipmentAssignmentManager;

    [SerializeField]
    private EquipmentCostGrid equipmentCostGrid;

    [SerializeField]
    private TroopsStatsPanelView troopStats;

    [SerializeField]
    private Button purchaseButton;

    private CharacterData currentTroopPreviewData;
    private GameObject currentActiveGridItem;

    private Dictionary<EquipmentSlotType, EquipmentSlotDetails> equipmentSlotDetailsDict;
    private Dictionary<EquipmentSlotType, int> currentlyEquippedIndexDict;

    // Start is called before the first frame update
    void Start()
    {
        var availableTroops = characterManager.GetAvailableTroopData();
        PopulateAvailableTroopsList(availableTroops);

        var equipmentItems = equipmentAssignmentManager.GetEquipmentItems();
        var equipmentSlots = gameObject.GetComponentsInChildren<EquipmentSlot>().ToList();

        InitializeEquipmentSlotDetailsDict(equipmentItems, equipmentSlots);
        InitializeCurrentlyEquippedIndexDictionary();

        RefreshArmoryItems();
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

        if (currentTroopPreviewData != null)
        {
            currentActiveGridItem.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            EventManager.TriggerEvent("DestroyCharacter", currentTroopPreviewData.Id);
        }

        SpawnTroop(characterData);
        currentTroopPreviewData = characterData;
        currentActiveGridItem = selectedGridItem;

        SyncEquipmentSlotsWithEquipmentLoadout(characterData.EquipmentLoadoutItems);
    }

    private void InitializeCurrentlyEquippedIndexDictionary()
    {
        currentlyEquippedIndexDict = new Dictionary<EquipmentSlotType, int>();

        foreach (var val in Enum.GetValues(typeof(EquipmentSlotType)))
        {
            currentlyEquippedIndexDict.Add((EquipmentSlotType)val, 0);
        }
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

            OnSlotArrowClick(equipmentSlotDetails);
        }
    }

    private void SyncEquipmentSlotsWithEquipmentLoadout(List<EquipmentLoadoutItem> equipmentLoadoutItems)
    {
        foreach (var equipmentLoadoutItem in equipmentLoadoutItems)
        {
            var equipmentSlotDetails = equipmentSlotDetailsDict[equipmentLoadoutItem.EquipmentSlotType];

            var itemIndex = equipmentSlotDetails.EquipmentItems.FindIndex(
                ei => string.Equals(ei.ItemName, equipmentLoadoutItem.ItemName, StringComparison.OrdinalIgnoreCase)
            );

            if (itemIndex != -1)
            {
                equipmentSlotDetails.CurrentIndex = itemIndex;
                equipmentSlotDetails.EquipmentSlotInstance.UpdateSlotSelection(equipmentSlotDetails.EquipmentItems[itemIndex]);

                currentlyEquippedIndexDict[equipmentLoadoutItem.EquipmentSlotType] = itemIndex;
            }
        }

        UpdateCurrentlyEquippedIconForAllSlots(true);
        UpdateTroopStats();
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

            OnSlotArrowClick(equipmentSlotDetails);
        }
    }

    private void OnSlotArrowClick(EquipmentSlotDetails equipmentSlotDetails)
    {
        var equipmentItem = equipmentSlotDetails.EquipmentItems[equipmentSlotDetails.CurrentIndex];

        equipmentSlotDetails.EquipmentSlotInstance.UpdateSlotSelection(equipmentItem);

        if (equipmentSlotDetails.CurrentIndex != currentlyEquippedIndexDict[equipmentSlotDetails.EquipmentSlotInstance.EquipmentSlotType])
        {
            UpdateCurrentlyEquippedIconForSlot(false, equipmentItem.EquipmentSlotType);
        }
        else
        {
            UpdateCurrentlyEquippedIconForSlot(true, equipmentItem.EquipmentSlotType);
        }

        var currentlySelectedEquipmentItems = GetSelectedLoadout();
        var availableResources = resourceManager.GetAvailableResources();

        UpdateEquipmentCostGrid(currentlySelectedEquipmentItems, availableResources);
    }
    public void OnPurchase()
    {
        var selectedEquipmentItems = GetSelectedLoadout();

        var characterLookupSuccessful = characterManager.TryGetCharacterInstanceById(currentTroopPreviewData.Id, out var characterGameObj);
        characterManager.TryUpdateTroopData(currentTroopPreviewData);

        if (characterLookupSuccessful)
        {
            EventManager.TriggerEvent("DestroyCharacter", currentTroopPreviewData.Id);

            var characterData = currentTroopPreviewData;
            characterData.Id = Guid.NewGuid();

            var loadoutItems = new List<EquipmentLoadoutItem>();
            var previousLoadout = currentTroopPreviewData.EquipmentLoadoutItems;

            var allEquipmentItems = equipmentAssignmentManager.GetEquipmentItems();

            foreach (var item in selectedEquipmentItems)
            {
                loadoutItems.Add(new EquipmentLoadoutItem() { ItemName = item.ItemName, EquipmentSlotType = item.EquipmentSlotType });

                var correspondingPreviousLoadoutItem = previousLoadout.First(li => li.EquipmentSlotType == item.EquipmentSlotType);
                var correspondingPreviousEquipmentItem = allEquipmentItems.First(ei => ei.ItemName == correspondingPreviousLoadoutItem.ItemName);

                if (correspondingPreviousEquipmentItem is not EmptyEquipmentSlotItem 
                    && correspondingPreviousEquipmentItem.ItemName != item.ItemName
                    && !correspondingPreviousEquipmentItem.IsItemFree)
                {
                    equipmentAssignmentManager.AddItemToArmory(correspondingPreviousEquipmentItem);
                }
            }

            characterData.EquipmentLoadoutItems = loadoutItems;

            SpawnTroop(characterData);

            var updateCost = new ResourceData();
            
            foreach (var item in selectedEquipmentItems)
            {
                updateCost.Wood -= item.WoodCost;
                updateCost.Iron -= item.IronCost;
                updateCost.Leather -= item.LeatherCost;
            }

            EventManager.TriggerEvent("UpdateResources", updateCost);

            UpdateCurrentlyEquippedIconForAllSlots(true);
            UpdateTroopStats();

            PurchaseSound.Play();
            RefreshArmoryItems();
        }
    }

    private void RefreshArmoryItems()
    {
        var armoryItems = equipmentAssignmentManager.GetArmoryItems();

        var currentGridEntries = armoryItemsGrid.GetComponentsInChildren<ArmoryItemEntry>();
        foreach (var entry in currentGridEntries)
        {
            Destroy(entry.gameObject);
        }

        foreach (var item in armoryItems)
        {
            var gridItemInstance = GameObject.Instantiate(armoryGridItemPrefab);
            gridItemInstance.transform.SetParent(armoryItemsGrid.transform);

            var tmp = gridItemInstance.GetComponentInChildren<TextMeshProUGUI>();
            tmp.text = item.ItemName;
        }
    }
    private void UpdateTroopStats()
    {
        int ar = 0;
        int dr = 0;

        foreach (var kvp in equipmentSlotDetailsDict)
        {
            var equipmentItem = kvp.Value.EquipmentItems[kvp.Value.CurrentIndex];

            if (equipmentItem is ArmorItem)
            {
                var armorItem = (ArmorItem)equipmentItem;

                dr += armorItem.DefenseRating;
            }
            else if (equipmentItem is WeaponItem)
            {
                var weaponItem = (WeaponItem)equipmentItem;

                ar += weaponItem.AttackRating;
            }
        }

        troopStats.UpdateAttackRating(ar);
        troopStats.UpdateDefenseRating(dr);
    }
    private List<AbstractEquipmentItem> GetSelectedLoadout()
    {
        var selectedItems = new List<AbstractEquipmentItem>();

        foreach (var kvp in equipmentSlotDetailsDict)
        {
            var equipmentItemsForSlot = kvp.Value.EquipmentItems;

            if (equipmentItemsForSlot.Any())
            {
                selectedItems.Add(equipmentItemsForSlot[kvp.Value.CurrentIndex]);
            }
        }

        return selectedItems;
    }

    private void UpdateCurrentlyEquippedIconForAllSlots(bool active)
    {
        foreach (var kvp in equipmentSlotDetailsDict)
        {
            kvp.Value.EquipmentSlotInstance.SetCurrentlyEquippedIconState(active);
        }
    }

    private void UpdateCurrentlyEquippedIconForSlot(bool active, EquipmentSlotType slotType)
    {
        var equipmentSlotInstance = equipmentSlotDetailsDict[slotType].EquipmentSlotInstance;
        equipmentSlotInstance.SetCurrentlyEquippedIconState(active);
        
    }
    private void UpdateEquipmentCostGrid(List<AbstractEquipmentItem> selectedEquipmentItems, ResourceData availableResources)
    {
        int ironCost = 0, woodCost = 0, leatherCost = 0;

        foreach (var item in selectedEquipmentItems)
        {
            ironCost += item.IronCost;
            woodCost += item.WoodCost;
            leatherCost += item.LeatherCost;
        }

        equipmentCostGrid.UpdateResourceLabel(woodCost, ResourceType.Wood, availableResources.Wood - woodCost < 0 ? Color.red : Color.white);
        equipmentCostGrid.UpdateResourceLabel(ironCost, ResourceType.Iron, availableResources.Iron - ironCost < 0 ? Color.red : Color.white);
        equipmentCostGrid.UpdateResourceLabel(leatherCost, ResourceType.Leather, availableResources.Leather - leatherCost < 0 ? Color.red : Color.white);

        if (availableResources.Wood - woodCost < 0 || availableResources.Iron - ironCost < 0 || availableResources.Leather - leatherCost < 0)
        {
            purchaseButton.interactable = false;
        }
        else
        {
            purchaseButton.interactable = true;
        }
    }
}
