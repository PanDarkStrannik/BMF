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
    private Prop prop;

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
            prop.ChangePropState(PropState.Serenity);
            StartCoroutine(Attacking(toAttackTime));
        }
    }

    protected override IEnumerator Attacking(float time)
    {
        State = WeaponState.Attack;
        if(state == WeaponState.Attack)
        {
            yield return new WaitForSeconds(time);
            propBody.isKinematic = false;
            propBody.AddForce(firePoint.forward * throwForce * Time.deltaTime, forceMode);
            StartCoroutine(Reload(toReloadTime));
        }

    }

    protected override IEnumerator Reload(float time)
    {
        State = WeaponState.Reload;
        if(state == WeaponState.Reload)
        {
            yield return new WaitForSeconds(time);
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
        if(prop != null)
        {
            propBody.isKinematic = true;
            prop.transform.position = Vector3.MoveTowards(prop.transform.position, firePoint.position, Time.deltaTime * setForce);
            if(Vector3.Distance(firePoint.position, prop.transform.position) < 0.1f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }



    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.CompareTag("Prop") && !isItemFound)
        //{
        //    isItemFound = true;
        //    GetPropComponents(other);
        //    Debug.Log(prop.name);
        //}

        prop = other.GetComponent<Prop>();
        if(prop == null)
        {
             prop = other.GetComponentInParent<Prop>();
             if(prop == null)
             {
                 prop = other.GetComponentInChildren<Prop>();
             }
        }

        if(prop != null && !isItemFound)
        {
            isItemFound = true;
            GetPropComponents();
            prop.ChangePropState(PropState.Telekinesis);
            Debug.Log(prop.name);

        }

    }

    private void GetPropComponents()
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
