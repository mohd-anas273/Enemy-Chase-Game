using UnityEngine.InputSystem;

public class BirdController : BaseController
{
    public override void PlayerJump_Action_Performed(InputAction.CallbackContext ctx)
    {
        base.PlayerJump_Action_Performed(ctx);
        audioController.PlayHopAudio();
    }
}
