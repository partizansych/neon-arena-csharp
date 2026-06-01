using Godot;

[GlobalClass]
public partial class KnockbackComponent : Node2D {
    // Чем выше, тем резче остановка. 5-15 = "crunchy" импульс, 1-3 = плавное скольжение
    [Export] public float Damping = 10f;
    [Export] public float Max = 600f;
    [Export] VelocityDebugger debugger;

    public Vector2 Velocity { get; private set; }

    public override void _PhysicsProcess(double delta) {
        if (!Velocity.IsZeroApprox()) {
            Velocity = Velocity.Lerp(Vector2.Zero, 1f - Mathf.Exp(-Damping * (float)delta));
            // Velocity *= Mathf.Max(0f, 1f - Damping * (float)delta);
        }

        if (debugger != null) {
            debugger.CurrentVelocity = Velocity;
        }
    }

    public void Add(Vector2 direction, float force) {
        if (direction != Vector2.Zero && force > 0f) {
            Velocity += direction.Normalized() * force;
            if (Velocity.Length() > Max) {
                Velocity = Velocity.Normalized() * Max;
            }
        }
    }
}
