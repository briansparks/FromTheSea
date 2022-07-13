using UnityEngine;
using UnityEngine.Events;

public class CharacterOrchestrator : MonoBehaviour
{
    public CharacterManager CharacterManager;
    void Start()
    {
        var spawnCharacterAction = new UnityAction<CharacterSpawnRequest>((spawnRequest) => HandleCharacterSpawnRequest(spawnRequest));
        EventManager.StartListening("SpawnCharacter", spawnCharacterAction);
    }

    private void HandleCharacterSpawnRequest(CharacterSpawnRequest spawnRequest)
    {
        var successfullySpawnCharacter = CharacterManager.TryInstantiateCharacter(spawnRequest, out var npcController);

        if (successfullySpawnCharacter)
        {
            if (spawnRequest.AssignedSeat != null)
            {
                npcController.AssignSeat(spawnRequest.AssignedSeat);
                npcController.UpdateCurrentState(NPCEnums.CurrentState.Sitting);
            }
        }
    }
}
