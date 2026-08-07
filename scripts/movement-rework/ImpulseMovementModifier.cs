using Godot;

[GlobalClass]
public partial class ImpulseMovementModifier : MovementModifier {
    // Чем выше, тем резче остановка. 5-15 = "crunchy" импульс, 1-3 = плавное скольжение
    [Export] public float Damping = 12f;

    public Vector2 Velocity { get; private set; }

    public override void _PhysicsProcess(double delta) {
        if (!Velocity.IsZeroApprox()) {
            Velocity = Velocity.Lerp(Vector2.Zero, 1f - Mathf.Exp(-Damping * (float)delta));
        }
    }

    public override void Modify(ref Vector2 velocity) {
        velocity += Velocity;
    }

    public void ApplyImpulse(Vector2 impulse) {
        if (impulse != Vector2.Zero) {
            Velocity += impulse;
        }
    }

    public void ApplyImpulse(Vector2 direction, float force) {
        if (direction != Vector2.Zero && force > 0f) {
            Velocity += direction * force;
        }
    }
}
