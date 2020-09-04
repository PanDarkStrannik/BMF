using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private List<WeaponAnimTriggerName> animations;
    [SerializeReference] private List<MainEvents> animationEvents;

    public delegate void InMovementRootAnimationHelper(bool rootMotion);
    public event InMovementRootAnimationHelper InMovementRoot; 
   


    private void Start()
    {
        foreach(var animEv in animationEvents)
        {
            animEv.AnimTypeEvent += ActivateRandomeTriggerByType;
            animEv.AnimTypeEventWithFloat += GiveValueToAllAnimType;
        }
        foreach (var e in animations)
        {
            e.Initialize();
            foreach (var animEv in animationEvents)
            {
                e.InChangeAnimator += animEv.OnAnimationStateEvent;
            }
        }
    }


  


    public void ActivateAllTriggerByType(AnimationType animationType)
    {
        var tmp = FindAllAnimByType(animationType);
        if (tmp != null)
        {
            foreach (var e in tmp)
            {                
                e.ActivateAnimTrigger();
            }
        }
    }

    public void GiveValueToAllAnimType(AnimationType animationType, float value)
    {
        var tmp = FindAllAnimByType(animationType);
        if(tmp!=null)
        {
            foreach(var e in tmp)
            {
                e.SetValue(value);
            }
        }
    }

    public void ActivateRandomeTriggerByType(AnimationType animationType)
    {
        var tmp = FindAllAnimByType(animationType);
        if (tmp != null)
        {
            var rand = Random.Range(0, tmp.Count);
            tmp[rand].ActivateAnimTrigger();
        }
    }


    private WeaponAnimTriggerName FindAnimByType(AnimationController.AnimationType type)
    {
        foreach(var anim in animations)
        {
            if(anim.Type==type)
            {
                return anim;
            }
        }
        return null;
    }


    private List<WeaponAnimTriggerName> FindAllAnimByType(AnimationController.AnimationType type)
    {
        List<WeaponAnimTriggerName> temp = new List<WeaponAnimTriggerName>();
        foreach (var anim in animations)
        {
            if (anim.Type == type)
            {
                temp.Add(anim);
            }
        }
        if (temp.Count > 0)
        {
            return temp;
        }
        else
        {
            return null;
        }
    }





    public enum AnimationType
    {
        MeleAttack, RangeAttack, ChangeWeapon, Movement, Start, Death
    }
}

[System.Serializable]
public class WeaponAnimTriggerName
{
    [SerializeField] private Animator animator;
    [SerializeField] private string animTriggerName;
    [SerializeField] private AnimationController.AnimationType type;
    [SerializeField] private List<StateTypeAnimAction> animsActions;

    private List<StateController> stateControllers;

    public delegate void InChangeAnimatorHelper(StateTypeAnimAction animAction);
    public event InChangeAnimatorHelper InChangeAnimator;

    public AnimationController.AnimationType Type
    {
        get
        {
            return type;
        }
    }

    public void Initialize()
    {
        stateControllers = new List<StateController>(animator.GetBehaviours<StateController>());

        foreach(var e in stateControllers)
        {
            e.StateEvent += StateTypeCheker;
        }
    }

    public void ReInitialize()
    {
        foreach (var e in stateControllers)
        {
            e.StateEvent -= StateTypeCheker;
        }
    }

    private void StateTypeCheker(StateController.StateType type)
    {
        if (animsActions.Count > 0)
        {
            foreach (var e in animsActions)
            {
                if (type == e.StateType)
                {
                    ChangeAnimator(e);
                    return;
                }
            }
        } 
    }


    private void ChangeAnimator(StateTypeAnimAction animAction)
    {        
        InChangeAnimator?.Invoke(animAction);
        animator.applyRootMotion = animAction.RootMotion;
    }


    public void ActivateAnimTrigger()
    {
        if (animator.isActiveAndEnabled)
        {        
            animator.SetTrigger(animTriggerName);
        }
    }

    public void SetAnimBool(bool setValue)
    {
        if (animator.isActiveAndEnabled)
        {
            animator.SetBool(animTriggerName, setValue);
        }
    }

    public void SetValue(float value)
    {
        if(animator.isActiveAndEnabled)
        {
            animator.SetFloat(animTriggerName, value);
        }
    }

 
    
}

[System.Serializable]
public class StateTypeAnimAction
{
    [SerializeField] private StateController.StateType stateType;
    [SerializeField] private bool rootMotion;


    public delegate void StateActivatedHelper();
    public event StateActivatedHelper StateActivated;

    public StateController.StateType StateType
    {
        get
        {
            return stateType;
        }
    }

    public bool RootMotion
    {
        get
        { 
            StateActivated?.Invoke();
            return rootMotion;
        }
    }
}