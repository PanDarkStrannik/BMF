using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telekinesis : AWeapon, IDamagingWeapon
{
    public override WeaponType WeaponType => WeaponType.Telekinesis;

    [SerializeField] private Transform firePoint;
    [SerializeField] private ForceMode forceMode;
    [SerializeField] private float throwForce;
    [SerializeField] private float setForce;


    [SerializeField] private float toAttackTime;
    [SerializeField] private float toReloadTime;
    [SerializeField] private float coolDown;

    private Rigidbody propBody = null;
    private Prop prop = null;

    private bool isItemFound = false;


    public override void UseWeapon()
    {
        Attack();
    }

    public void Attack()
    {
        if (state == WeaponState.Serenity)
        {
            StopAllCoroutines();
            StartCoroutine(Charge());
        }
    }


    protected override IEnumerator Charge()
    {
        State = WeaponState.Charge;
        if(state == WeaponState.Charge)
        {
            yield return new WaitUntil(() => ReadyToThrow());
            StartCoroutine(Attacking(toAttackTime));
        }
    }

    protected override IEnumerator Attacking(float time)
    {
        State = WeaponState.Attack;
        if(state == WeaponState.Attack)
        {
            yield return new WaitForSeconds(time);
            Throw();
            StartCoroutine(Reload(toReloadTime));
        }

    }

    protected override IEnumerator Reload(float time)
    {
        State = WeaponState.Reload;
        if(state == WeaponState.Reload)
        {
            prop.ChangePropState(PropState.Serenity);
            yield return new WaitForSeconds(time);
            ResetProp();
            StartCoroutine(Serenity(coolDown));
        }
    }

    protected override IEnumerator Serenity(float time)
    {
        yield return new WaitForSeconds(time);
        State = WeaponState.Serenity;
    }


    private bool ReadyToThrow()
    {
        if(propBody != null)
        {
            propBody.isKinematic = true;
            prop.transform.position = Vector3.MoveTowards(prop.transform.position, firePoint.position, Time.deltaTime * setForce);
            if(Vector3.Distance(firePoint.position, prop.transform.position) < 0.1f)
            {
                prop.transform.parent = firePoint;
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private void Throw()
    {
        if(propBody != null)
        {
            propBody.isKinematic = false;
            propBody.AddForce(firePoint.forward * throwForce * Time.deltaTime, forceMode);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.CompareTag("Prop") && !isItemFound)
        //{
        //    isItemFound = true;
        //    GetPropComponents(other);
        //    Debug.Log(prop.name);
        //}

        if(prop == null)
        {
            prop = other.GetComponent<Prop>();
            if(prop == null)
            {
                 prop = other.GetComponentInParent<Prop>();
                 if(prop == null)
                 {
                     prop = other.GetComponentInChildren<Prop>();
                 }
            }
        }

        if(prop != null && !isItemFound)
        {
            if(!prop.isTaken)
            {
                isItemFound = true; 
                GetPropBody();
                prop.ChangePropState(PropState.Telekinesis);
                Debug.Log(prop.name);
            }
            else
            {
                ResetProp();
            }

        }

    }

    private void GetPropBody()
    {
        if(prop != null)
        {
            propBody = prop.GetComponent<Rigidbody>();
            if(propBody == null)
            {
                propBody = prop.GetComponentInParent<Rigidbody>();
                if(propBody == null)
                {
                    propBody = prop.GetComponentInChildren<Rigidbody>();
                }
            }
        }
    }

    private void ResetProp()
    {
        prop.transform.parent = null;
        prop = null;
        propBody = null;
        isItemFound = false;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        State = WeaponState.Serenity;
    }

}
