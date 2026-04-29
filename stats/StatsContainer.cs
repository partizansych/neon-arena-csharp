using Godot;
using NeonArenaCsharp.stats;

[GlobalClass]
public partial class StatsContainer : Node {
    [Export] StatsData data;

    public readonly Stat Speed = new(0f);
    public readonly Stat MaxHp = new(0f);

    public override void _Ready() {
        Speed.BaseValue = data.Speed;
        MaxHp.BaseValue = data.MaxHp;
    }
}
