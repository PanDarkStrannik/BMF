using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleTimer : MonoBehaviour
{
    [SerializeField] private List<EventTimeComparer> timers = new List<EventTimeComparer>();


    public void StartTimer(int timerNum)
    {
        if (timerNum >= 0 && timerNum < timers.Count)
        {
            timers[timerNum].TryInvoke(this);
        }
        else
        {
            new System.Exception($"Таймер под номером {timerNum} не существует на объекте {gameObject.name}");
        }
    }

    public void StopTimer(int timerNum)
    {
        if (timerNum >= 0 && timerNum < timers.Count)
        {
            timers[timerNum].StopTimer();
        }
        else
        {
            new System.Exception($"Таймер под номером {timerNum} не существует на объекте {gameObject.name}");
        }
    }

    private void OnValidate()
    {
        if (timers.Count > 0)
        {
            for (int i = 0; i < timers.Count; i++)
            {
                timers[i].NumInList = i;
            }
        }
    }

    private void OnDisable()
    {
        foreach(var e in timers)
        {
            e.StopTimer();
        }
    }


    [System.Serializable]
    private class EventTimeComparer
    {
        [SerializeField] private string name;
        [SerializeField, ReadOnly] private int num = 0;
        [Min(0f)]
        [SerializeField] private float time = 0f;
        [SerializeField] private bool useRealtime = false;
        [SerializeField] private UnityEvent onTimeEnd;
        [SerializeField] private UnityEvent onDisableTimer;

        private bool alreadyStart = false;
        private MonoBehaviour behaviourForCoroutine = null;
        private Coroutine timerCoroutine = null;

        public int NumInList
        {
            get => num;
            set => num = value;
        }

        public bool TryInvoke(MonoBehaviour behaviourForCoroutine)
        {
            this.behaviourForCoroutine = behaviourForCoroutine;
            if (alreadyStart)
                return false;
            alreadyStart = true;
            timerCoroutine = behaviourForCoroutine.StartCoroutine(Timer());
            return true;
        }

        private IEnumerator Timer()
        {

            if (useRealtime)
            {
                for (int i = 0; i < time; i++)
                {
                    if (alreadyStart)
                    {
                        yield return new WaitForSecondsRealtime(1);
                    }
                    else
                        yield return null;
                }
            }
            else
            {
                for (int i = 0; i < time; i++)
                {
                    if (alreadyStart)
                    {
                        yield return new WaitForSeconds(1);
                    }
                    else
                        yield return null;
                }
            }
            if (alreadyStart)
                onTimeEnd?.Invoke();
            alreadyStart = false;
        }

        public void StopTimer()
        {
            if(alreadyStart)
            {
                alreadyStart = false;
            }
        }

    }
}
