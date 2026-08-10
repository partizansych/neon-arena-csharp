using System.Collections.Generic;
using Godot;

namespace Stats;

[GlobalClass]
public partial class StatContainer : Node {
    [Export] StatData[] initialData;

    readonly Dictionary<StatType, Stat> stats = [];

    public override void _Ready() {
        foreach (var init in initialData) {
            stats.Add(init.Type, new Stat(init.BaseValue));
        }
    }

    public float GetValue(StatType type) {
        if (stats.TryGetValue(type, out var stat)) {
            return stat.Value;
        }
        return 0f;
    }

    public float GetBaseValue(StatType type) {
        if (stats.TryGetValue(type, out var stat)) {
            return stat.BaseValue;
        }
        return 0f;
    }

    public void AddModifier(StatType type, Modifier mod) {
        if (stats.TryGetValue(type, out var stat)) {
            stat.Add(mod);
        }
    }

    public void AddModifiers(StatType type, IEnumerable<Modifier> mods) {
        if (stats.TryGetValue(type, out var stat)) {
            stat.Add(mods);
        }
    }

    public bool RemoveModifier(StatType type, Modifier mod) {
        return stats.TryGetValue(type, out var stat) && stat.Remove(mod);
    }

    public int RemoveModifiers(StatType type, IEnumerable<Modifier> mods) {
        return stats.TryGetValue(type, out var stat) ? stat.Remove(mods) : 0;
    }

    public int RemoveModifiers(object source) {
        if (source == null) return 0;

        int totalRemoved = 0;
        foreach (var stat in stats.Values) {
            totalRemoved += stat.Remove(source);
        }
        return totalRemoved;
    }
}
