using UnityEngine;
using System.Collections;

public class TapInputMode : BaseInputMode
{
    private int maxTaps = 4;
    private float movementTime = 0.3f;
    private int taps = 0;

    public void Initialize(int maxTaps, float movementTime, float movementSpeed)
    {
        this.maxTaps = maxTaps;
        this.movementTime = movementTime;
        this.movementSpeed = movementSpeed;
    }
    public override void TapPerformed()
    {
        if (!shouldMove)
        {
            return;
        }
        taps++;
        float value= (float)taps/maxTaps;
        progress_Action?.Invoke(value);
        StartCoroutine(PlayerJump_Coroutine());
    }
    IEnumerator PlayerJump_Coroutine()
    {
        shouldMove = false;
        Vector3 move = transform.position;
        float t = 0;
        while (t <= movementTime)
        {
            float deltaTime = Time.deltaTime;
            move.x += movementSpeed * deltaTime;
            transform.position = move;
            t += deltaTime;
            yield return null;
        }
        shouldMove = true;
    }
}
