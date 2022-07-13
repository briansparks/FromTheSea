using System.Collections;
using UnityEngine;

public abstract class AbstractLabelView : MonoBehaviour
{
    public RaidSettings RaidSettings;
    protected TextMesh Label { get; set; }

    protected IPlayerView PlayerCharacter { get; set; }

    protected abstract float CULLING_DISTANCE { get; }

    protected bool IsDisabled { get; set; }
    public void SetDisabledState(bool state)
    {
        IsDisabled = state;
        Label.gameObject.SetActive(!state);
    }

    void Awake()
    {
        PlayerCharacter = GameObject.FindGameObjectWithTag(RaidSettings.PLAYER_TAG)?.GetComponent<IPlayerView>();
        Label = gameObject.GetComponentInChildren<TextMesh>();

        StartCoroutine(PollingDistanceForCulling());
    }

    protected void RotateLabelToFacePlayer()
    {
        RotateYandZAxis(Quaternion.LookRotation(PlayerCharacter.Instance.transform.position - transform.position));
    }

    protected bool RenderIfWithinCullingDistance()
    {
        if (Vector3.Distance(PlayerCharacter.Instance.transform.position, gameObject.transform.parent.transform.position) < CULLING_DISTANCE)
        {
            Label.gameObject.SetActive(true);
            return true;
        }

        Label.gameObject.SetActive(false);
        return false;
    }

    public void SetLabelText(string labelText)
    {
        Label.text = labelText;
    }

    private void RotateYandZAxis(Quaternion rot)
    {
        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    private IEnumerator PollingDistanceForCulling()
    {
        while (true)
        {
            RenderIfWithinCullingDistance();
            yield return new WaitForSeconds(RaidSettings.LABEL_POLLING_CULLING_CHECK_IN_SECONDS);
        }
    }
}
