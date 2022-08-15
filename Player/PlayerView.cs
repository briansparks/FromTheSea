using UnityEngine;
public interface IPlayerView : ICharacterView
{
    bool IsAimingRangedWeapon { get; set; }
}
public class PlayerView : AbstractCharacterView, IPlayerView
{
    public GameObject MainMeleeWeapon;
    public GameObject Shield;
    public BowView BowView;

    public GameObject ArrowPrefab;
    public Vector3 ArrowPosition;
    public Vector3 ArrowRotation;
    public Vector3 ArrowScale;

    private ArrowView drawnArrow;
    public bool IsAimingRangedWeapon { get; set; }
    public GameObject DialogTarget { get; set; }
    public CameraFollow CameraFollow { get; set; }

    private RightHand rightHand;
    private StringBoneParentLocation stringBoneParentLocation;

    // Use this for initialization
    void Start()
    {
        rightHand = gameObject.GetComponentInChildren<RightHand>();
        stringBoneParentLocation = gameObject.GetComponentInChildren<StringBoneParentLocation>();

        if (MainMeleeWeapon != null)
            MainMeleeWeapon.SetActive(false);

        BowView = gameObject.GetComponentInChildren<BowView>();

        Cursor.lockState = CursorLockMode.Locked;

        CameraFollow = gameObject.GetComponentInChildren<CameraFollow>();
    }


    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            EventManager.TriggerEvent("EnterMouseMode");
        }

        if (Input.GetKeyUp("escape"))
        {
            EventManager.TriggerEvent("ExitMouseMode");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EventManager.TriggerEvent("TogglePauseMenu");
        }

        //TODO: need to make sure none of wasd are still down
        if (Input.GetKeyUp("w") || Input.GetKeyUp("a") || Input.GetKeyUp("s") || Input.GetKeyUp("d"))
        {
            animator.SetBool("IsWalking", false);
        }

        if (Input.GetKeyDown("w") || Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("d"))
        {
            animator.SetBool("IsWalking", true);
        }

        // left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("IsAiming", true);
            
            IsAimingRangedWeapon = true;

            if (BowView != null)
            {
                BowView.DrawBow();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            drawnArrow.transform.SetParent(null);

            BowView.ReleaseArrow(drawnArrow);
            animator.SetTrigger("ReleaseArrow");
            animator.SetBool("IsAiming", false);

            BowView.ResetStringBoneParent();
        }

        // left mouse button
        if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("AimCancelled");
            animator.SetBool("IsAiming", false);
            BowView.ResetStringBoneParent();
            BowView.CancelDraw();
            IsAimingRangedWeapon = false;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            CameraFollow.ToggleThirdPersonView();
        }
    }

    #region animatoration Events
    public void ArrowLoaded()
    {
        BowView.ParentStringBoneToObject(stringBoneParentLocation.gameObject);
    }

    public void GrabArrow()
    {
        var arrowGameObj = GameObject.Instantiate(ArrowPrefab, stringBoneParentLocation.transform);
        drawnArrow = arrowGameObj.GetComponent<ArrowView>();

        drawnArrow.transform.localPosition = ArrowPosition;
        drawnArrow.transform.localEulerAngles = ArrowRotation;
        drawnArrow.transform.localScale = ArrowScale;
    }

    public void FinishShot()
    {
        IsAimingRangedWeapon = false;
    }
    #endregion
}
