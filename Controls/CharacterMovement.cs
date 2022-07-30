using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody rb;

    public float speed = 3.0f;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        var movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        var direction = rb.rotation * movement;

        rb.MovePosition(rb.position + direction * 20.0f * Time.fixedDeltaTime);
    }
}
