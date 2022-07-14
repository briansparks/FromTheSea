using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameDataManager GameDataManager;
    public string PathToCharacterPrefabs = "Characters/Prefabs";

    private List<INPCController> spawnedCharacterControllers;

    // This will maintain the list of troops from the save file, as well as any that have been added since loaded
    private List<CharacterData> availableTroopData;
    void Start()
    {
        spawnedCharacterControllers = new List<INPCController>();

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
}
