using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameObjectExtensions
{
    public static Component GetComponentInParentTraverse<T>(this GameObject gameObject) where T : Component
    {
        Component component = null;
        var gameObj = gameObject.transform.parent;

        while (gameObj != null)
        {
            component = gameObj.GetComponent<T>();

            if (component != null)
            {
                break;
            }

            gameObj = gameObj.transform.parent;
        }

        return component;
    }

    public static IEnumerable<T> GetComponentsInChildrenOfLayer<T>(this GameObject gameObject, int layer) where T : Component
    {
        var allComponentsInChildren = gameObject.GetComponentsInChildren<T>();
        return allComponentsInChildren.Where(c => c.gameObject.layer == layer);
    }

    public static bool ReachedDestinationTarget(this GameObject gameObject, Vector3 target, float distanceBuffer)
    {
        var result = false;

        if (Vector3.Distance(gameObject.transform.position, target) < distanceBuffer)
            result = true;

        return result;
    }
}
