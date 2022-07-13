using System;
using UnityEngine;

public class RaidSetupZone : MonoBehaviour
{
    public Vector3 FaeringSpawnPosition;
    public Vector3 KarviSpawnPosition;
    public Vector3 DrakkarSpawnPosition;

    public GameObject FaeringPrefab;

    public bool TrySpawnBoat(out BoatView boatView)
    {
        try
        {
            boatView = GameObject.Instantiate(FaeringPrefab, gameObject.transform, false).GetComponent<BoatView>();
            boatView.gameObject.transform.localPosition = FaeringSpawnPosition;

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
