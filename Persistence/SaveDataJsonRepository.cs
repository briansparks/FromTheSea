using System.Collections.Generic;

public class SaveDataJsonRepository : AbstractJsonFileRepository<SavedGame>, ISaveDataRepository
{
    public string PathToSavedGames;

    public IEnumerable<SavedGame> GetAllSavedGames()
    {
        return DeserializeJsonFilesAtPath(PathToSavedGames);
    }

    public SavedGame GetSavedGameByName(string name)
    {
        return DeserialzeJsonFileAtPath(PathToSavedGames + $"{name}");
    }

    public void SaveGame(SavedGame gameToSave, string fileName)
    {
        SaveJsonFile(gameToSave, PathToSavedGames, fileName);
    }
}
