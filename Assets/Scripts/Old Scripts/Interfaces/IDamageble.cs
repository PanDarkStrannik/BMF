﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageble
{
    void ApplyDamage(DamageByType weapon);
    void ApplyHeal(DamageByType healWeapon);
    void Push(Vector3 pushValue, ForceMode forceMode);
}
