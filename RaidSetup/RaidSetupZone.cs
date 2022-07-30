using UnityEngine;

public class RaidSetupZone : MonoBehaviour
{
    public ShipSpawnPositionAndRotation ShipSpawnPositionAndRotation;

    private void Awake()
    {
        ShipSpawnPositionAndRotation = gameObject.GetComponentInChildren<ShipSpawnPositionAndRotation>();
    }
}
