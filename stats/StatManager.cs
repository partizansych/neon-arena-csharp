using Godot;
using NeonArenaCsharp.stats;
using System.Collections.Generic;

[GlobalClass]
public partial class StatManager : Node {
    // [Signal] public delegate void ChangedEventHandler(StatType type, float oldValue, float newValue);

    readonly Dictionary<StatType, Stat> stats = [];

    public void RegisterStat(StatType type, Stat stat) {
        if (!stats.ContainsKey(type))
            return;

        stats.Add(type, stat);

        // stat.Changed += (oldValue, newValue) => {
        //     EmitSignal(SignalName.Changed, type, oldValue, newValue);
        // };
    }

    public float Get(StatType type) {
        if (stats.TryGetValue(type, out var stat))
            return stat.Value;
        return 0f;
    }

    public float GetBase(StatType type) {
        if (stats.TryGetValue(type, out var stat))
            return stat.BaseValue;
        return 0f;
    }

    public void SetBase(StatType type, float newValue) {
        if (stats.TryGetValue(type, out var stat))
            stat.BaseValue = newValue;
    }

    public void AddModifier(StatType type, Modifier mod) {
        if (stats.TryGetValue(type, out var stat)) {
            stat.AddModifier(mod);
        }
    }

    public void RemoveModifiersBySource(object source) {
        foreach (var stat in stats.Values) {
            stat.RemoveModifiersBySource(source);
        }
    }
}
