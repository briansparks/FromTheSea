using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RaidLocation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RawImage locationImage;
    public GameObject raidDetailsPanel;
    public bool IsSelected;
    public string SceneToLoad;

    public HomeSceneSettings Settings;
    public void OnStartRaidButtonClick()
    {
        OnStartRaidClick();
    }

    public void OnCloseButtonClick()
    {
        Dismiss();
    }

    public void Dismiss()
    {
        IsSelected = false;
        locationImage.color = Settings.RAID_LOCATION_DEFAULT_COLOR;
        raidDetailsPanel.SetActive(false);
    }

    public void Display()
    {
        IsSelected = true;
        locationImage.color = Settings.RAID_LOCATION_ACTIVE_COLOR;
        raidDetailsPanel.SetActive(true);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        locationImage.color = Settings.RAID_LOCATION_ACTIVE_COLOR;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!IsSelected)
        {
            locationImage.color = Settings.RAID_LOCATION_DEFAULT_COLOR;
        }
    }

    private void OnStartRaidClick()
    {
        EventManager.TriggerEvent("StartRaid", SceneToLoad);
    }
}
