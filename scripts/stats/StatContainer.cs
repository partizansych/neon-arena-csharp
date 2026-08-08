using System.Collections.Generic;
using Godot;
using Stats;

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
}
