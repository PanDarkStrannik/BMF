using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;


public class WeaponAnimations : MonoBehaviour
{
    [SerializeField] private Animator weaponAnimator;
    [SerializeField] private string inputFloat;
    [SerializeField] private string xInputFloat;

    [SerializeField] private string shiftBool;
    [SerializeField] private bool usedByPlayer = true;
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
        if(usedByPlayer)
        {
            weaponAnimator.SetFloat(inputFloat, obj.magnitude, 0.1f, Time.deltaTime);
           if(obj != Vector3.zero && shiftBool != null)
           {
               weaponAnimator.SetBool(shiftBool, !shifting);
           }
           else
           {
               weaponAnimator.SetBool(shiftBool, false);
           }


            weaponAnimator.SetFloat(xInputFloat, obj.x, 0.1f, Time.deltaTime);
        }
    }

    public void PlayRandomAnimation()
    {
        randomAnimation.RandomizeAnimation(weaponAnimator);
    }

    public void InvokeAnimationEvent(string eventName)
    {
        if(animationEvents.Count > 0)
        {
            foreach (var a in animationEvents)
            {
                if(a.Name == eventName)
                {
                  a.Invoke();
                }
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