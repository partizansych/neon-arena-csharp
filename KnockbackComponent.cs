using Godot;

[GlobalClass]
public partial class KnockbackComponent : Node2D {
    [Export] public float Friction = 100f;

    public Vector2 Velocity { get; private set; } = Vector2.Zero;

    public override void _PhysicsProcess(double delta) {
        if (Velocity != Vector2.Zero) {
            Velocity = Velocity.MoveToward(
                Vector2.Zero,
                (float)delta * Friction
            );
        }
    }

    public void Add(Vector2 direction, float force) {
        if (direction != Vector2.Zero && force > 0f) {
            Velocity += direction.Normalized() * force;
        }
    }
}
