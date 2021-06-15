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

    private GameObject prop = null;
    private Rigidbody propBody = null;

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
            propBody.AddForce(firePoint.forward * throwForce * Time.deltaTime, forceMode);
            propBody.useGravity = true;
            StartCoroutine(Reload(toReloadTime));
        }

    }

    protected override IEnumerator Reload(float time)
    {
        State = WeaponState.Reload;
        if(state == WeaponState.Reload)
        {
            ResetProp();
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
            propBody.useGravity = false;
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



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Prop") && !isItemFound)
        {
            isItemFound = true;
            GetPropComponents(other);
            Debug.Log(prop.name);
        }
    }

    private void GetPropComponents(Collider other)
    {
        prop = other.gameObject;
        propBody = other.GetComponent<Rigidbody>();
        if(propBody == null)
        {
            propBody = other.GetComponentInChildren<Rigidbody>();
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
