using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWithDamageArea : SimpleBullet
{
    [SerializeReference] private DamageArea damageAreaObject;
    [SerializeField] protected float timeToDestroyArea = 1f;

    protected override void OnDie()
    {
        base.OnDie();
        damageAreaObject.gameObject.GetComponent<Collider>().enabled = true;
        damageAreaObject.transform.parent = null;
        damageAreaObject.transform.position = gameObject.transform.position;
        damageAreaObject.Parent = transform;
        damageAreaObject.TimeToDeactiveArea(timeToDestroyArea);
    }
}
