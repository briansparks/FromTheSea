public class DockLabelView : InteractableLabelView
{
    public void UpdateText(string message)
    {
        SetLabelText(message);
    }

    public void UpdateEventToEmit(string evnt)
    {
        eventToEmitForInteraction = evnt;
    }
}
