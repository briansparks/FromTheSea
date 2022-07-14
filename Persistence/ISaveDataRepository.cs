using System.Collections.Generic;
using UnityEngine;

public interface ISaveDataRepository
{
    SavedGame GetSavedGameByName(string name);
    IEnumerable<SavedGame> GetAllSavedGames();
    void SaveGame(SavedGame gameToSave, string fileName);
}
