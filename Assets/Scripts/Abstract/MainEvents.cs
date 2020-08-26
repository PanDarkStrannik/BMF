using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEvents : MonoBehaviour
{
    public delegate void AnimTypeEventHandler(AnimationController.AnimationType animationType);
    public event AnimTypeEventHandler AnimTypeEvent;
    public virtual void OnAnimEvent(AnimationController.AnimationType animationType)
    {
        AnimTypeEvent?.Invoke(animationType);
    }

    public delegate void AnimTypeFloatEventHandler(AnimationController.AnimationType animationType, float value);
    public event AnimTypeFloatEventHandler AnimTypeEventWithFloat;
    public virtual void OnAnimEvent(AnimationController.AnimationType animationType, float value)
    {
        AnimTypeEventWithFloat?.Invoke(animationType, value);
    }


}
