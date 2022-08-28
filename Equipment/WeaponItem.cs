using UnityEngine;

public class WeaponItem : AbstractEquipmentItem
{
    [Header("Item Placement Values")]
    public Vector3 PlacedPosition;
    public Vector3 PlacedRotation;
    public Vector3 PlacedScale;

    [Header("Item Rating")]
    public int AttackRating;

    public override void AttachToRig(GameObject equipmentItem, GameObject targetObject)
    {
        equipmentItem.transform.SetParent(targetObject.transform);

        equipmentItem.transform.localPosition = PlacedPosition;
        equipmentItem.transform.localEulerAngles = PlacedRotation;
        equipmentItem.transform.localScale = PlacedScale;
    }
}
