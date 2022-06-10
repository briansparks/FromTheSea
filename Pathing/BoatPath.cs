using UnityEngine;

public class BoatPath : MonoBehaviour
{
    public void DrawBoatPath()
    {
        ResetBoatPath();

        var boatPathNodes = GetPathNodes();

        var lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = boatPathNodes.Length;

        for (int i = 0; i < boatPathNodes.Length; i++)
        {
            lineRenderer.SetPosition(i, boatPathNodes[i].transform.position);
        }
    }

    public void ResetBoatPath()
    {
        var lineRenderer = gameObject.GetComponent<LineRenderer>();

        if (lineRenderer != null)
        {
            GameObject.DestroyImmediate(lineRenderer);
        }
    }

    public BoatPathNode[] GetPathNodes()
    {
        return gameObject.GetComponentsInChildren<BoatPathNode>();
    }
}
