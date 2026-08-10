using Godot;

namespace Buffs;

[GlobalClass]
public partial class RegenerationEffect : BuffEffect {
    [Export] public float HpPerTick { get; private set; }

    public override void OnTick(Node target, ActiveBuff buff) {
        // TODO: Заменить на получение через название
        var health = target.GetComponent<RpgHealth>();
        health?.Restore(HpPerTick * buff.Stacks);
    }
}
