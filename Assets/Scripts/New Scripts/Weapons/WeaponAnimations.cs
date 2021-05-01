using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;


public class WeaponAnimations : MonoBehaviour
{
    [SerializeField] private Animator weaponAnimator;
    [SerializeField] private string blendTreeFloat;
    [SerializeField] private string shiftBool;
    [SerializeField] private WAnimationRandomizer randomAnimation;
    [SerializeField] private List<AnimationEvents> animationEvents;

    private void OnEnable()
    {
        PlayerInformation.GetInstance().PlayerController.OnPlayerMoved += OnPlayerMove;
    }
    private void OnDisable()
    {
        PlayerInformation.GetInstance().PlayerController.OnPlayerMoved -= OnPlayerMove;
    }


    private void OnPlayerMove(Vector3 obj, bool shifting)
    {
        weaponAnimator.SetFloat(blendTreeFloat, obj.magnitude, 0.1f, Time.deltaTime);
        if(obj != Vector3.zero && shiftBool != null)
        {
            weaponAnimator.SetBool(shiftBool, !shifting);
        }
        else
        {
            weaponAnimator.SetBool(shiftBool, false);
        }
    }

    public void PlayRandomAnimation()
    {
        randomAnimation.RandomizeAnimation(weaponAnimator);
    }

    public void InvokeAnimationEvent()
    {
        if(animationEvents.Count > 0)
        {
            foreach (var a in animationEvents)
            {
                a.Invoke();
            }
        }
    }

}

[System.Serializable]
public class WAnimationRandomizer
{
    [SerializeField] private string[] triggers;

    public void RandomizeAnimation(Animator anim)
    {
        if(anim != null && triggers.Length > 0)
        {
            int index = Random.Range(0, triggers.Length);
            string triggerName = triggers[index];
            anim.SetTrigger(triggerName);
        }
    }
}

[System.Serializable]
public class AnimationEvents
{
    public string Name;
    public UnityEvent AnimationEvent;

    public void Invoke()
    {
        AnimationEvent?.Invoke();
    }
}