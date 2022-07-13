using UnityEngine;

public class SeatView : MonoBehaviour
{
    public int Priority;
    public bool IsOccupied { get; set; }

    private CharacterSpawnPosition characterSpawnPosition;

    private void Start()
    {
        characterSpawnPosition = gameObject.GetComponentInChildren<CharacterSpawnPosition>();
    }

    public Vector3 GetCharacterSpawnPosition()
    {
        return characterSpawnPosition.transform.position;
    }

    public Quaternion GetCharacterSpawnRotation()
    {
        return characterSpawnPosition.transform.rotation;
    }
}
