using System;
using System.Collections.Generic;
using UnityEngine;

public interface IGameDataManager
{
    bool TryGetSavedGameByName(string name, out SavedGame savedGame);
    bool TryGetAllSavedGames(out IEnumerable<SavedGame> savedGames);
}

public class GameDataManager : MonoBehaviour, IGameDataManager
{
    public GameObject SaveDataRepositoryGameObject;

    private ISaveDataRepository saveDataRepository;

    private SavedGame currentyLoadedSave;

    void Awake()
    {
        saveDataRepository = SaveDataRepositoryGameObject.GetComponent<ISaveDataRepository>();

        //TODO: using this to test, save should be handled from title screen
        TryGetSavedGameByName("Save1", out var savedGame);

        currentyLoadedSave = savedGame;
    }
    public bool TryGetAllSavedGames(out IEnumerable<SavedGame> savedGames)
    {
        try
        {
            savedGames = saveDataRepository.GetAllSavedGames();
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"An exception has occurred while trying to fetch saved games: {ex}", this);
            savedGames = new List<SavedGame>();
            return false;
        }
    }

    public bool TryGetSavedGameByName(string name, out SavedGame savedGame)
    {
        try
        {
            savedGame = saveDataRepository.GetSavedGameByName(name);
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"An exception has occurred while trying to fetch saved game: {ex}", this);
            savedGame = null;
            return false;
        }
    }

    public SavedGame GetCurrentlyLoadedSave()
    {
        return currentyLoadedSave;
    }
}
