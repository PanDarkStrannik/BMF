using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeAnimation : WeaponAnimations
{
    private void LateUpdate()
    {
        OnPlayerMove();
    }

    public override void OnPlayerMove()
    {
        base.OnPlayerMove();
    }
}
