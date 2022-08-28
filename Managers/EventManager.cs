using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour, IManager
{
    private Dictionary<string, UnityEvent> eventDictionary;
    private Dictionary<string, UnityEvent<int>> intEventDictionary;
    private Dictionary<string, UnityEvent<string>> stringEventDictionary;
    private Dictionary<string, UnityEvent<Vector3>> vector3EventDictionary;
    private Dictionary<string, UnityEvent<CharacterSpawnRequest>> characterSpawnRequestDictionary;
    private Dictionary<string, UnityEvent<ActiveRaidCharacterSpawnRequest>> activeRaidCharacterSpawnRequestDictionary;
    private Dictionary<string, UnityEvent<Guid>> guidDictionary;
    private Dictionary<string, UnityEvent<RaidLocationDto>> raidLocationDtoDictionary;
    private Dictionary<string, UnityEvent<ResourceData>> resourceDataDictionary;

    private static EventManager eventManager;

    public void Initialize()
    {
        eventDictionary = new Dictionary<string, UnityEvent>();
        intEventDictionary = new Dictionary<string, UnityEvent<int>>();
        stringEventDictionary = new Dictionary<string, UnityEvent<string>>();
        vector3EventDictionary = new Dictionary<string, UnityEvent<Vector3>>();
        characterSpawnRequestDictionary = new Dictionary<string, UnityEvent<CharacterSpawnRequest>>();
        guidDictionary = new Dictionary<string, UnityEvent<Guid>>();
        activeRaidCharacterSpawnRequestDictionary = new Dictionary<string, UnityEvent<ActiveRaidCharacterSpawnRequest>>();
        raidLocationDtoDictionary = new Dictionary<string, UnityEvent<RaidLocationDto>>();
        resourceDataDictionary = new Dictionary<string, UnityEvent<ResourceData>>();
    }

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType<EventManager>();

                if (!eventManager)
                {
                    Debug.LogError("There must be one active EventManager script on a GameObject in the scene.");
                }
                else
                {
                    eventManager.Initialize();
                }
            }

            return eventManager;
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

    public static void StartListening(string eventName, UnityAction<RaidLocationDto> listener)
    {
        if (instance.raidLocationDtoDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new RaidLocationDtoUnityEvent();
            thisEvent.AddListener(listener);
            instance.raidLocationDtoDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StartListening(string eventName, UnityAction<ResourceData> listener)
    {
        if (instance.resourceDataDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new ResourceDataUnityEvent();
            thisEvent.AddListener(listener);
            instance.resourceDataDictionary.Add(eventName, thisEvent);
        }
    }
    public static void StartListening(string eventName, UnityAction<Guid> listener)
    {
        if (instance.guidDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new GuidUnityEvent();
            thisEvent.AddListener(listener);
            instance.guidDictionary.Add(eventName, thisEvent);
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

    public static void StartListening(string eventName, UnityAction<CharacterSpawnRequest> listener)
    {
        if (instance.characterSpawnRequestDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new CharacterSpawnRequestUnityEvent();
            thisEvent.AddListener(listener);
            instance.characterSpawnRequestDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StartListening(string eventName, UnityAction<ActiveRaidCharacterSpawnRequest> listener)
    {
        if (instance.activeRaidCharacterSpawnRequestDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new ActiveRaidCharacterSpawnRequestUnityEvent();
            thisEvent.AddListener(listener);
            instance.activeRaidCharacterSpawnRequestDictionary.Add(eventName, thisEvent);
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

    public static void TriggerEvent(string eventName, Guid param)
    {
        if (instance.guidDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.Invoke(param);
        }
    }

    public static void TriggerEvent(string eventName, Vector3 param)
    {
        if (instance.vector3EventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.Invoke(param);
        }
    }

    public static void TriggerEvent(string eventName, RaidLocationDto param)
    {
        if (instance.raidLocationDtoDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.Invoke(param);
        }
    }

    public static void TriggerEvent(string eventName, ResourceData param)
    {
        if (instance.resourceDataDictionary.TryGetValue(eventName, out var thisEvent))
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

    public static void TriggerEvent(string eventName, CharacterSpawnRequest param)
    {
        if (instance.characterSpawnRequestDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.Invoke(param);
        }
    }

    public static void TriggerEvent(string eventName, ActiveRaidCharacterSpawnRequest param)
    {
        if (instance.activeRaidCharacterSpawnRequestDictionary.TryGetValue(eventName, out var thisEvent))
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
public class GuidUnityEvent : UnityEvent<Guid>
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

[System.Serializable]
public class CharacterSpawnRequestUnityEvent : UnityEvent<CharacterSpawnRequest>
{

}

[System.Serializable]
public class ActiveRaidCharacterSpawnRequestUnityEvent : UnityEvent<ActiveRaidCharacterSpawnRequest>
{

}

[System.Serializable]
public class RaidLocationDtoUnityEvent : UnityEvent<RaidLocationDto>
{

}

[System.Serializable]
public class ResourceDataUnityEvent : UnityEvent<ResourceData>
{

}
