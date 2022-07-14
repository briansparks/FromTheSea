using System;
using System.Collections.Generic;

[Serializable]
public class SavedGame
{
    public string Name;
    public DateTime DateTime;
    public int CurrentInGameWeek;
    public List<CharacterData> AvailableTroops;
    public BoatData ActiveBoat;
    public List<BoatData> InactiveBoats;
}
