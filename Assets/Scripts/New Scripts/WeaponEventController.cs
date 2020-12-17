using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class WeaponEventController
{
    //[SerializeField] private List<EventOnAttackState> events;




    

    //public IEnumerator EventStarter(AWeapon.WeaponState weaponState)
    //{
    //    for (int i = 0; i < events.Count; i++)
    //    {
    //        if (events[i].WeaponState == weaponState)
    //        {
    //            StartCoroutine(events[i].Invoke());
    //            yield return new WaitForSeconds(events[i].MinTime);
    //        }
    //    }
    //}




    //#region Вспомогательный класс

    //[System.Serializable]
    //public class EventOnAttackState
    //{
    //    [SerializeField] private AWeapon.WeaponState weaponState;
    //    [SerializeField] private float toEventStart;
    //    [SerializeField] private UnityEvent weaponEvent;

    //    private EventOnAttackState nextState;

    //    public EventOnAttackState NextState
    //    {
    //        get
    //        {
    //            return nextState;
    //        }

    //        set
    //        {
    //            nextState = value;
    //        }

    //    }

    //    public UnityEvent WeaponEvent
    //    {
    //        get
    //        {
    //            return weaponEvent;
    //        }
    //    }



    //    public AWeapon.WeaponState WeaponState
    //    {
    //        get
    //        {
    //            return weaponState;
    //        }
    //    }



    //    public float MinTime
    //    {
    //        get
    //        {

    //            return toEventStart;

    //        }
    //    }

    //    public delegate void WeaponEventStateHelper(EventOnAttackState weaponEvent);
    //    public event WeaponEventStateHelper EventState;

    //    public delegate void WeaponEventEndHelper();
    //    public event WeaponEventEndHelper EventEnd;

    //    public IEnumerator Invoke()
    //    {
    //        EventState?.Invoke(this);
    //        yield return new WaitForSeconds(toEventStart);
    //        weaponEvent?.Invoke();
    //        EventEnd?.Invoke();
    //    }
    //}

    //#endregion


}
