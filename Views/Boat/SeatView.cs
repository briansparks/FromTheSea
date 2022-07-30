using System;
using UnityEngine;

public class SeatView : MonoBehaviour
{
    [SerializeField]
    private string id_value;

    public int Priority;
    public bool IsOccupied { get; set; }
    public Guid GetId()
    {
        return Guid.Parse(id_value);
    }
    public Vector3 GetCharacterSpawnPosition()
    {
        return gameObject.GetComponentInChildren<CharacterSpawnPosition>().transform.position;
    }

    public Quaternion GetCharacterSpawnRotation()
    {
        return gameObject.GetComponentInChildren<CharacterSpawnPosition>().transform.rotation;
    }
}
