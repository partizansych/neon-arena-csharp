using System.Collections.Generic;
using Godot;

namespace NeonArenaCsharp;

public struct DamageContext {
    public Node Source;
    public Node Target;
    public float Damage;
    public bool IsCritical;
}

public interface IDamageStep {
    void Execute(DamageContext context);
}

public class CriticalStrikeStep : IDamageStep {
    public void Execute(DamageContext context) {
        if (GD.Randf() < 0.15f) {
            context.Damage *= 1.5f;
            context.IsCritical = true;
        }
    }
}

public class ApplyDamageStep : IDamageStep {
    public void Execute(DamageContext context) {
        if (context.Target is IDamageable damageable) {
            damageable.TakeDamage(context.Damage);
        }
    }
}

public partial class Combat : Node {
    public static Combat Instance { get; private set; }

    readonly List<IDamageStep> pipeline = [];

    public override void _Ready() {
        Instance = this;

        pipeline.Add(new CriticalStrikeStep());
        pipeline.Add(new ApplyDamageStep());
    }

    public void Request(DamageContext context) {
        var target = context.Target;
        if (target != null && IsInstanceValid(target))
            foreach (var step in pipeline)
                step.Execute(context);
    }
}
