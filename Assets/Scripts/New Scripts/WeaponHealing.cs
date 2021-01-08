using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHealing : AWeapon, IHeallingWeapon
{
    [SerializeField] private HealingData waterHealingData;
  //  [SerializeField] private ObjectFinder objectFinder;

    private GameObject healingObject;


    private int currentHealCount = 0;

    public override WeaponType WeaponType
    {
        get
        {
            return WeaponType.Directional;
        }
    }




    //public void Attack(GameObject healingObject)
    //{
    //    if (state != WeaponState.ImposibleAttack && currentHealCount >= healCount)
    //    {
    //        StopCoroutine(Damaging(attackTime,healingObject));
    //        StartCoroutine(Reload(reloadTime));
    //    }
    //    else if (state == WeaponState.Serenity)
    //    {
    //        currentHealCount++;
    //        StartCoroutine(Damaging(attackTime, healingObject));
    //    }
    //}

    //private void Start()
    //{
    //    if(objectFinder.TryFindGameObject(transform.position, out GameObject returned))
    //    {
    //        Debug.Log("Нашли " + returned.name);
    //    }
    //}

    public override void UseWeapon()
    {
        if (FindComponentInIerarhy<ParamController>(weaponObject, out ParamController finded))
        {
            Heal(finded.gameObject);
        }
    }

    public void Heal(GameObject healingObject)
    {
        this.healingObject = healingObject;
        if (state != WeaponState.ImposibleAttack && currentHealCount >= waterHealingData.HealCount)
        {
            StopCoroutine(Damaging(waterHealingData.AttackTime));
            StartCoroutine(Reload(waterHealingData.ReloadTime));
        }
        else if (state == WeaponState.Serenity)
        {
            currentHealCount++;
            StartCoroutine(Damaging(waterHealingData.AttackTime));
        }
    }

    protected override IEnumerator Damaging(float time)
    {
        State = WeaponState.Attack;
        if (state == WeaponState.Attack)
        {
            if (FindComponentInIerarhy<ParamController>(healingObject, out ParamController finded))
            {
                foreach (var healType in waterHealingData.WeaponData)
                {
                    finded.DamagebleParams.HealAllByType(healType);
                }
            }

            yield return new WaitForSecondsRealtime(time);
            StartCoroutine(Serenity(0f));
        }
    }

    protected override IEnumerator Reload(float time)
    {
        State = WeaponState.Reload;
        if (state == WeaponState.Reload)
        {
            yield return new WaitForSecondsRealtime(time); 
            currentHealCount = 0;
            StartCoroutine(Serenity(0f));
        }
    }

    protected override IEnumerator Serenity(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        State = WeaponState.Serenity;
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        state = WeaponState.Serenity;
    }

    private bool FindComponentInIerarhy<T>(GameObject searchObject, out T finded) where T : MonoBehaviour
    {
        finded = default(T);
        if (searchObject.GetComponent<T>() != null)
        {
            finded = searchObject.GetComponent<T>();            
            return true;
        }
        else if (searchObject.GetComponentInParent<T>() != null)
        {
            finded = searchObject.GetComponentInParent<T>();
            return true;
        }
        else if (searchObject.GetComponentInChildren<T>() != null)
        {
            finded = searchObject.GetComponentInChildren<T>();
            return true;
        }
        return false;
    }


    //private void OnDrawGizmos()
    //{
    //    objectFinder.DrawGizmos(transform.position);
    //}

    //[System.Serializable]
    //private class ObjectFinder
    //{
    //    [SerializeField] private LayerMask layerMask;
    //    [SerializeField] private float distance;
    //    [SerializeField] private float radius;
    //    [SerializeField] private Vector3 direction;
    //    [SerializeField] private Color color;

    //    public bool TryFindGameObject(Vector3 position, out GameObject returned)
    //    {
    //        returned = null;
    //        if (Physics.SphereCast(position, radius, direction, out RaycastHit hit, distance, layerMask))
    //        {
                
    //            Debug.Log("Что-то нашли!");
    //            returned = hit.collider.gameObject;
    //            return true;
    //        }
    //        return false;
    //    }

      
    //    public void DrawGizmos(Vector3 position)
    //    { 
    //        Gizmos.color = color;
    //        Gizmos.DrawSphere(position, radius);
    //        Gizmos.DrawSphere(position + Vector3.Normalize(direction) * distance, radius);
    //    }
    //}

    [System.Serializable]
    private class HealingData
    {
        [SerializeField] private List<DamageByType> weaponData;
        [SerializeField] private float attackTime;
        [SerializeField] private float reloadTime = 0f;
        [SerializeField] private float healCount = 0;

        public List<DamageByType> WeaponData
        {
            get
            {
                return weaponData;
            }
        }

        public float AttackTime
        {
            get
            {
                return attackTime;
            }
        }

        public float ReloadTime
        {
            get
            {
                return reloadTime;
            }
        }

        public float HealCount
        {
            get
            {
                return healCount;
            }
        }

    }
}
