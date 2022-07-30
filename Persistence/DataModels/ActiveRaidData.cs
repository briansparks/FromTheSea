using System;
using System.Collections.Generic;

[Serializable]
public class ActiveRaidData
{
    public string SceneName;
    public string LocationName;
    public List<TroopAssignmentData> TroopAssignments;
}
