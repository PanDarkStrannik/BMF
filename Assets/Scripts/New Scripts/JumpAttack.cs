using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JumpAttack : AWeapon
{

    [SerializeReference] private Rigidbody body;
    [SerializeField] private float forceValue=3f;
    [SerializeField] private float damageTime = 3f;
    [SerializeField] private float reloadTime = 3f;
    [SerializeField] private UnityEvent<bool> damaging;

    private Vector3 startBodyPosition;
    
    private float damageTimer = 0f;
    private float reloadTimer = 0f;




    public override void Attack()
    {
        
        state = WeaponState.Attack;
        Debug.Log("Должна была произойти атака");
        body.AddForce(forceValue * body.transform.forward, ForceMode.Impulse);
    }

    public void FixedUpdate()
    {
        if(state == WeaponState.Serenity)
        {
            startBodyPosition = body.transform.position;
        }



        if(state == WeaponState.Attack)
        {
            damageTimer += Time.fixedDeltaTime;
            if (damageTimer >= damageTime)
            {
                damageTimer = 0;
                state = WeaponState.Reload;
            }
            else
            {
                damaging?.Invoke(true);
                //Damaging();
            }
        }
        else
        {
            damaging?.Invoke(false);
        }

        if (state == WeaponState.Reload)
        {
            reloadTimer += Time.fixedDeltaTime;
            body.transform.position = startBodyPosition;
            if (reloadTimer >= reloadTime)
            {
                reloadTimer = 0;
                state = WeaponState.Serenity;
            }
        }

    }

    //private void Damaging()
    //{
        
    //        if (damageCollider.Raycast(new Ray(damageCollider.transform.position, damageCollider.transform.forward), out RaycastHit hit, 1f))
    //        {
    //            if (hit.transform.GetComponentInParent<IDamageble>() != null && state == WeaponState.Attack)
    //            {
    //                if ((hit.transform.GetComponent<ADamageble>().Layer.value & (1 << layer)) == 0)
    //                {
    //                    foreach (var e in weaponData)
    //                    {
    //                        hit.transform.GetComponentInParent<IDamageble>().ApplyDamage(e);
    //                        Debug.Log("Вроде как нанесли урон, но хз");
    //                    }
    //                }
    //            }

    //        }
        
    //}

}
