using Godot;

namespace Buffs;

[GlobalClass]
public partial class DamageOverTimeEffect : BuffEffect {
    [Export] public float DamagePerTick { get; private set; }

    public override void OnTick(Node target, ActiveBuff buff) {
        var health = target.GetComponent<RpgHealth>();
        health?.Reduce(DamagePerTick * buff.Stacks);
    }
}
