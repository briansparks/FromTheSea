using UnityEngine;
using UnityEngine.AI;

public class LootCartView : MonoBehaviour
{
    private Axle axle;

    private bool isBeingPushed;
    private float wheelRotationSpeed = 15.0f;
    private NavMeshAgent navMeshAgent;

    private bool awaitingNextDestination;

    // Start is called before the first frame update
    void Start()
    {
        axle = gameObject.GetComponentInChildren<Axle>();
        navMeshAgent = gameObject.GetComponentInChildren<NavMeshAgent>();
    }

    void Update()
    {
        if (isBeingPushed)
        {
            HandleMoveTowardsDestination();
            RotateWheels();
        }
    }

    private void RotateWheels()
    {
        axle.transform.Rotate(Vector3.right * (wheelRotationSpeed * Time.deltaTime));
    }

    private void HandleMoveTowardsDestination()
    {
        if (!awaitingNextDestination && Vector3.Distance(gameObject.transform.position, navMeshAgent.destination) < 1.0f)
        {
            Debug.Log("Triggering LootCartReachedDestination");
            awaitingNextDestination = true;
            EventManager.TriggerEvent("LootCartReachedDestination");
        }
    }
    public void OnCartPush(Vector3 targetPos)
    {
        isBeingPushed = true;
        awaitingNextDestination = false;
        navMeshAgent.SetDestination(targetPos);
    }

    public void OnCartStopped()
    {
        isBeingPushed = false;
        navMeshAgent.isStopped = true;
    }
}
