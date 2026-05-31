using System.Collections.Generic;
using Godot;

public struct DamageContext {
    public Node2D Source;
    public Node2D Target;
    public Vector2 HitPos;
    public Gun Weapon;
}

public interface IDamageStep {
    void Execute(DamageContext context);
}

// public class CriticalStrikeStep : IDamageStep {
//     public void Execute(DamageContext context) {
//         if (GD.Randf() < 0.15f) {
//             context.Damage *= 1.5f;
//             context.IsCritical = true;
//         }
//     }
// }

public class ApplyDamageStep : IDamageStep {
    public void Execute(DamageContext context) {
        if (context.Target is IDamageable damageable) {
            var weapon = context.Weapon;
            damageable.TakeDamage(weapon.Data.Damage);
        }
    }
}

public class ApplyKnockbackStep : IDamageStep {
    public void Execute(DamageContext context) {
        if (context.Target is Character character) {
            var targetPos = context.Target.GlobalPosition;
            var direction = context.HitPos.DirectionTo(targetPos);
            character.ApplyKnockback(direction, 100f);
        }
    }

}

public partial class Combat : Node {
    public static Combat Instance { get; private set; }

    readonly List<IDamageStep> pipeline = [];

    public override void _Ready() {
        Instance = this;

        pipeline.Add(new ApplyDamageStep());
        // pipeline.Add(new CriticalStrikeStep());
        pipeline.Add(new ApplyKnockbackStep());
    }

    public void Request(DamageContext context) {
        var target = context.Target;
        if (target != null && IsInstanceValid(target))
            foreach (var step in pipeline)
                step.Execute(context);
    }
}
