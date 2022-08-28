using System;
using System.Collections.Generic;

[Serializable]
public class CharacterData 
{
    public Guid Id;
    public string Name;
    public int HP;
    public string PrefabName;
    public List<EquipmentLoadoutItem> EquipmentLoadoutItems;
}
