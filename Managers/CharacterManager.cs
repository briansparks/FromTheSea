using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterManager : MonoBehaviour, IManager
{
    public GameDataManager GameDataManager;
    public string PathToCharacterPrefabs = "Characters/Prefabs";

    private List<INPCController> spawnedCharacterControllers;

    // This will maintain the list of troops from the save file, as well as any that have been added since loaded
    private List<CharacterData> availableTroopData;

    private List<TroopAssignmentData> activeRaidTroopAssignments;
    public void Initialize()
    {
        spawnedCharacterControllers = new List<INPCController>();
        activeRaidTroopAssignments = new List<TroopAssignmentData>();

        var savedGame = GameDataManager.GetCurrentlyLoadedSave();

        if (savedGame != null)
        {
            InitializeAvailableTroopData(savedGame);
        }
        else
        {
            Debug.LogError($"Unable to initialize available troops since the current save failed to load", this);
        }
    }

    private void InitializeAvailableTroopData(SavedGame savedGame)
    {
        availableTroopData = new List<CharacterData>();
        availableTroopData.AddRange(savedGame.AvailableTroops);
    }

    public List<CharacterData> GetAvailableTroopData()
    {
        return availableTroopData;
    }

    public bool TryGetCharacterDataById(Guid troopId, out CharacterData characterData)
    {
        try
        {
            characterData = availableTroopData.First(x => x.Id == troopId);
            return true;
        }
        catch (Exception ex)
        {
            characterData = null;
            Debug.LogError($"Failed to find character {troopId} in available troop data! {ex}", this);
            return false;
        }
    }
    public bool TryInstantiateCharacter(CharacterSpawnRequest characterSpawnRequest, out INPCController npcController)
    {
        try
        {
            var characterFactory = new CharacterFactory();

            var characterPrefab = Resources.Load<GameObject>($"{PathToCharacterPrefabs}/{characterSpawnRequest.CharacterData.PrefabName}");

            npcController = characterFactory.CreateNPC(characterSpawnRequest.CharacterData,
                characterSpawnRequest.Position, 
                characterSpawnRequest.Rotation, 
                characterPrefab, 
                characterSpawnRequest.Parent
            );
           
            spawnedCharacterControllers.Add(npcController);

            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to create character: ${characterSpawnRequest.CharacterData.Id}! {ex}");
            npcController = null;
            return false;
        }
    }

    public bool TryDestroyCharacter(Guid id)
    {
        try
        {
            var npcController = spawnedCharacterControllers.First(x => x.Model.Id == id);

            Destroy(npcController.View.Instance);
            spawnedCharacterControllers.Remove(npcController);

            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to destroy character {id}! {ex}", this);
            return false;
        }
    }

    public void MoveCharacterToPosition(Vector3 vector3, GameObject gameObject)
    {
        gameObject.transform.position = vector3;
    }
    public void AddActiveRaidTroopAssignment(Guid characterId, Guid seatId)
    {
        var troopAssignment = new TroopAssignmentData() { CharacterId = characterId, SeatId = seatId };
        activeRaidTroopAssignments.Add(troopAssignment);
    }

    public void ResetActiveRaidCharacters()
    {
        activeRaidTroopAssignments.Clear();
    }

    public IEnumerable<TroopAssignmentData> GetActiveRaidCharacters()
    {
        return activeRaidTroopAssignments;
    }
}
