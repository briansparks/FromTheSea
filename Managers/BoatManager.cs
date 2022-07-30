using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoatManager : MonoBehaviour, IManager
{
    private BoatData activeBoat;
    private IEnumerable<BoatData> availableBoatsData;

    public GameDataManager GameDataManager;

    public void Initialize()
    {
        var currentSave = GameDataManager.GetCurrentlyLoadedSave();
        availableBoatsData = currentSave.AvailableBoats;
        activeBoat = availableBoatsData.First(b => b.Id == currentSave.ActiveBoatId);
    }

    public BoatData GetActiveBoat()
    {
        return activeBoat;
    }
    public void UpdateActiveBoatById(Guid boatId)
    {
        var currentSave = GameDataManager.GetCurrentlyLoadedSave();
        var activeBoat = availableBoatsData.First(b => b.Id == boatId);
    }

    public IEnumerable<BoatData> GetAvailableBoatsData()
    {
        return availableBoatsData;
    }
    public bool TrySpawnBoat(GameObject boatPrefab, Vector3 spawnPosition, Transform parent, out BoatView boatView)
    {
        try
        {
            boatView = GameObject.Instantiate(boatPrefab, parent, false).GetComponent<BoatView>();
            boatView.gameObject.transform.localPosition = spawnPosition;

            boatView.Initialize();

            return true;
        }
        catch (Exception ex)
        {
            boatView = null;
            Debug.LogError($"Failed to spawn boat! {ex}", this);
            return false;
        }
    }
}
