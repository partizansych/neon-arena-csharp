using Godot;
using Stats;

namespace Buffs;

[GlobalClass]
public partial class StunEffect : BuffEffect {
    [Export] public ModifierData SpeedMod { get; private set; }

    public override void OnApply(Node target, ActiveBuff buff) {
        var stats = target.GetComponent<StatContainer>();
        stats?.AddModifier(StatType.Speed, SpeedMod.AsMod(this));
    }

    public override void OnRemove(Node target, ActiveBuff buff) {
        var stats = target.GetComponent<StatContainer>();
        stats?.RemoveModifiers(this);
    }
}
