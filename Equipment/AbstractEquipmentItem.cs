using System.Collections.Generic;
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

    public bool IsItemFree => IronCost == 0 && WoodCost == 0 && LeatherCost == 0;
    public virtual void AttachToRig(GameObject equipmentItem, GameObject targetObject)
    {
        SkinnedMeshRenderer equipmentItemRenderer = equipmentItem.GetComponentInChildren<SkinnedMeshRenderer>();

        Dictionary<string, Transform> boneMap = new Dictionary<string, Transform>();
        SkinnedMeshRenderer targetRenderer = targetObject.GetComponent<SkinnedMeshRenderer>();

        equipmentItemRenderer.transform.SetParent(targetObject.transform.parent);
        equipmentItemRenderer.rootBone = targetRenderer.rootBone;

        foreach (Transform bone in targetRenderer.bones)
        {
            if (bone != null)
            {
                boneMap[bone.gameObject.name] = bone;
            }
        }

        Transform[] newBones = new Transform[equipmentItemRenderer.bones.Length];

        for (int i = 0; i < equipmentItemRenderer.bones.Length; ++i)
        {
            GameObject bone = equipmentItemRenderer.bones[i].gameObject;

            if (!boneMap.TryGetValue(bone.name, out newBones[i]))
            {
                Debug.Log("Unable to map bone \"" + bone.name + "\" to target skeleton.");
            }
        }

        equipmentItemRenderer.bones = newBones;
        GameObject.Destroy(targetObject);
    }
}
