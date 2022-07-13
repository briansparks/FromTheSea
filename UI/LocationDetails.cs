using TMPro;
using UnityEngine;

public class LocationDetails : MonoBehaviour
{
    public TextMeshProUGUI LocationName;
    public TextMeshProUGUI RiverName;

    public int LocationNameFadeInTime = 5;
    public int LocationNameFadeOutTime = 10;
    public int RiverNameFadeInTime = 5;
    public int RiverNameFadeOutTime = 10;

    // Start is called before the first frame update
    void Start()
    {
        LocationName.CrossFadeAlpha(0, 0, false);
        LocationName.CrossFadeAlpha(1, LocationNameFadeInTime, false);

        RiverName.CrossFadeAlpha(0, 0, false);
        RiverName.CrossFadeAlpha(1, RiverNameFadeInTime, false);

        StartCoroutine(StartFadeInThenOutTimer(LocationName, LocationNameFadeInTime, LocationNameFadeOutTime));
        StartCoroutine(StartFadeInThenOutTimer(RiverName, RiverNameFadeInTime, RiverNameFadeOutTime));
    }
    private System.Collections.IEnumerator StartFadeOutTimer(TextMeshProUGUI textMeshPro, float fadeOutTime)
    {
        int seconds = 0;

        while (seconds < fadeOutTime)
        {
            yield return new WaitForSeconds(1);
            seconds++;
        }

        textMeshPro.text = "";
    }

    private System.Collections.IEnumerator StartFadeInThenOutTimer(TextMeshProUGUI textMeshPro, float fadeInTime, float fadeOutTime)
    {
        int seconds = 0;

        while (seconds < fadeInTime)
        {
            yield return new WaitForSeconds(1);
            seconds++;
        }

        LocationName.CrossFadeAlpha(0.0f, fadeOutTime, false);
        StartCoroutine(StartFadeOutTimer(textMeshPro, fadeOutTime));
    }
}
