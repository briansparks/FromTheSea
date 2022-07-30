public class PlayerController
{
    private readonly IPlayerView playerView;

    public PlayerController(IPlayerView argPlayerView)
    {
        playerView = argPlayerView;
    }

    // TODO: When exiting the ship, you'll need to find a exit ship point that isn't occupied by other characters and move it it
    // This should be event based and not have any logic in the view besides triggering the initial event
    // This controller should move the player view to the exit point on event trigger
    // The CharacterOrchestrator should be listending for the event
    // This should be instantiated with the player view (on game load)
    // For testing, you can use the find all objects
}
