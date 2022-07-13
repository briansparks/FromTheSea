using System;
using UnityEngine;

public interface INPCView
{
    Guid Id { get; set; }
    Action CurrentAction { get; set; }
    void SitDownOnSeat();
    GameObject AssignedSeat { get; set; }
}
public class NPCView : MonoBehaviour, INPCView
{
    public Guid Id { get; set; }
    public Action CurrentAction { get; set; }
    public GameObject AssignedSeat { get; set; }


    private Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        animator = gameObject.GetComponentInParent<Animator>();

        DisableRagdoll();
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentAction != null)
        {
            CurrentAction();
        }
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

    public void SitDownOnSeat()
    {
        if (AssignedSeat != null)
        {
            gameObject.transform.position = AssignedSeat.transform.position;
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
}
