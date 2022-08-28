using UnityEngine;
using UnityEngine.UI;

public class TroopsStatsPanelView : MonoBehaviour
{
    [SerializeField]
    private Slider ARSlider;

    [SerializeField]
    private Slider DRSlider;

    public void UpdateAttackRating(int arVal)
    {
        ARSlider.value = arVal;
    }

    public void UpdateDefenseRating(int drVal)
    {
        DRSlider.value = drVal;
    }
}
