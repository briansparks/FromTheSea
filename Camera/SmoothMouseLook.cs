using UnityEngine;

[AddComponentMenu("Camera-Control/Smooth Mouse Look")]
public class SmoothMouseLook : MonoBehaviour
{
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    float rotationX = 0F;
    float rotationY = 0F;
    Quaternion originalRotation;
    Vector2 smoothV;
    public float sensitivity = 15.0f;

    private GameObject spine;
    private GameObject character;

    private IPlayerView playerView;

    private CameraFollow cameraFollow;
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player");
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        playerView = character.GetComponent<IPlayerView>();
        spine = character.GetComponentInChildren<SpineBone>().gameObject;

        originalRotation = character.transform.localRotation;
    }

    void LateUpdate()
    {
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        md = Vector2.Scale(md, new Vector2(sensitivity, sensitivity));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1.0f);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1.0f);

        // Read the mouse input axis
        rotationX += Input.GetAxis("Mouse X") * sensitivityX;
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

        rotationX = ClampAngle(rotationX, minimumX, maximumX);
        rotationY = ClampAngle(rotationY, minimumY, maximumY);
        Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);

        Quaternion yQuaternion;

        //if (playerView.IsAimingRangedWeapon)
        //{
            yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.back);
        //}
        //else
        //{
        //    yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
        //}

        var cameraYQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);


        character.transform.localRotation = originalRotation * xQuaternion;
        spine.transform.localRotation = yQuaternion;

        cameraFollow.UpdateCameraRotation(xQuaternion, cameraYQuaternion);
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}