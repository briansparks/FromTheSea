using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private ThirdPersonCameraLocation thirdPersonCameraPosition;

    private CameraLocation cameraLocation;
    private HeadBone headBone;

    private bool thirdPersonViewEnabled;

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {    
        var player = GameObject.FindGameObjectWithTag("Player");
        cameraLocation = player.GetComponentInChildren<CameraLocation>();
        thirdPersonCameraPosition = player.GetComponentInChildren<ThirdPersonCameraLocation>();
        gameObject.transform.SetParent(cameraLocation.transform);
        UpdateCameraLocation();
    }

    public void UpdateCameraLocation()
    {
        if (!thirdPersonViewEnabled)
        {
            gameObject.transform.position = cameraLocation.transform.position;
            gameObject.transform.rotation = cameraLocation.transform.rotation;
        }
        else
        {
            gameObject.transform.position = thirdPersonCameraPosition.gameObject.transform.position;
            gameObject.transform.rotation = thirdPersonCameraPosition.gameObject.transform.rotation;
        }
    }

    private void Update()
    {
        UpdateCameraLocation();
    }

    public void UpdateCameraRotation(Quaternion xQuaternion, Quaternion yQuaternion)
    {
        cameraLocation.transform.localRotation = yQuaternion;
    }

    public void ToggleThirdPersonView()
    {
        thirdPersonViewEnabled = !thirdPersonViewEnabled;
    }
}
