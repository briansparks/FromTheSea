using System;
using UnityEngine;
using UnityEngine.AI;

public interface ICharacterView
{
    Guid Id { get; set; }
    void SitDownOnSeat();
    GameObject AssignedSeat { get; set; }
    GameObject Instance { get; set; }
}
public class AbstractCharacterView : MonoBehaviour, ICharacterView
{
    public Guid Id { get; set; }
    public GameObject AssignedSeat { get; set; }
    public GameObject Instance { get; set; }

    protected Rigidbody rb;
    protected Animator animator;
    protected NavMeshAgent agent;

    void Awake()
    {
        Instance = gameObject;
        animator = gameObject.GetComponentInParent<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();

        rb = gameObject.GetComponent<Rigidbody>();

        DisableRagdoll();
    }

    public void SitDownOnSeat()
    {
        if (AssignedSeat != null)
        {
            gameObject.transform.position = AssignedSeat.transform.position;

            //gameObject.AddComponent<FixedJoint>();
            //var fixedJoin = gameObject.GetComponent<FixedJoint>();

            //fixedJoin.connectedBody = AssignedSeat.gameObject.GetComponent<Rigidbody>();
            //fixedJoin.breakForce = float.PositiveInfinity;

            rb.freezeRotation = true;

            animator.SetTrigger("SitDown");
        }
        else
        {
            Debug.LogWarning($"Character: {Id} does not have an assigned seat.  Unable to sit down.");
        }
    }

    public void Row()
    {

    }

    private void DisableRagdoll()
    {
        DisableRagdollColliders();
        SetRagdollCollidersToKinematic();
    }

    private void DisableRagdollColliders()
    {
        var colliders = gameObject.GetComponentsInChildrenOfLayer<Collider>(Constants.RAGDOLL_LAYER);

        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
    }

    private void SetRagdollCollidersToKinematic()
    {
        var rigidBodies = gameObject.GetComponentsInChildrenOfLayer<Rigidbody>(Constants.RAGDOLL_LAYER);

        foreach (var rigidBody in rigidBodies)
        {
            rigidBody.isKinematic = true;
        }
    }
}
