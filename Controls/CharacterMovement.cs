using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody rigidbody;

    public float speed = 3.0f;

    private void Start()
    {
        rigidbody = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        var direction = rigidbody.rotation * movement;

        rigidbody.MovePosition(rigidbody.position + direction * 20.0f * Time.fixedDeltaTime);
    }
}
