using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour, IManager
{
    public GameDataManager GameDataManager;
    public CharacterManager CharacterManager;
    public BoatManager BoatManager;
    public RaidSetupZone RaidSetupZone;

    public string PathToBoatPrefabs = "Boats/";

    public Vector3 FaeringSpawnPosition;
    public Vector3 KarviSpawnPosition;
    public Vector3 DrakkarSpawnPosition;

    private int currentInGameWeek;
    public void Initialize()
    {
        var currentSave = GameDataManager.GetCurrentlyLoadedSave();
        currentInGameWeek = currentSave.CurrentInGameWeek;

        SpawnActiveBoatInRaidZone();
    }
    public void HandleRaidStart(RaidLocationDto raidLocation)
    {
        var currentSave = GameDataManager.GetCurrentlyLoadedSave();

        var activeRaidCharacters = CharacterManager.GetActiveRaidCharacters();

        var activeRaid = new ActiveRaidData()
        {
            SceneName = raidLocation.SceneName,
            LocationName = raidLocation.LocationName,
            TroopAssignments = activeRaidCharacters.ToList()
        };

        var gameToSave = new SavedGame()
        {
            Name = currentSave.Name,
            DateTime = DateTime.Now,
            CurrentInGameWeek = GetCurrentInGameWeek(),
            AvailableTroops = CharacterManager.GetAvailableTroopData(),
            ActiveBoatId = BoatManager.GetActiveBoat().Id,
            AvailableBoats = BoatManager.GetAvailableBoatsData().ToList(),
            ActiveRaid = activeRaid
        };

        GameDataManager.TrySaveGame(gameToSave, "test");
        StartCoroutine(LoadSceneAsync(activeRaid.SceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneToLoad)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private void SpawnActiveBoatInRaidZone()
    {
        var raidSetupZone = GameObject.FindObjectOfType<RaidSetupZone>();
        var activeBoat = BoatManager.GetActiveBoat();

        var fullPath = PathToBoatPrefabs + activeBoat.PrefabName;
        var boatPrefab = Resources.Load<GameObject>(fullPath);

        BoatManager.TrySpawnBoat(boatPrefab, FaeringSpawnPosition, raidSetupZone.transform, out var boatView);
        boatView.transform.rotation = raidSetupZone.ShipSpawnPositionAndRotation.transform.rotation;
    }

    public int GetCurrentInGameWeek()
    {
        return currentInGameWeek;
    }

    public void IncrementCurrentInGameWeek()
    {
        currentInGameWeek += 1;
    }
}
