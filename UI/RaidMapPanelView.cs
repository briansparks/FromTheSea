using UnityEngine;
using UnityEngine.UI;

public class RaidMapPanelView : MonoBehaviour
{
    private RaidLocation[] raidLocations { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        raidLocations = gameObject.GetComponentsInChildren<RaidLocation>();

        foreach (var raidLocation in raidLocations)
        {
            var locationButton = raidLocation.GetComponentInChildren<Button>();

            if (locationButton != null)
            {
                locationButton.onClick.AddListener(() => OnRaidLocationClick(raidLocation));
            }
        }
    }

    public void OnRaidLocationClick(RaidLocation selectedLocation)
    {
        foreach (var raidLocation in raidLocations)
        {
            raidLocation.Dismiss();
        }

        selectedLocation.Display();
    }

    public void OnBackButtonClick()
    {
        gameObject.SetActive(false);
    }
}
