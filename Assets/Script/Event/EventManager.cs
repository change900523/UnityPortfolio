using System.Collections.Generic;
using UnityEngine.Events;

public class EventManager : Singleton<EventManager>
{
    private readonly Dictionary<EventEnum, UnityAction> eventDictionary = new Dictionary<EventEnum, UnityAction>();

    public void StartListening(EventEnum eventEnum, UnityAction t)
    {
        if (eventDictionary.TryGetValue(eventEnum, out UnityAction thisEvent))
        {
            thisEvent += t;
            eventDictionary[eventEnum] = thisEvent;
        }
        else
        {
            thisEvent += t;
            eventDictionary.Add(eventEnum, thisEvent);
        }
    }

    public void StopListening(EventEnum eventEnum, UnityAction t)
    {
        if (eventDictionary.TryGetValue(eventEnum, out UnityAction thisEvent))
        {
            thisEvent -= t;
            eventDictionary[eventEnum] = thisEvent;
        }
    }

    public void TriggerEvent(EventEnum eventEnum)
    {
        if (eventDictionary.TryGetValue(eventEnum, out UnityAction thisEvent))
        {
            if (thisEvent != null)
            {
                thisEvent.Invoke();
            }
        }
    }
}

public class EventManager<T> : Singleton<EventManager<T>>
{
    private readonly Dictionary<EventEnum, UnityAction<T>> eventDictionary = new Dictionary<EventEnum, UnityAction<T>>();

    public void StartListening(EventEnum eventEnum, UnityAction<T> t)
    {
        if (eventDictionary.TryGetValue(eventEnum, out UnityAction<T> thisEvent))
        {
            thisEvent += t;
            eventDictionary[eventEnum] = thisEvent;
        }
        else
        {
            thisEvent += t;
            eventDictionary.Add(eventEnum, thisEvent);
        }
    }

    public void StopListening(EventEnum eventEnum, UnityAction<T> t)
    {
        if (eventDictionary.TryGetValue(eventEnum, out UnityAction<T> thisEvent))
        {
            thisEvent -= t;
            eventDictionary[eventEnum] = thisEvent;
        }
    }

    public void TriggerEvent(EventEnum eventEnum, T t)
    {
        if (eventDictionary.TryGetValue(eventEnum, out UnityAction<T> thisEvent))
        {
            if (thisEvent != null)
            {
                thisEvent.Invoke(t);
            }
        }
    }
}

public class EventManager<T1, T2> : Singleton<EventManager<T1, T2>>
{
    private readonly Dictionary<EventEnum, UnityAction<T1, T2>> eventDictionary = new Dictionary<EventEnum, UnityAction<T1, T2>>();

    public void StartListening(EventEnum eventEnum, UnityAction<T1, T2> t)
    {
        if (eventDictionary.TryGetValue(eventEnum, out UnityAction<T1, T2> thisEvent))
        {
            thisEvent += t;
            eventDictionary[eventEnum] = thisEvent;
        }
        else
        {
            thisEvent += t;
            eventDictionary.Add(eventEnum, thisEvent);
        }
    }

    public void StopListening(EventEnum eventEnum, UnityAction<T1, T2> t)
    {
        if (eventDictionary.TryGetValue(eventEnum, out UnityAction<T1, T2> thisEvent))
        {
            thisEvent -= t;
            eventDictionary[eventEnum] = thisEvent;
        }
    }

    public void TriggerEvent(EventEnum eventEnum, T1 t1, T2 t2)
    {
        if (eventDictionary.TryGetValue(eventEnum, out UnityAction<T1, T2> thisEvent))
        {
            if (thisEvent != null)
            {
                thisEvent.Invoke(t1, t2);
            }
        }
    }
}

public class EventManager<T1, T2, T3> : Singleton<EventManager<T1, T2, T3>>
{
    private readonly Dictionary<EventEnum, UnityAction<T1, T2, T3>> eventDictionary = new Dictionary<EventEnum, UnityAction<T1, T2, T3>>();

    public void StartListening(EventEnum eventEnum, UnityAction<T1, T2, T3> t)
    {
        if (eventDictionary.TryGetValue(eventEnum, out UnityAction<T1, T2, T3> thisEvent))
        {
            thisEvent += t;
            eventDictionary[eventEnum] = thisEvent;
        }
        else
        {
            thisEvent += t;
            eventDictionary.Add(eventEnum, thisEvent);
        }
    }

    public void StopListening(EventEnum eventEnum, UnityAction<T1, T2, T3> t)
    {
        if (eventDictionary.TryGetValue(eventEnum, out UnityAction<T1, T2, T3> thisEvent))
        {
            thisEvent -= t;
            eventDictionary[eventEnum] = thisEvent;
        }
    }

    public void TriggerEvent(EventEnum eventEnum, T1 t1, T2 t2, T3 t3)
    {
        if (eventDictionary.TryGetValue(eventEnum, out UnityAction<T1, T2, T3> thisEvent))
        {
            if (thisEvent != null)
            {
                thisEvent.Invoke(t1, t2, t3);
            }
        }
    }
}