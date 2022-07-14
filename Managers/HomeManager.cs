using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    public GameDataManager GameDataManager;
    public RaidSetupZone RaidSetupZone;

    public Vector3 FaeringSpawnPosition;
    public Vector3 KarviSpawnPosition;
    public Vector3 DrakkarSpawnPosition;

    public string PathToBoatPrefabs = "Boats/";

    // Start is called before the first frame update
    void Start()
    {
        var currentSave = GameDataManager.GetCurrentlyLoadedSave();
        SpawnBoat(currentSave.ActiveBoat.PrefabName);
    }

    private BoatView SpawnBoat(string prefabName)
    {
        var fullPath = PathToBoatPrefabs + prefabName;
        var boatPrefab = Resources.Load<GameObject>(fullPath);

        if (RaidSetupZone.TrySpawnBoat(boatPrefab, FaeringSpawnPosition, out var boatView))
        {
            return boatView;
        }
        else
        {
            // Display error or try again?
            return null;
        }
    }

    // TODO: need to sync current save with current data and save to file
    public void HandleRaidStart(string sceneToLoad)
    {
        var currentSave = GameDataManager.GetCurrentlyLoadedSave();
        var gameToSave = new SavedGame();

        GameDataManager.TrySaveGame(gameToSave, "test");
        StartCoroutine(LoadSceneAsync(sceneToLoad));
    }

    IEnumerator LoadSceneAsync(string sceneToLoad)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
