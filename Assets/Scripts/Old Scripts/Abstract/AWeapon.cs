using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] protected GameObject weaponObject;
    [SerializeField] protected UnityEvent<bool> AttackStartEvent;
    [SerializeField] protected UnityEvent OnAttackEvent;

    [SerializeField] protected List<AttackState> attackStates;

    protected WeaponState state = WeaponState.Serenity;

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
    }

    public GameObject WeaponObject
    {
        get
        {
            return weaponObject;
        }
    }



    protected virtual void Awake()
    {
        foreach (var e in attackStates)
        {
            e.EventState += WeaponEventListener;
        }
    }


    protected virtual void OnDestroy()
    {
        foreach (var e in attackStates)
        {
            e.EventState -= WeaponEventListener;
        }
    }


    public virtual void Attack()
    {
        if (state == AWeapon.WeaponState.Serenity)
        {
            StartCoroutine(EventsStarter());
        }
    }

    private IEnumerator EventsStarter()
    {
        if (attackStates.Count > 0)
        {
            for (int i = 0; i < attackStates.Count; i++)
            {
                StartCoroutine(attackStates[i].Invoke());
                yield return new WaitForSeconds(attackStates[i].MinTime);
            }
        }
    }

    private void WeaponEventListener(AttackState weaponEvent)
    {
        state = weaponEvent.WeaponState;

        if(weaponEvent.ActivateStateFunc)
        {
            switch (weaponEvent.WeaponState)
            {
                case WeaponState.Attack:
                    StartCoroutine(Damaging(weaponEvent.FuncTime));
                    break;
                case WeaponState.Reload:
                    StartCoroutine(Reload(weaponEvent.FuncTime));
                    break;
                case WeaponState.ImposibleAttack:
                    Debug.LogError("Стейт невозможности атаки! Функции нет!");
                    break;
                case WeaponState.Serenity:
                    Debug.LogError("Стейт спокойствия оружия! Функции нет!");
                    break;
                default:
                    break;
            }
        }
    }


    #region Переопределяемые методы

    protected virtual IEnumerator Damaging(float time)
    {
        Debug.Log("Нанесение урона начали");
        yield return new WaitForSeconds(time);
        Debug.Log("Нанесение урона окончено");
    }

    protected virtual IEnumerator Reload(float time)
    {
        Debug.Log("Перезарядку начали");
        yield return new WaitForSeconds(time);
        Debug.Log("Перезарядка окончена");
    }

    public virtual IEnumerator StopAttack(float stopTime)
    {
        Debug.Log("Остановили Атаку!");
        StopAllCoroutines();
        state = WeaponState.ImposibleAttack;
        yield return new WaitForSeconds(stopTime);
        state = WeaponState.Serenity;
        Debug.Log("Разрешили Атаку!");
    }
    #endregion


    #region Вспомогательный класс

    [System.Serializable]
    public class AttackState
    {
        [SerializeField] private WeaponState weaponState;
        [SerializeField] private float toEventStart;
        [SerializeField] private bool activateStateFunc;
        [SerializeField] protected float funcTime;
        [SerializeField] private UnityEvent weaponEvent;

        //private AttackState nextState;

        //public AttackState NextState
        //{
        //    get
        //    {
        //        return nextState;
        //    }

        //    set
        //    {
        //        nextState = value;
        //    }

        //}

        public bool ActivateStateFunc
        {
            get
            {
                return activateStateFunc;
            }
        }

        public WeaponState WeaponState
        {
            get
            {
                return weaponState;
            }
        }

        public float FuncTime
        {
            get
            {
                return funcTime;
            }
        }

        public float MinTime
        {
            get
            {
                if (funcTime < toEventStart)
                {
                    return funcTime;
                }
                else
                {
                    return toEventStart;
                }
            }
        }

        public delegate void WeaponEventStateHelper(AttackState weaponEvent);
        public event WeaponEventStateHelper EventState;

        public IEnumerator Invoke()
        {
            EventState?.Invoke(this);
            yield return new WaitForSeconds(toEventStart);
            weaponEvent?.Invoke();
        }
    }

    #endregion

    public enum WeaponState
    {
        Attack, Reload, ImposibleAttack, Serenity
    }


}

public enum WeaponType
{
    Mili, Range, Jump, Summon, Blink
}