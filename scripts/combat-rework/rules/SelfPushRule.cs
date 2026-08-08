using Godot;
using Movement;

[GlobalClass]
public partial class SelfPushRule : WeaponRule {
    [Export] public float Force { get; private set; }
    [Export] public float PowerToForceRatio { get; private set; } = 0f;

    public override void Execute(AttackContext ctx) {
        if (ctx.Source.TryGetComponent<Impulse>(out var component)) {
            var force = Force + ctx.Power * PowerToForceRatio;
            component.ApplyImpulse(-ctx.StartDirection * force);
        };
    }
}
