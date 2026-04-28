using Godot;
using NeonArenaCsharp.stats;

[GlobalClass]
public partial class StatsContainer : Node {
    public readonly Stat Speed = new(0f);
    public readonly Stat MaxHp = new(0f);

    public void Setup(StatsData data) {
        Speed.BaseValue = data.Speed;
        MaxHp.BaseValue = data.MaxHp;
    }
}
