using System;

public enum ResourceUpdateType
{
    Additive,
    Replace
}

[Serializable]
public class ResourceData
{
    public int Wood;
    public int Stone;
    public int Iron;
    public int Leather;
    public int Gold;
}
