using System.Linq;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public float Speed;

    private BoatPath boatPath;

    private BoatPathNode[] boatPathNodes;
    private BoatPathNode startingNode;
    private BoatPathNode endingNode;
    private BoatPathNode dockNode;

    private BoatPathNode targetNode;

    // Start is called before the first frame update
    void Start()
    {
        boatPath = GameObject.FindObjectOfType<BoatPath>();

        boatPathNodes = boatPath.GetPathNodes();
        startingNode = boatPathNodes.First(n => n.BoatPathType == BoatPathType.Start);
        endingNode = boatPathNodes.First(n => n.BoatPathType == BoatPathType.End);
        dockNode = boatPathNodes.First(n => n.BoatPathType == BoatPathType.Dock);
        targetNode = startingNode;
    }

    private void Update()
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

    private void MoveToPathNode(BoatPathNode targetNode)
    {
        var step = Speed * Time.deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(transform.position, targetNode.transform.position, step);
    }

    public static bool ReachedDestinationTarget(Vector3 currentPosition, Vector3 target, float distanceBuffer)
    {
        var result = false;

        if (Vector3.Distance(currentPosition, target) < distanceBuffer)
            result = true;

        return result;
    }

    private void LookAtTarget(Vector3 target, float rotationSpeed)
    {
        Vector3 relativePos = target - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    }
}
