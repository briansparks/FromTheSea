using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI header;

    [SerializeField]
    private RawImage slotIcon; 

    public EquipmentSlotType EquipmentSlotType;

    public void UpdateHeader(string text)
    {
        header.text = text;
    }

    public void UpdateSlotSelection(AbstractEquipmentItem equipmentItem)
    {
        slotIcon.texture = equipmentItem.IconTexture;
        header.text = equipmentItem.ItemName;
    }
}
