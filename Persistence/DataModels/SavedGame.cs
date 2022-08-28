using System;
using System.Collections.Generic;

[Serializable]
public class SavedGame
{
    public string Name;
    public DateTime DateTime;
    public int CurrentInGameWeek;
    public ResourceData AvailableResources;
    public List<CharacterData> AvailableTroops;
    public Guid ActiveBoatId;
    public List<BoatData> AvailableBoats;
    public List<string> ArmoryItems;
    public ActiveRaidData ActiveRaid;
}
