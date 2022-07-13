using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoatView : MonoBehaviour
{
    public float Speed;

    private BoatPath boatPath;

    private BoatPathNode[] boatPathNodes;
    private BoatPathNode startingNode;
    private BoatPathNode endingNode;
    private BoatPathNode dockNode;

    private BoatPathNode targetNode;
    private List<SeatView> seats;

    // Start is called before the first frame update
    void Start()
    {
        boatPath = GameObject.FindObjectOfType<BoatPath>();

        if (boatPath != null)
        {
            boatPathNodes = boatPath.GetPathNodes();
            startingNode = boatPathNodes.First(n => n.BoatPathType == BoatPathType.Start);
            endingNode = boatPathNodes.First(n => n.BoatPathType == BoatPathType.End);
            dockNode = boatPathNodes.First(n => n.BoatPathType == BoatPathType.Dock);
            targetNode = startingNode;
        }

        seats = gameObject.GetComponentsInChildren<SeatView>().ToList();
    }
    private void Update()
    {
    }
    private void MoveToPathNode(BoatPathNode targetNode)
    {
        var step = Speed * Time.deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(transform.position, targetNode.transform.position, step);
    }

    private void LookAtTarget(Vector3 target, float rotationSpeed)
    {
        Vector3 relativePos = target - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    }

    private void NavigatePath()
    {
        if (ReachedDestinationTarget(gameObject.transform.position, targetNode.transform.position, 1.0f))
        {
            if (targetNode.NextNode != null)
            {
                targetNode = targetNode.NextNode;
            }
        }
        else
        {
            var lookAtPos = targetNode.transform.position;
            lookAtPos.y = gameObject.transform.position.y;

            LookAtTarget(lookAtPos, 1.0f);

            MoveToPathNode(targetNode);
        }
    }
    public static bool ReachedDestinationTarget(Vector3 currentPosition, Vector3 target, float distanceBuffer)
    {
        var result = false;

        if (Vector3.Distance(currentPosition, target) < distanceBuffer)
            result = true;

        return result;
    }

    public bool TryFindNextOpenSeat(out SeatView openSeat)
    {
        var unoccupiedSeats = seats.Where(s => !s.IsOccupied);

        if (unoccupiedSeats.Any())
        {
            openSeat = unoccupiedSeats.ToList().OrderBy(s => s.Priority).First();
            return true;
        }

        openSeat = null;
        return false;
    }
}
