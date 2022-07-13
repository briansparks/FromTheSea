using UnityEngine;

public class ArrowView : MonoBehaviour
{
    private Rigidbody rb;
    private BoxCollider boxCollider;
    private bool hasCollided;
    private TrailRenderer trailRenderer;

    public AudioSource hitSound;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        boxCollider = gameObject.GetComponent<BoxCollider>();
        trailRenderer = gameObject.GetComponent<TrailRenderer>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!hasCollided)
        {
            hitSound.Play();

            rb.isKinematic = true;
            boxCollider.enabled = false;

            Destroy(gameObject, 30.0f);

            hasCollided = true;
        }
    }

    private void EnableArrowTrail()
    {
        trailRenderer.emitting = true;
    }

    public void Shoot(float releaseStrength)
    {
        EnableArrowTrail();
        rb.isKinematic = false;
        boxCollider.enabled = true;

        rb.AddForce(rb.transform.forward * releaseStrength, ForceMode.VelocityChange);
    }
}
