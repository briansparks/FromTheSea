using System.Collections.Generic;
using UnityEngine;

public interface ISaveDataRepository
{
    public SavedGame GetSavedGameByName(string name);
    public IEnumerable<SavedGame> GetAllSavedGames();
}
