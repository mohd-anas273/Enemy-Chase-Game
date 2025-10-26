using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressUIController : MonoBehaviour
{
    [SerializeField] private float progressLerpValue = 2f;
    [SerializeField] private Slider progressSlider;


    public Action<float> OnProgressChanged_Action;
    private float progressValue=0;
    public void Enable()
    {
        StartCoroutine(SmoothSlider());
    }

    public void Disable()
    {
        StopAllCoroutines();
    }
    public void UpdateProgress(float progress)
    {
        progressValue = progress;
        OnProgressChanged_Action?.Invoke(progressValue);
    }

    IEnumerator SmoothSlider()
    {
        while (true)
        {
            progressSlider.value = Mathf.Lerp(progressSlider.value, progressValue, progressLerpValue * Time.deltaTime);
            yield return null;
        }
    }

    public void ResetProgress()
    {
        StopAllCoroutines();
        progressValue = 0;
        progressSlider.value = 0;
    }
}
