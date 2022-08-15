using UnityEngine;

public class LootCartPathNode : MonoBehaviour
{
    public LootCartPathType PathType;
    public LootCartPathNode NextNode;
}

public enum LootCartPathType
{
    Start,
    Normal,
    End
}