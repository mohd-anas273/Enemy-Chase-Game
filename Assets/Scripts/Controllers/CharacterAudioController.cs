using UnityEngine;

public class CharacterAudioController : MonoBehaviour
{
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected AudioClip hopingAudioClip;
    [SerializeField] protected AudioClip catchAudioClip;

    public void PlayCatchAudioClip()
    {
        audioSource.PlayOneShot(catchAudioClip);
    }

    //Called by Controller for bird, for pig called by animation event
    public void PlayHopAudio()
    {
        if (hopingAudioClip != null)
        {
            audioSource.PlayOneShot(hopingAudioClip);
        }
    }
}
