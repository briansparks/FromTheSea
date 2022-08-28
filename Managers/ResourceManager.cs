using UnityEngine;

public class ResourceManager : MonoBehaviour, IManager
{
    [SerializeField]
    private ResourcesPanelView resourcesPanelView;

    [SerializeField]
    private GameDataManager gameDataManager;

    public void Initialize()
    {
        var currentSave = gameDataManager.GetCurrentlyLoadedSave();
        UpdateAvailableResources(currentSave.AvailableResources, ResourceUpdateType.Replace);
    }

    public void UpdateAvailableResources(ResourceData resourceData, ResourceUpdateType updateType)
    {
        resourcesPanelView.UpdateResourceCounts(resourceData, updateType);       
    }

    public ResourceData GetAvailableResources()
    {
        return resourcesPanelView.GetCurrentResources();
    }
}
