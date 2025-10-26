using UnityEngine;
using System.Collections;

public class HoldInputMode : BaseInputMode
{
    private Transform enemyTransform;
    private float catchDistance = 3.8f;
    private float totalDistance = 0;

    public void Initialize(Transform enemyTransform,float catchDistance,float movementSpeed)
    {
        this.enemyTransform = enemyTransform;
        this.catchDistance= catchDistance;
        this.movementSpeed=movementSpeed;
    }
    private void Start()
    {
        totalDistance = Vector2.Distance(transform.position, enemyTransform.position);
    }
    public override void TapPerformed()
    {
        shouldMove = true;
        StartCoroutine(PlayerJump_Coroutine());
    }
    public override void TapCanceled()
    {
        shouldMove = false;
    }

    IEnumerator PlayerJump_Coroutine()
    {
        Vector3 move = transform.position;
        while (shouldMove)
        {
            if (enemyTransform != transform)
            {
                float distance = Vector2.Distance(move, enemyTransform.position);
                if (distance < catchDistance)
                {
                    progress_Action?.Invoke(1);
                    yield break;
                }
                progress_Action?.Invoke(1 - Mathf.Clamp01(distance / totalDistance));
            }
            move.x += movementSpeed * Time.deltaTime;
            transform.position = move;
            yield return null;
        }
    }
}
