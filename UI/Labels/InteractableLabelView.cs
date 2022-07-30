using UnityEngine;

public class InteractableLabelView : AbstractLabelView
{
    public string EventToEmitForInteraction;
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
            EventManager.TriggerEvent(EventToEmitForInteraction);
            Debug.Log($"Firing {EventToEmitForInteraction} event");
        }
    }
}
