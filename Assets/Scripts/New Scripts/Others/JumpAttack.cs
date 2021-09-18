using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JumpAttack : AWeapon, IDamagingWeapon
{
    [SerializeReference] private Rigidbody body;
   // [SerializeField] private List<DamageByType> weaponData;
    [SerializeField] private float forceValue=3f;
    [SerializeField] private float damageTime = 3f;
    [SerializeField] private float reloadTime = 3f;
    [SerializeField] private AttackArea damageArea;
    [SerializeField] private UnityEvent<bool> damaging;
    [SerializeField] private ForceMode forceMode;

    //private Vector3 startBodyPosition;
    //private Quaternion startBodyRotation;

    private float damageTimer = 0f;
    private float reloadTimer = 0f;


    public override WeaponType WeaponType
    {
        get
        {
            return WeaponType.Jump;
        }
    }




    private void Start()
    {
      //  damageArea.AddDamage(weaponData);
        //startBodyRotation = body.transform.localRotation;
        //startBodyPosition = body.transform.localPosition;
    }

    public void Attack()
    {
        state = WeaponState.Attack;
        Debug.Log("Должна была произойти атака");
        body.velocity = Vector3.zero;
        body.AddForce(forceValue * body.transform.forward, forceMode);
    }

    public override void UseWeapon()
    {
        Attack();
    }

    public void FixedUpdate()
    {
        //if(state == WeaponState.Serenity)
        //{
        //    startBodyPosition = body.transform.position;
        //}



        if(state == WeaponState.Attack)
        {
            damageArea.gameObject.SetActive(true);
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
            damageArea.gameObject.SetActive(false);
            //body.Sleep();
            damaging?.Invoke(false);
        }

        if (state == WeaponState.Reload)
        {
            reloadTimer += Time.fixedDeltaTime;
            //body.transform.localPosition = startBodyPosition;
            //body.transform.localRotation = startBodyRotation;
            if (reloadTimer >= reloadTime)
            {
                reloadTimer = 0;
                state = WeaponState.Serenity;
            }
        }

    }

    private void OnDisable()
    {
        //body.transform.localRotation = startBodyRotation;
        //body.transform.localPosition = startBodyPosition;
        state = WeaponState.Serenity;
        damageArea.gameObject.SetActive(false);
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
