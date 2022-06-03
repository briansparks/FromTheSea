using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
public class EventManager : MonoBehaviour
{
    private Dictionary<string, UnityEvent> eventDictionary;
    private Dictionary<string, UnityEvent<int>> intEventDictionary;
    private Dictionary<string, UnityEvent<string>> stringEventDictionary;
    private Dictionary<string, UnityEvent<Vector3>> vector3EventDictionary;

    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There must be one active EventManager script on a GameObject in the scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    private void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityEvent>();
            intEventDictionary = new Dictionary<string, UnityEvent<int>>();
            stringEventDictionary = new Dictionary<string, UnityEvent<string>>();
            vector3EventDictionary = new Dictionary<string, UnityEvent<Vector3>>();
        }
    }

    public static void StartListening(string eventName, UnityAction listener)
    {
        if (instance.eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StartListening(string eventName, UnityAction<int> listener)
    {
        if (instance.intEventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new IntUnityEvent();
            thisEvent.AddListener(listener);
            instance.intEventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StartListening(string eventName, UnityAction<string> listener)
    {
        if (instance.stringEventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new StringUnityEvent();
            thisEvent.AddListener(listener);
            instance.stringEventDictionary.Add(eventName, thisEvent);
        }
    }
    public static void StartListening(string eventName, UnityAction<Vector3> listener)
    {
        if (instance.vector3EventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new Vector3UnityEvent();
            thisEvent.AddListener(listener);
            instance.vector3EventDictionary.Add(eventName, thisEvent);
        }
    }
    public static void StopListening(string eventName, UnityAction<int> listener)
    {
        if (eventManager == null)
        {
            return;
        }

        if (instance.intEventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.RemoveListener(listener);
            instance.eventDictionary.Remove(eventName);
        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        if (eventManager == null)
        {
            return;
        }

        if (instance.eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.RemoveListener(listener);
            instance.eventDictionary.Remove(eventName);
        }
    }
    public static void TriggerEvent(string eventName)
    {
        if (instance.eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.Invoke();
        }
    }

    public static void TriggerEvent(string eventName, Vector3 param)
    {
        if (instance.vector3EventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.Invoke(param);
        }
    }

    public static void TriggerEvent(string eventName, int param)
    {
        if (instance.intEventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.Invoke(param);
        }
    }

    public static void TriggerEvent(string eventName, string param)
    {
        if (instance.stringEventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.Invoke(param);
        }
    }
    public void UITriggerEvent(string eventName)
    {
        TriggerEvent(eventName);
    }
}

[System.Serializable]
public class IntUnityEvent : UnityEvent<int>
{

}

[System.Serializable]
public class StringUnityEvent : UnityEvent<string>
{

}


[System.Serializable]
public class Vector3UnityEvent : UnityEvent<Vector3>
{

}
