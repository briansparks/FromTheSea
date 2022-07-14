using System;
using UnityEngine;

public class RaidSetupZone : MonoBehaviour
{
    public bool TrySpawnBoat(GameObject boatPrefab, Vector3 spawnPosition, out BoatView boatView)
    {
        try
        {
            boatView = GameObject.Instantiate(boatPrefab, gameObject.transform, false).GetComponent<BoatView>();
            boatView.gameObject.transform.localPosition = spawnPosition;

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
