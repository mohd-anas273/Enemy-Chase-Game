using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class BaseController : MonoBehaviour
{
    
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] protected CharacterAnimatorController animatorController;
    [SerializeField] protected CharacterAudioController audioController;

    [HideInInspector] public BaseInputMode baseInput;

    protected virtual void Awake()
    {
        transform.position = spawnPosition;
    }
    public virtual void OnCatch()
    {
        audioController.PlayCatchAudioClip();
        animatorController.PlayCatchAnimation();
        baseInput.StopAllCoroutines();
    }

    public virtual void PlayerJump_Action_Performed(InputAction.CallbackContext ctx)
    {
        baseInput.TapPerformed();
    }
    public virtual void PlayerJump_Action_Canceled(InputAction.CallbackContext ctx)
    {
        baseInput.TapCanceled();
    }
    public virtual void GameStarted()
    {
        animatorController.PlayHopAnimation();
    }

    public virtual void ResetState()
    {
        animatorController.PlayIdleStaticAnimation();
        transform.position = spawnPosition;
        Destroy(baseInput);
    }
}
