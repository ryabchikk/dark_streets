using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EventController : MonoBehaviour
{
    [SerializeField] private List<Event> events;
    [SerializeField] private float globalEventChance;
    [SerializeField] private float localEventChance;

    public event Action NewEvent;
    public event Action GlobalEventsUpdated;
    public event Action LocalEventsUpdated;

    private static EventController _instance;
    private List<(Event, int)> _activeGlobalEvents = new();
    private Dictionary<PlayerClass, List<(Event, int)>> _activeLocalEvents = new();

    private void Awake()
    {
        _instance = this;
    }

    public static float GetActiveEffectFor(TypeBusiness businessType, PlayerClass player)
    {
        float globalEffect = 1;
        globalEffect *= _instance
            ._activeGlobalEvents
            .Where(ev => ev.Item1.AffectedBusinessType == businessType)
            .Select(ev => ev.Item1.ActiveIncomeModifier)
            .Aggregate(1.0f, (acc, x) => x * acc);

        if (player is null)
        {
            return globalEffect;
        }

        if (!_instance._activeLocalEvents.TryGetValue(player, out var localEffects))
        {
            return globalEffect;
        }
        
        return globalEffect * localEffects
            .Where(ev => ev.Item1.AffectedBusinessType == businessType)
            .Select(ev => ev.Item1.ActiveIncomeModifier)
            .Aggregate((acc, x) => x * acc);
    }
    
    public static float GetPassiveEffectFor(TypeBusiness businessType, PlayerClass player)
    {
        float globalEffect = 1;
        globalEffect *= _instance
            ._activeGlobalEvents
            .Where(ev => ev.Item1.AffectedBusinessType == businessType)
            .Select(ev => ev.Item1.PassiveIncomeModifier)
            .Aggregate(1.0f, (acc, x) => x * acc);

        if (player is null)
        {
            return globalEffect;
        }

        if (!_instance._activeLocalEvents.TryGetValue(player, out var localEffects))
        {
            return globalEffect;
        }
        
        return globalEffect * localEffects
            .Where(ev => ev.Item1.AffectedBusinessType == businessType)
            .Select(ev => ev.Item1.PassiveIncomeModifier)
            .Aggregate((acc, x) => x * acc);
    }

    public static Event RollGlobalEvent()
    {
        if (Random.value > _instance.globalEventChance)
        {
            return null;
        }

        var globalEvents = _instance.events.Where(ev => ev.Type == EventType.Global && !_instance._activeGlobalEvents.Select(ev => ev.Item1).Contains(ev)).ToArray();
        var ev = globalEvents[Random.Range(0, globalEvents.Length)];
        
        if(ev.Duration > 0)
        {
            _instance._activeGlobalEvents.Add((ev, ev.Duration));
        }
        
        _instance.NewEvent?.Invoke();
        _instance.GlobalEventsUpdated?.Invoke();

        return ev;
    }

    public static Event RollLocalEvent(PlayerClass player)
    {
        if (Random.value > _instance.localEventChance)
        {
            return null;
        }

        var localEvents = _instance.events.Where(ev => ev.Type == EventType.Local).ToArray();
        var ev = localEvents[Random.Range(0, localEvents.Length)];

        if(ev.Duration > 0)
        {
            if (!_instance._activeLocalEvents.TryAdd(player, new List<(Event, int)> { (ev, ev.Duration) }))
            {
                _instance._activeLocalEvents[player].Add((ev, ev.Duration));
            }
        }
        
        _instance.NewEvent?.Invoke();
        _instance.LocalEventsUpdated?.Invoke();

        return ev;
    }

    public static void NotifyTurnPassed(PlayerClass player)
    {
        var idxToRemove = new List<int>();
        if (!_instance._activeLocalEvents.TryGetValue(player, out var events))
        {
            return;
        }
        
        _instance._activeLocalEvents[player] = events.Where(tuple => tuple.Item2 - 1 > 0).ToList();
        
        _instance.LocalEventsUpdated?.Invoke();
    }

    public static void NotifyAllTurnsPassed()
    {
        _instance._activeGlobalEvents = _instance._activeGlobalEvents.Where(tuple => tuple.Item2 - 1 > 0).ToList();
        _instance.GlobalEventsUpdated?.Invoke();
    }

    public List<(Event, int)> GlobalEvents()
    {
        return _instance._activeGlobalEvents;
    }
}
