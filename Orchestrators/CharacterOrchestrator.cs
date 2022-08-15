using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class CharacterOrchestrator : MonoBehaviour
{
    public CharacterManager CharacterManager;
    void Start()
    {
        var spawnCharacterAction = new UnityAction<CharacterSpawnRequest>((spawnRequest) => HandleCharacterSpawnRequest(spawnRequest));
        EventManager.StartListening("SpawnCharacter", spawnCharacterAction);

        var spawnCharacterPreviewAction = new UnityAction<CharacterSpawnRequest>((spawnRequest) => HandleCharacterPreviewSpawnRequest(spawnRequest));
        EventManager.StartListening("SpawnCharacterPreview", spawnCharacterPreviewAction);

        var spawnRaidCharacterAction = new UnityAction<ActiveRaidCharacterSpawnRequest>((spawnRequest) => HandleActiveRaidCharacterSpawnRequest(spawnRequest));
        EventManager.StartListening("SpawnActiveRaidCharacter", spawnRaidCharacterAction);

        var destroyCharacterAction = new UnityAction<Guid>((id) => HandleDestroyCharacterRequest(id));
        EventManager.StartListening("DestroyCharacter", destroyCharacterAction);
    }

    private void HandleDestroyCharacterRequest(Guid id)
    {
        CharacterManager.TryDestroyCharacter(id);
    }
    private void HandleCharacterSpawnRequest(CharacterSpawnRequest spawnRequest)
    {
        CharacterManager.TryInstantiateCharacter(spawnRequest, out var npcController);
    }

    private void HandleCharacterPreviewSpawnRequest(CharacterSpawnRequest spawnRequest)
    {
        var success = CharacterManager.TryInstantiateCharacter(spawnRequest, out var npcController);

        if (success)
        {
            npcController.View.DisableNavigation();
            npcController.View.DisablePhysics();
        }
    }
    private void HandleActiveRaidCharacterSpawnRequest(ActiveRaidCharacterSpawnRequest spawnRequest)
    {
        var successfullySpawnedCharacter = CharacterManager.TryInstantiateCharacter(spawnRequest, out var npcController);

        if (successfullySpawnedCharacter)
        {
            npcController.AssignSeat(spawnRequest.AssignedSeat);
            npcController.UpdateCurrentState(NPCEnums.CurrentState.Sitting);

            var seatView = spawnRequest.AssignedSeat.GetComponent<SeatView>();
            CharacterManager.AddActiveRaidTroopAssignment(spawnRequest.CharacterData.Id, seatView.GetId());
        }
    }
}
