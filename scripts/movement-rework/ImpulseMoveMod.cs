using Godot;

namespace Movement;

[GlobalClass]
public partial class ImpulseMoveMod : MoveMod {
    // Чем выше, тем резче остановка. 5-15 = "crunchy" импульс, 1-3 = плавное скольжение
    [Export] public float Damping = 12f;

    public Vector2 Velocity { get; private set; }

    public override void Update(float delta) {
        if (!Velocity.IsZeroApprox()) {
            Velocity = Velocity.Lerp(Vector2.Zero, 1f - Mathf.Exp(-Damping * (float)delta));
        }
    }

    public override MoveOutput Modify(float speed) {
        return new MoveOutput(Velocity);
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
