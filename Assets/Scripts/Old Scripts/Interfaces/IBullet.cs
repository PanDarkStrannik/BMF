using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    void Init(List<DamageByType> datas, LayerMask layerMask);
}
