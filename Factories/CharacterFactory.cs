using System;
using UnityEngine;

public interface ICharacterFactory
{
    public INPCController CreateNPC(CharacterData characterData, Vector3 position, Quaternion rotation, GameObject prefab, GameObject parent);
}
public class CharacterFactory : ICharacterFactory
{
    public INPCController CreateNPC(CharacterData characterData, Vector3 position, Quaternion rotation, GameObject prefab, GameObject parent)
    {
        var instance = GameObject.Instantiate(prefab, position, rotation);
        var npcView = instance.AddComponent<NPCView>();

        var nameLabelView = instance.GetComponentInChildren<NameLabelView>();
        //nameLabelView.SetLabelText(characterData.Name);
        nameLabelView.enabled = false;

        instance.transform.SetParent(parent.transform);

        var npcModel = new NPCModel(characterData.Id);
        npcView.Id = characterData.Id;

        var npcController = new NPCController(npcModel, npcView);

        return npcController;
    }
}
