using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageArea : MonoBehaviour
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

    private Dictionary<Collider,bool> enterColiders;

    [SerializeField] private List<EventOnDamageLayer> damageLayersEvents;

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

        if(parent!=null)
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
            CheckingLayerDamage(other);
            enterColiders.Add(other, false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var bodyChild = other.gameObject.GetComponentInChildren<Rigidbody>();
        var bodyGO = other.gameObject.GetComponent<Rigidbody>();
        var bodyParent = other.gameObject.GetComponentInParent<Rigidbody>();


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
        else if (bodyGO != null)
        {
            PushArea(bodyGO);
        }
        else if( bodyChild != null)
        {
            PushArea(bodyChild);
        }
        else if (bodyParent != null)
        {
            PushArea(bodyParent);
        }


    }
    private void CheckingLayerDamage(Collider other)
    {
        for (int i = 0; i < damageLayersEvents.Count; i++)
        {
            if(other != null)
            {
                if(other.gameObject.CompareTag(damageLayersEvents[i].TagName))
                {
                    damageLayersEvents[i].InvokeByTag();
                }
            }


            var unitLayer = other.GetComponent<DamageblePlace>();
            if (unitLayer != null)
            {
                damageLayersEvents[i].InvokeByLayer(unitLayer.Layer);
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
           other.GetComponent<IDamageble>().ApplyDamage(damage);
           var tempPush = transform.TransformDirection(push);
            var unit = other.GetComponent<IDamageble>();
            unit.Push(tempPush, forceMode);
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


    private void PushArea(Rigidbody body)
    {
        var tempPush = transform.TransformDirection(push);
        body.AddForce(tempPush, forceMode);

    }


    private void OnDrawGizmosSelected()
    {
        ChangeArea(colliderScale);
        Gizmos.color = gizmosColor;
        Gizmos.DrawCube(damageCollider.transform.position, damageCollider.transform.lossyScale);
    }


}

[System.Serializable]
public class EventOnDamageLayer
{
    public string TagName;
    public LayerMask whoIsTarget;
    public UnityEvent OnDamaged;

    public void InvokeByLayer(LayerMask layer)
    {
        if(whoIsTarget == layer)
        {
            OnDamaged?.Invoke();
        }
    }

    public void InvokeByTag()
    {
         OnDamaged?.Invoke();
    }
}
