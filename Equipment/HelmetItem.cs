using UnityEngine;

public class HelmetItem : ArmorItem
{
    [Header("Item Placement Values")]
    public Vector3 PlacedPosition;
    public Vector3 PlacedRotation;
    public Vector3 PlacedScale;

    public override void AttachToRig(GameObject equipmentItem, GameObject targetObject)
    {
        var hair = targetObject.GetComponentInChildren<Hair>();

        if (hair != null)
        {
            hair.gameObject.SetActive(false);
        }

        equipmentItem.transform.SetParent(targetObject.transform);

        equipmentItem.transform.localPosition = PlacedPosition;
        equipmentItem.transform.localEulerAngles = PlacedRotation;
        equipmentItem.transform.localScale = PlacedScale;
    }
}
