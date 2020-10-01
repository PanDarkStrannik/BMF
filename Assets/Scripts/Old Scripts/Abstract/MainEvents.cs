using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEvents : MonoBehaviour
{
    public delegate void AnimTypeEventHandler(AnimationController.AnimationType animationType);
    public event AnimTypeEventHandler AnimTypeEvent;
    public void OnAnimEvent(AnimationController.AnimationType animationType)
    {
        AnimTypeEvent?.Invoke(animationType);
    }

    public delegate void AnimTypeFloatEventHandler(AnimationController.AnimationType animationType, float value);
    public event AnimTypeFloatEventHandler AnimTypeEventWithFloat;
    public void OnAnimEvent(AnimationController.AnimationType animationType, float value)
    {
        AnimTypeEventWithFloat?.Invoke(animationType, value);
    }


    public delegate void DetectedObjectHelper(Transform detectedObject);
    public event DetectedObjectHelper OnDetectedObject;
    public void DetectedObjectEvent(Transform point)
    {
        OnDetectedObject?.Invoke(point);
    }


    public delegate void EffectTypeHandler(EffectsController.EffectType effectType, bool activ);
    public event EffectTypeHandler EffectTypeEvent;
    public void OnEffectEvent(EffectsController.EffectType effectType, bool activ)
    {
        EffectTypeEvent?.Invoke(effectType, activ);
    }



    public delegate void OnAnimationStateEventHelper(StateTypeAnimAction animAction);
    public event OnAnimationStateEventHelper AnimationStateEvent;
    public void OnAnimationStateEvent(StateTypeAnimAction animAction)
    {
        AnimationStateEvent?.Invoke(animAction);
    }
}
