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

    [SerializeField]
    private RawImage currentlyEquippedIcon;

    public EquipmentSlotType EquipmentSlotType;

    public void UpdateSlotSelection(AbstractEquipmentItem equipmentItem)
    {
        slotIcon.texture = equipmentItem.IconTexture;
        header.text = equipmentItem.ItemName;
    }

    public void SetCurrentlyEquippedIconState(bool active)
    {
        currentlyEquippedIcon.gameObject.SetActive(active);
    }
}
