using UnityEngine;
using UnityEngine.Events;

public class ResourceOrchestrator : MonoBehaviour
{
    [SerializeField]
    private ResourceManager resourceManager;

    // Start is called before the first frame update
    void Start()
    {
        var updateResourcesAction = new UnityAction<ResourceData>((resourceData) => { HandleUpdateResources(resourceData); });
        EventManager.StartListening("UpdateResources", updateResourcesAction);
    }

    private void HandleUpdateResources(ResourceData resourceData)
    {
        resourceManager.UpdateAvailableResources(resourceData, ResourceUpdateType.Additive);
    }
}
