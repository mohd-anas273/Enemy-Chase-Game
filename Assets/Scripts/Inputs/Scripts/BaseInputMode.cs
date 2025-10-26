using System;
using UnityEngine;

public class BaseInputMode : MonoBehaviour
{
    [HideInInspector]public float movementSpeed = 5;
    protected bool shouldMove = true;
    public Action<float> progress_Action;
    public virtual void TapPerformed()
    {
        //Function overriden by derived classes
    }

    public virtual void TapCanceled()
    {
        //Function overriden by derived classes
    }
}
