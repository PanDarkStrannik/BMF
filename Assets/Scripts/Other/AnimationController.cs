using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationController : MonoBehaviour
{
    [SerializeField] private List<WeaponAnimTriggerName> animations;
    [SerializeReference] private List<MainEvents> animationEvents;

    private void Start()
    {
        foreach(var animEv in animationEvents)
        {
            animEv.AnimTypeEvent += ActivateRandomeTriggerByType;
            animEv.AnimTypeEventWithFloat += GiveValueToAllAnimType;
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
            Debug.Log("Активирован: "+rand);
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

    public AnimationController.AnimationType Type
    {
        get
        {
            return type;
        }
    }

    public void ActivateAnimTrigger()
    {
        animator.SetTrigger(animTriggerName);    
    }

    public void SetAnimBool(bool setValue)
    {
        animator.SetBool(animTriggerName, setValue);
    }

    public void SetValue(float value)
    {
        if(animator.isActiveAndEnabled)
        {
            animator.SetFloat(animTriggerName, value);
        }
    }
    
}

