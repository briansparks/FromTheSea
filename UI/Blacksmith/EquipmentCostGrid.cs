using TMPro;
using UnityEngine;
using static ResourceEnums;

public class EquipmentCostGrid : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI woodCostLabel;

    [SerializeField]
    private TextMeshProUGUI ironCostLabel;

    [SerializeField]
    private TextMeshProUGUI leatherCostLabel;

    public void UpdateResourceLabel(int cost, ResourceType resourceType, Color textColor)
    {
        switch (resourceType)
        {
            case ResourceType.Wood:
                UpdateLabel(cost, woodCostLabel, textColor);
                break;
            case ResourceType.Iron:
                UpdateLabel(cost, ironCostLabel, textColor);
                break;
            case ResourceType.Leather:
                UpdateLabel(cost, leatherCostLabel, textColor);
                break;
            default:
                Debug.LogError($"Unsupported resource type for cost label: {resourceType.ToString()}!", this);
                break;
        }
    }

    public int GetCurrentValueByResourceType(ResourceType resourceType)
    {
        switch (resourceType)
        {
            case ResourceType.Wood:
                return int.Parse(GetValueForLabel(woodCostLabel));
            case ResourceType.Iron:
                return int.Parse(GetValueForLabel(ironCostLabel));
            case ResourceType.Leather:
                return int.Parse(GetValueForLabel(leatherCostLabel));
            default:
                Debug.LogError($"Unsupported resource type for cost label: {resourceType.ToString()}!", this);
                return 0;
        }
    }

    private string GetValueForLabel(TextMeshProUGUI label)
    {
        return label.text;
    }
    private void UpdateLabel(int cost, TextMeshProUGUI label, Color color)
    {
        label.color = color;
        label.text = cost.ToString();
    }
}
