using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telekinesis : AWeapon, IDamagingWeapon
{
    public override WeaponType WeaponType => WeaponType.Telekinesis;

    [SerializeField] private Transform firePoint;
    [SerializeField] private float propForce;

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
        if (State == WeaponState.Serenity)
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
            propBody.AddForce(firePoint.forward * propForce * Time.deltaTime, ForceMode.Impulse);
            yield return new WaitForSeconds(toReloadTime);
            ResetProp();
            StartCoroutine(Reload(toAttackTime));
        }

    }

    protected override IEnumerator Reload(float time)
    {
        State = WeaponState.Reload;
        if(state == WeaponState.Reload)
        {
            yield return new WaitForSeconds(time);
            StartCoroutine(Serenity(toReloadTime));
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
            prop.transform.position = Vector3.Lerp(prop.transform.position, firePoint.position, Time.deltaTime);
            if(Vector3.Distance(firePoint.position, prop.transform.position) < 0.1f)
            {
              return true;
            }
            else
            {
                return false;
            }
        }
        Debug.Log("Вокруг нет предметов для броска");
        return false;
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Prop") && !isItemFound)
        {
            isItemFound = true;
            prop = other.gameObject;
            GetPropBody(other);
            Debug.Log(prop.name);
        }
    }

    private void GetPropBody(Collider other)
    {
        propBody = other.GetComponent<Rigidbody>();
        if(propBody == null)
        {
            propBody = other.GetComponentInChildren<Rigidbody>();
        }
    }

    private void ResetProp()
    {
        prop = null;
        propBody = null;
        isItemFound = false;
    }

}
