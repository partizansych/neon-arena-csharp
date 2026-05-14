using System;
using System.Collections.Generic;
using NeonArenaCsharp.stats;

namespace NeonArenaCsharp;

public class StatSheet<T> where T : Enum {
    readonly Dictionary<T, Stat> stats = [];

    public static StatSheet<T> CreateDefault() {
        var instance = new StatSheet<T>();
        foreach (T type in Enum.GetValues(typeof(T))) {
            instance.Register(type, 0f);
        }
        return instance;
    }

    public void Register(T type, float baseValue) {
        if (!stats.TryGetValue(type, out var stat)) {
            stats[type] = new Stat(baseValue);
        }
        else {
            stat.BaseValue = baseValue;
        }
    }

    public float GetValue(T type) {
        if (stats.TryGetValue(type, out var stat))
            return stat.Value;
        throw new KeyNotFoundException("Нельзя получить значение незарегистрированного стата");
    }

    public void AddModifier(T type, Modifier mod) {
        if (!stats.TryGetValue(type, out var stat))
            throw new KeyNotFoundException("Нельзя модицифировать незарегистрированный стат");
        stat.AddModifier(mod);
    }

    public void RemoveModifier(T type, object source) {
        if (!stats.TryGetValue(type, out var stat))
            throw new KeyNotFoundException("Нельзя модицифировать незарегистрированный стат");
        stat.RemoveModifiersBySource(source);
    }
}
