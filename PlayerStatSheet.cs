using System.Collections.Generic;
using Godot;

[GlobalClass]
public partial class PlayerStatSheet : Node {
    readonly Dictionary<PlayerStat, Stat> stats = [];

    public void Setup(PlayerData data) {
        stats[PlayerStat.Speed] = new Stat(data.Speed);
        stats[PlayerStat.MaxHp] = new Stat(data.MaxHp);
    }

    public float Get(PlayerStat stat) {
        if (stats.TryGetValue(stat, out var statInstance))
            return statInstance.Value;
        return 0f;
    }
}