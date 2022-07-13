using UnityEngine;

public class HomeSceneSettings : MonoBehaviour
{
    [Header("Camera Settings")]
    public int DOLLY_TRACK_SPEED;
    public Vector3 MAIN_CAMERA_INITIAL_POSITION;

    [Header("Raid Map Settings")]
    public Color RAID_LOCATION_ACTIVE_COLOR;
    public Color RAID_LOCATION_DEFAULT_COLOR;

    [Header("Orchestrator Settings")]
    public float COROUTINE_DISPLAY_OVERLAY_TIME;
}
