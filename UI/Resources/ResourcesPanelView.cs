using TMPro;
using UnityEngine;

public class ResourcesPanelView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI wood;

    [SerializeField]
    private TextMeshProUGUI stone;

    [SerializeField]
    private TextMeshProUGUI iron;

    [SerializeField]
    private TextMeshProUGUI leather;

    [SerializeField]
    private TextMeshProUGUI gold;

    public void UpdateResourceCounts(ResourceData resourceData, ResourceUpdateType updateType)
    {
        if (updateType == ResourceUpdateType.Additive)
        {
            wood.text = (resourceData.Wood + int.Parse(wood.text)).ToString();
            stone.text = (resourceData.Stone + int.Parse(stone.text)).ToString();
            iron.text = (resourceData.Iron + int.Parse(iron.text)).ToString();
            leather.text = (resourceData.Leather + int.Parse(leather.text)).ToString();
            gold.text = (resourceData.Gold + int.Parse(gold.text)).ToString();
        }
        else
        {
            wood.text = resourceData.Wood.ToString();
            stone.text = resourceData.Stone.ToString();
            iron.text = resourceData.Iron.ToString();
            leather.text = resourceData.Leather.ToString();
            gold.text = resourceData.Gold.ToString();
        }
    }

    public ResourceData GetCurrentResources()
    {
        var resourceData = new ResourceData();

        resourceData.Wood = int.Parse(wood.text);
        resourceData.Iron = int.Parse(iron.text);
        resourceData.Stone = int.Parse(stone.text);
        resourceData.Leather = int.Parse(leather.text);
        resourceData.Gold = int.Parse(gold.text);

        return resourceData;
    }
}
