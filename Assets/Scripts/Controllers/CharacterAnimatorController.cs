using System;
using UnityEngine;

[RequireComponent (typeof(Animator))]
public class CharacterAnimatorController : MonoBehaviour
{
    private Animator animator;
    public readonly int catchAnimationHash = Animator.StringToHash("CatchAnim");
    public readonly int hopAnimationHash = Animator.StringToHash("HopAnim");
    public readonly int idleStaticAnimationHash = Animator.StringToHash("IdleStaticAnim");
    public readonly int victoryAnimationHash= Animator.StringToHash("VictoryAnim");
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void PlayCatchAnimation()
    {
        animator.Play(catchAnimationHash);
    }
    public void PlayHopAnimation()
    {
        animator.Play(hopAnimationHash);
    }

    //Called by Animation Event
    public void OnVictoryAnimation()
    {
        animator.Play(victoryAnimationHash);
    }
    public void PlayIdleStaticAnimation()
    {
        animator.Play(idleStaticAnimationHash);
    }
    
}
