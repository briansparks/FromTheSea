using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarryRigidBodies : MonoBehaviour
{
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();

    public Vector3 LastPosition;
    Transform _transform;

    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        LastPosition = _transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (rigidbodies.Any())
        {
            for (int i = 0; i < rigidbodies.Count; i++)
            {
                Rigidbody rb = rigidbodies[i];
                Vector3 velocity = (_transform.position - LastPosition);

                if (rb == null)
                {
                    rigidbodies.RemoveAt(i);
                }
                else
                {
                    rb.transform.Translate(velocity, Space.World);
                }
            }
        }

        LastPosition = _transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();

        if (rb != null)
        {
            AddRb(rb);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();

        if (rb != null)
        {
            RemoveRb(rb);
        }
    }
    private void AddRb(Rigidbody rb)
    {
        if (!rigidbodies.Contains(rb))
        {
            rigidbodies.Add(rb);
        }
    }

    private void RemoveRb(Rigidbody rb)
    {
        if (rigidbodies.Contains(rb))
        {
            rigidbodies.Remove(rb);
        }
    }
}
