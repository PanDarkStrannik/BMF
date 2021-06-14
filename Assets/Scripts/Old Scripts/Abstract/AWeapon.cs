using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AWeapon : MonoBehaviour
{
    [SerializeField] protected GameObject weaponObject;
    [SerializeField] private List<EventOnAttackState> events;

    protected bool isWeaponCharged = false;

    protected WeaponState state = WeaponState.Serenity;

    #region PROPERTIES

    public abstract WeaponType WeaponType
    {
        get;
    }

    public WeaponState State
    {
        get
        {
            return state;
        }

        protected set
        {
            WeaponStateEvent?.Invoke(value);
            state = value;
        }

    }

    public GameObject WeaponObject
    {
        get
        {
            return weaponObject;
        }
    }

    public bool IsWeaponCharged { get => isWeaponCharged; set => isWeaponCharged = value; }

    #endregion


    public delegate void WeaponStateEventHelper(WeaponState state);
    public event WeaponStateEventHelper WeaponStateEvent;



    protected virtual void Awake()
    {
        WeaponStateEvent += WeaponStateListener;
    }

    protected virtual void OnDestroy()
    {
        WeaponStateEvent -= WeaponStateListener;
    }

    private void WeaponStateListener(WeaponState state)
    {
        StartCoroutine(EventStarter(state));
    }


    public IEnumerator EventStarter(AWeapon.WeaponState weaponState)
    {
        for (int i = 0; i < events.Count; i++)
        {
            if (events[i].WeaponState == weaponState)
            {
                StartCoroutine(events[i].Invoke());
                yield return new WaitForSeconds(events[i].MinTime);
            }
        }
    }

    //public virtual void Attack()
    //{
    //   throw new System.Exception("Ненаправленная атака нереализована");      
    //}

    //public virtual void Attack(GameObject attackedObject)
    //{
    //    throw new System.Exception("Направленная атака нереализована");
    //}


    #region Переопределяемые методы

    protected virtual IEnumerator Charge()
    {
        State = WeaponState.Charge;
        isWeaponCharged = true;
        yield return new WaitForSeconds(0);
    }

    protected virtual IEnumerator Attacking(float time)
    {
        State = WeaponState.Attack;
        Debug.Log("Нанесение урона начали");
        yield return new WaitForSeconds(time);
        Debug.Log("Нанесение урона окончено");
    }

    protected virtual IEnumerator Attacking(float time, GameObject damagingObject)
    {
        State = WeaponState.Attack;
        Debug.Log("Нанесение урона начали");
        yield return new WaitForSeconds(time);
        Debug.Log("Нанесение урона окончено");
    }

    protected virtual IEnumerator Reload(float time)
    {
        State = WeaponState.Reload;
        Debug.Log("Перезарядку начали");
        yield return new WaitForSeconds(time);
        Debug.Log("Перезарядка окончена");
    }

    protected virtual IEnumerator Serenity(float time)
    {
        State = WeaponState.Serenity;
        Debug.Log("Перезарядку начали");
        yield return new WaitForSeconds(time);
        Debug.Log("Перезарядка окончена");
    }

    public virtual IEnumerator StopAttack(float stopTime)
    {
        Debug.Log("Остановили Атаку!");
        StopAllCoroutines();
        State = WeaponState.ImposibleAttack;
        yield return new WaitForSeconds(stopTime);
        State = WeaponState.Serenity;
        Debug.Log("Разрешили Атаку!");
    }


    public abstract void UseWeapon();
   

    #endregion


    public enum WeaponState
    {
        Charge,Attack, Reload, ImposibleAttack, Serenity, TransitionState
    }


    public bool TryReturnNeededWeaponType<T>(out T neededWeaponType) where T : class
    {
        neededWeaponType = null;
        if (this is T)
        {
            neededWeaponType = this as T;
            Debug.Log($"Оружие может использоваться как: {typeof(T).Name}");
            return true;
        }
        Debug.Log($"Оружие не может использоваться как: {typeof(T).Name}");
        return false;
    }



    #region Вспомогательный класс

    [System.Serializable]
    public class EventOnAttackState
    {
        [SerializeField] private AWeapon.WeaponState weaponState;
        [SerializeField] private float toEventStart;
        [SerializeField] private UnityEvent weaponEvent;

        private EventOnAttackState nextState;

        public EventOnAttackState NextState
        {
            get
            {
                return nextState;
            }

            set
            {
                nextState = value;
            }

        }

        public UnityEvent WeaponEvent
        {
            get
            {
                return weaponEvent;
            }
        }



        public AWeapon.WeaponState WeaponState
        {
            get
            {
                return weaponState;
            }
        }



        public float MinTime
        {
            get
            {

                return toEventStart;

            }
        }

        public delegate void WeaponEventStateHelper(EventOnAttackState weaponEvent);
        public event WeaponEventStateHelper EventState;

        public delegate void WeaponEventEndHelper();
        public event WeaponEventEndHelper EventEnd;

        public IEnumerator Invoke()
        {
            EventState?.Invoke(this);
            yield return new WaitForSeconds(toEventStart);
            weaponEvent?.Invoke();
            EventEnd?.Invoke();
        }
    }

    #endregion

}

public enum WeaponType
{
    Melee, Range, Jump, Summon, Blink, Directional, Heavy, Telekinesis
}