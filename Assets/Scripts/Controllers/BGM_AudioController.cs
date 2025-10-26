using System.Collections;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class BGM_AudioController : MonoBehaviour
{
    [Tooltip("Final value for fade audio")]
    [Range(0f, 1f)]
    [SerializeField] private float fadeAudioVolume = 0.2f;

    [Space(8)]
    [SerializeField] private float durationToFadeAudio = 1f;

    private AudioSource audioSource;
    private Coroutine fadeAudio_Coroutine;
    private float initialVolume;

    private void Awake()
    {
        audioSource= GetComponent<AudioSource>();
        initialVolume=audioSource.volume;
    }
    public void PlayBGM()
    {
        StopRunningFadeAudioCoroutine();
        audioSource.volume = initialVolume;
        audioSource.Play();
    }

    public void StopBGM()
    {
        StopRunningFadeAudioCoroutine();
        fadeAudio_Coroutine = StartCoroutine(FadeAudio());
    }


    private void StopRunningFadeAudioCoroutine()
    {
        if (fadeAudio_Coroutine != null)
        {
            StopCoroutine(fadeAudio_Coroutine);
            fadeAudio_Coroutine = null;
        }
    }

    IEnumerator FadeAudio()
    {
        float t = 0f;
        while (t <= durationToFadeAudio)
        {
            float value = Mathf.Clamp01(t / durationToFadeAudio);
            audioSource.volume=Mathf.Lerp(initialVolume, fadeAudioVolume, value);
            t += Time.deltaTime;
            yield return null;
        }
        audioSource.Stop();
    }
}
