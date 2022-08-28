using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipmentAssignmentManager : MonoBehaviour, IManager
{
    [SerializeField]
    private GameObject equipmentItemsParent;

    [SerializeField]
    private GameDataManager gameDataManager;

    private List<AbstractEquipmentItem> equipmentItems;

    private List<AbstractEquipmentItem> armoryEquipmentItems;
    public void Initialize()
    {
        var currentSave = gameDataManager.GetCurrentlyLoadedSave();

        equipmentItems = equipmentItemsParent.GetComponentsInChildren<AbstractEquipmentItem>().ToList();
        armoryEquipmentItems = new List<AbstractEquipmentItem>();

        foreach (var itemName in currentSave.ArmoryItems)
        {
            armoryEquipmentItems.Add(equipmentItems.First(ei => string.Equals(ei.ItemName, itemName, StringComparison.OrdinalIgnoreCase)));
        }
    }

    public List<AbstractEquipmentItem> GetEquipmentItems()
    {
        return equipmentItems;
    }

    public List<AbstractEquipmentItem> GetArmoryItems()
    {
        return armoryEquipmentItems;
    }

    public void AddItemToArmory(AbstractEquipmentItem equipmentItem)
    {
        armoryEquipmentItems.Add(equipmentItem);
    }

    public bool TryEquipItems(List<EquipmentLoadoutItem> equipmentLoadoutItems, GameObject character, out Dictionary<EquipmentSlotType, GameObject> equipmentLoadoutDict)
    {
        var itemsToEquip = new List<AbstractEquipmentItem>();

        foreach (var equipmentLoadoutItem in equipmentLoadoutItems)
        {
            itemsToEquip.Add(equipmentItems.First(ei => string.Equals(ei.ItemName, equipmentLoadoutItem.ItemName, StringComparison.OrdinalIgnoreCase) && 
                equipmentLoadoutItem.EquipmentSlotType == ei.EquipmentSlotType)
            );
        }

        return TryEquipItems(itemsToEquip, character, out equipmentLoadoutDict);
    }

    public bool TryEquipItems(List<AbstractEquipmentItem> equipmentItems, GameObject character, out Dictionary<EquipmentSlotType, GameObject> equipmentLoadoutDict)
    {
        equipmentLoadoutDict = new Dictionary<EquipmentSlotType, GameObject>();

        try
        {
            if (equipmentItems != null && character != null)
            {
                var currentLoadoutDict = character.GetComponent<AbstractCharacterView>().EquipmentLoadoutDictionary;

                foreach (var item in equipmentItems)
                {
                    var rendererTarget = GetRendererTarget(item, character);

                    if (item is not EmptyEquipmentSlotItem)
                    {
                        var equipmentItemInstance = GameObject.Instantiate(item.Prefab);

                        item.AttachToRig(equipmentItemInstance, rendererTarget);

                        equipmentLoadoutDict.Add(item.EquipmentSlotType, equipmentItemInstance);
                    }
                }

                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to equip items for character! {ex}", this);
        }

        return false;
    }

    private GameObject GetRendererTarget(AbstractEquipmentItem equipmentItem, GameObject character)
    {
        switch (equipmentItem.EquipmentSlotType)
        {
            case EquipmentSlotType.Head:
                return character.GetComponentInChildren<Head>().gameObject;
            case EquipmentSlotType.Body:
                return character.GetComponentInChildren<Body>().gameObject;
            case EquipmentSlotType.RightPrimary:
            case EquipmentSlotType.RightSecondary:
                return character.GetComponentInChildren<RightHand>().gameObject;
            case EquipmentSlotType.LeftPrimary:
            case EquipmentSlotType.LeftSecondary:
                return character.GetComponentInChildren<LeftHand>().gameObject;
            case EquipmentSlotType.Feet:
                return character.GetComponentInChildren<Feet>().gameObject;
            default:
                Debug.Log("No target defined for slot type");
                return null;
        }
    }
}
