using Godot;

[GlobalClass]
public partial class SelfPushRule : WeaponRule {
    [Export] public float Force { get; private set; }
    [Export] public float PowerToForceRatio { get; private set; } = 0f;

    public override void Execute(AttackContext ctx) {
        if (ctx.Source.TryGetComponent<KnockbackComponent>(out var component)) {
            var force = Force + ctx.Power * PowerToForceRatio;
            component.Add(-ctx.StartDirection, force);
        };
    }
}
