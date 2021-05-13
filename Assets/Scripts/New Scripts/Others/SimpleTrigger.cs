using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleTrigger : MonoBehaviour
{
    [SerializeField] private List<EventAndTimeToStart> eventsAndTime;

    private List<EventAndTimeToStart> alreadyDeactive;

    private void Start()
    {
        alreadyDeactive = new List<EventAndTimeToStart>();
        alreadyDeactive.AddRange(eventsAndTime);
    }


    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (alreadyDeactive.Count > 0)
            {
                foreach (var e in eventsAndTime)
                {
                    if (!e.AlreadyActive)
                    {
                        StartCoroutine(e.Invoke());
                    }
                    alreadyDeactive.Remove(e);
                }
            }
        }
    }


    [System.Serializable]
    public class EventAndTimeToStart
    {
        [SerializeField] private UnityEvent unityEvent;
        [SerializeField] private float time;

        private bool alreadyActive;

        public bool AlreadyActive
        {
            get
            {
                return alreadyActive;
            }
        }

        public IEnumerator Invoke()
        {
            alreadyActive = true;
            yield return new WaitForSeconds(time);
            unityEvent?.Invoke();
        }
    }
}
