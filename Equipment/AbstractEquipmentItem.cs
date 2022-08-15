using UnityEngine;

public abstract class AbstractEquipmentItem : MonoBehaviour
{
    public string ItemName;
    public GameObject Prefab;
    public Texture IconTexture;
    public EquipmentSlotType EquipmentSlotType;

    [Header("Resource Costs")]
    public int IronCost;
    public int WoodCost;
    public int LeatherCost;
}
