using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ADetecter : MonoBehaviour
{
    public delegate void DetectedObjectHelper(Transform detectedObject);
    public event DetectedObjectHelper OnDetectedObject;

    protected void DetectedObjectEvent(Transform point)
    {
        OnDetectedObject?.Invoke(point);
    }

}
