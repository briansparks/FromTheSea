using System;
using static NPCEnums;

public interface INPCModel
{
    Guid Id { get; }
    CurrentState CurrentState { get; set; }
}
public class NPCModel : INPCModel
{
    public CurrentState CurrentState { get; set; }
    public Guid Id { get; }

    public NPCModel(Guid id)
    {
        Id = id;
    }
}
