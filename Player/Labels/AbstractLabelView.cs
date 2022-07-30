using System.Collections;
using TMPro;
using UnityEngine;

public abstract class AbstractLabelView : MonoBehaviour
{
    protected TextMeshPro Label { get; set; }

    protected GameObject PlayerObject { get; set; }

    protected abstract float CULLING_DISTANCE { get; }

    protected bool IsLabelInRange { get; set; }

    void Awake()
    {
        PlayerObject = GameObject.FindGameObjectWithTag(Constants.PLAYER_TAG);
        Label = gameObject.GetComponentInChildren<TextMeshPro>();

        StartCoroutine(PollingDistanceForCulling());
    }

    protected void RotateLabelToFacePlayer()
    {
        RotateYandZAxis(Quaternion.LookRotation(PlayerObject.transform.position - transform.position));
    }

    private bool RenderIfWithinCullingDistance()
    {
        if (PlayerObject != null && Vector3.Distance(PlayerObject.transform.position, gameObject.transform.position) < CULLING_DISTANCE)
        {
            Label.gameObject.SetActive(true);
            return true;
        }

        Label.gameObject.SetActive(false);
        return false;
    }

    protected void SetLabelText(string labelText)
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
            IsLabelInRange = RenderIfWithinCullingDistance();
            yield return new WaitForSeconds(Constants.LABEL_CULLING_POLL_TIME);
        }
    }
}
