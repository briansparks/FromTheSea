using UnityEngine;

public class LootCartPath : MonoBehaviour
{
    public void DrawPath()
    {
        ResetPath();

        var pathNodes = GetPathNodes();

        var lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = pathNodes.Length;

        for (int i = 0; i < pathNodes.Length; i++)
        {
            lineRenderer.SetPosition(i, pathNodes[i].transform.position);
        }
    }

    public void ResetPath()
    {
        var lineRenderer = gameObject.GetComponent<LineRenderer>();

        if (lineRenderer != null)
        {
            GameObject.DestroyImmediate(lineRenderer);
        }
    }

    public LootCartPathNode[] GetPathNodes()
    {
        return gameObject.GetComponentsInChildren<LootCartPathNode>();
    }
}
