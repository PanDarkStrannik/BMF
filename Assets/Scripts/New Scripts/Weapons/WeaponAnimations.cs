using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimations : MonoBehaviour
{
    [SerializeField] protected Animator weaponAnimator;
    [SerializeField] protected string blendTreeFloat;

  
    public virtual void OnPlayerMove()
    {
        var movespeed = PlayerInformation.GetInstance().PlayerMovement.body.velocity.magnitude;
        weaponAnimator.SetFloat(blendTreeFloat, movespeed, 0.1f, Time.deltaTime);
    }
}
