using System;

public interface INPCView : ICharacterView
{
    Action CurrentAction { get; set; }
    void DisableNavigation();
    void DisablePhysics();
}
public class NPCView : AbstractCharacterView, INPCView
{
    public Action CurrentAction { get; set; }


    // Update is called once per frame
    void Update()
    {
        if (CurrentAction != null)
        {
            CurrentAction();
        }
    }

    public void DisableNavigation()
    {
        agent.enabled = false;
    }

    public void DisablePhysics()
    {
        rb.isKinematic = true;
        rb.useGravity = false;
    }
}
