using UnityEngine;
public interface IPlayerView
{
    GameObject Instance { get; set; }
}
public class PlayerView : MonoBehaviour, IPlayerView
{
    public GameObject MainMeleeWeapon;
    public GameObject Shield;
    public GameObject DialogTarget { get; set; }
    public CameraFollow CameraFollow { get; set; }
    public GameObject Instance { get; set; }

    public Animator Anim;

    // Use this for initialization
    void Start()
    {
        Instance = gameObject;

        if (MainMeleeWeapon != null)
            MainMeleeWeapon.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;

        CameraFollow = gameObject.GetComponentInChildren<CameraFollow>();
    }


    void FixedUpdate()
    {
        if (Input.GetKeyDown("escape"))
        {
            EventManager.TriggerEvent("EnterMouseMode");
        }

        if (Input.GetKeyUp("escape"))
        {
            EventManager.TriggerEvent("ExitMouseMode");
        }

        if (Input.GetButton("Horizontal"))
        {
            //Debug.Log("Character is walking.");
            //Anim.SetBool("IsRunning", true);

            //transform.localPosition += Input.GetAxis("Horizontal") * transform.right * Time.deltaTime * speed;
        }

        if (Input.GetButton("Vertical"))
        {
            //Debug.Log("Character is walking.");
            //Anim.SetBool("IsRunning", true);

            //transform.localPosition += Input.GetAxis("Horizontal") * transform.forward * Time.deltaTime * speed;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EventManager.TriggerEvent("TogglePauseMenu");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            EventManager.TriggerEvent("ToggleDebugMenu");
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            EventManager.TriggerEvent("ToggleFormationMenu");
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            EventManager.TriggerEvent("ToggleMovementMenu");
        }

        //TODO: need to make sure none of wasd are still down
        if (Input.GetKeyUp("w") || Input.GetKeyUp("a") || Input.GetKeyUp("s") || Input.GetKeyUp("d"))
        {
            Anim.SetBool("IsWalking", false);
        }

        if (Input.GetKeyDown("w") || Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("d"))
        {
            Anim.SetBool("IsWalking", true);
        }

        // left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            Anim.SetTrigger("IsSwinging");

            var maxComboStage = 2;
            var comboStage = Anim.GetInteger("SwingCombo");
            comboStage++;

            if (comboStage > maxComboStage)
            {
                comboStage = 1;
            }

            if (comboStage == 1)
            {
                Anim.SetTrigger("FirstSwing");
            }
            else if (comboStage == 2)
            {
                Anim.SetTrigger("SecondSwing");
            }

            Anim.SetInteger("SwingCombo", comboStage);
            }

            // right mouse button down
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("Right mouse button clicked.");
                Anim.SetBool("IsBlockingWithShield", true);
            }

            // right mouse button release
            if (Input.GetMouseButtonUp(1))
            {
                Debug.Log("Right mouse button clicked.");
                Anim.SetBool("IsBlockingWithShield", false);
            }

            //if (Input.GetKeyDown("1"))
            //{
            //    Anim.SetBool("IsSheathing", false);
            //    Anim.SetBool("IsDrawingMeleeWeapon", true);
            //}

            if (Input.GetKeyDown("e"))
            {
                Anim.SetBool("IsDrawingMeleeWeapon", false);
                Anim.SetBool("IsSheathing", true);
            }            
        
    }

    public void EnableMeleeWeapon()
    {
        //MainMeleeWeapon.SetActive(true);
    }

    public void DisableMeleeWeapon()
    {
        //MainMeleeWeapon.SetActive(false);
    }
}
