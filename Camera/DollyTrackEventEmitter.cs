using Cinemachine;
using UnityEngine;

public class DollyTrackEventEmitter : MonoBehaviour
{
    public string EventToEmit;
    public CinemachineDollyCart DollyCart;

    private float endPositionBuffer = 1.0f;
    private bool pathComplete;

    // Update is called once per frame
    void Update()
    {
        if (!pathComplete && HasReachedEndOfPath())
        {
            pathComplete = true;
            EventManager.TriggerEvent(EventToEmit);
        }
    }

    public bool HasReachedEndOfPath()
    {
        if (DollyCart.m_Position >= (DollyCart.m_Path.PathLength - endPositionBuffer))
        {
            return true;
        }

        return false;
    }

    public void ResetToDefaults()
    {
        enabled = false;
        pathComplete = false;
    }
}
