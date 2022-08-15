using UnityEngine;

public class InteractableLabelView : AbstractLabelView
{
    [SerializeField]
    protected string eventToEmitForInteraction;
    protected override float CULLING_DISTANCE { get; } = 5.0f;

    private void Update()
    {
        if (IsLabelInRange)
        {
            RotateLabelToFacePlayer();
            InteractWhenKeyPressed();
        }
    }

    public void InteractWhenKeyPressed()
    {
        if (Input.GetKeyDown("e"))
        {
            EventManager.TriggerEvent(eventToEmitForInteraction);
            Debug.Log($"Firing {eventToEmitForInteraction} event");
        }
    }
}
