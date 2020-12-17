using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealArea : MonoBehaviour
{
    [SerializeField] private List<DamageByType> damages;
    [SerializeField] private Collider damageCollider;
    [SerializeField] private Vector3 colliderScale;
    [SerializeField] private Color gizmosColor = Color.red;
    [SerializeField] private float timeBetweenDamage = 1f;
    [SerializeField] private LayerMask layer;

    [SerializeField] private Transform parent = null;

    [SerializeField] private Vector3 push;
    [SerializeField] private ForceMode forceMode;

    private Dictionary<Collider, bool> enterColiders;

    public Transform Parent
    {
        get
        {
            return parent;
        }
        set
        {
            parent = value;
        }
    }

    public List<DamageByType> Damages
    {
        get
        {
            return damages;
        }
        set
        {
            damages = value;
        }
    }

    public void AddDamage(List<DamageByType> addDamages)
    {
        if (damages.Count > 0)
        {
            var temp = new List<DamageByType>();
            foreach (var damage in damages)
            {
                foreach (var add in addDamages)
                {
                    if (damage.DamageType == add.DamageType)
                    {
                        damage.AddDamage(add.DamageValue);
                    }
                    else
                    {
                        temp.Add(add);
                    }
                }
            }
            damages.AddRange(temp);
        }
        else
        {
            damages = addDamages;
        }
    }

    private void Awake()
    {

        enterColiders = new Dictionary<Collider, bool>();

        if (parent != null)
        {
            damageCollider.transform.parent = parent;
        }
        damageCollider.isTrigger = true;
    }


    private void OnDisable()
    {
        if (enterColiders.Count > 0)
        {
            enterColiders.Clear();
            StopAllCoroutines();
        }
    }


    public void ChangeArea(Vector3 scale)
    {
        damageCollider.transform.localScale = colliderScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!enterColiders.ContainsKey(other))
        {
            enterColiders.Add(other, false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<IDamageble>() != null)
        {
            if (!enterColiders[other])
            {
                if ((layer.value & other.transform.GetComponent<ADamageble>().Layer.value) != 0)
                {
                    StartCoroutine(GetDamage(other));
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (enterColiders.ContainsKey(other))
        {
            StopCoroutine(GetDamage(other));
            // enterColiders.Remove(other);
        }
    }

    private IEnumerator GetDamage(Collider other)
    {
        enterColiders[other] = true;

        foreach (var damage in damages)
        {
            other.GetComponent<IDamageble>().ApplyHeal(damage);
        }
        Debug.Log("Зафиксили урон");
        yield return new WaitForSeconds(timeBetweenDamage);
        //if (timer >= timeBetweenDamage)
        //{
        enterColiders[other] = false;
        // }
    }

    public void TimeToDeactiveArea(float time)
    {
        Invoke("DeactiveArea", time);
    }
    private void DeactiveArea()
    {
        if (parent != null)
        {
            gameObject.transform.position = parent.transform.position;
            gameObject.transform.parent = parent;
        }
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        ChangeArea(colliderScale);
        Gizmos.color = gizmosColor;
        Gizmos.DrawCube(damageCollider.transform.position, damageCollider.transform.lossyScale);
    }

}
