using UnityEngine;

public class BoatPathNode : MonoBehaviour
{
    public BoatPathType BoatPathType;
    public BoatPathNode NextNode;
}

public enum BoatPathType
{
    Normal,
    Start,
    Dock,
    End
}