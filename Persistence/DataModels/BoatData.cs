using System;
using System.Collections.Generic;

[Serializable]
public class BoatData
{
    public Guid Id;
    public string Name;
    public int HP;
    public string PrefabName;
    public List<SeatData> Seats;
}
