using Godot;
using System;

namespace Movement;

[GlobalClass]
public partial class DashMoveMod : MoveMod {
    public event Action Started;
    public event Action Finished;

    [Export] public float Speed = 700f;
    [Export] public float Duration = 0.27f;
    [Export] public float Steering = 8f;
    [Export] public float Cooldown = 1.0f;

    public Vector2 Velocity { get; private set; }
    public bool IsDashing => dashTimer > 0f;

    float dashTimer;
    float cooldownTimer;
    Vector2 dashDirection;
    Vector2 targetSteeringDirection;

    public override void Update(float delta) {
        if (cooldownTimer > 0f) {
            cooldownTimer -= delta;
        }

        if (IsDashing) {
            dashTimer -= delta;

            if (dashTimer <= 0f) {
                cooldownTimer = Cooldown;
                Finished?.Invoke();
            }

            if (targetSteeringDirection != Vector2.Zero) {
                var t = 1.0f - (float)Mathf.Exp(-Steering * delta);
                dashDirection = dashDirection.Lerp(targetSteeringDirection, t);
                dashDirection = dashDirection.Normalized(); // Вместо SLerp
            }

            Velocity = dashDirection * Speed;
        }
        else Velocity = Vector2.Zero;
    }

    public override MoveOutput Modify(float speed) {
        return IsDashing ? new MoveOutput(Velocity) : MoveOutput.Silenced;
    }

    // Если компонент нуждается во внешнем контексте (инпут игрока, движение врага к игроку),
    // то избегать параметры не получится, ведь компонент уникален.

    public void Start(Vector2 direction) {
        if (cooldownTimer > 0f || IsDashing) return;
        dashDirection = direction.Normalized();
        targetSteeringDirection = dashDirection;
        dashTimer = Duration;
        Started?.Invoke();
    }

    public void SetSteeringDirection(Vector2 direction) {
        if (!IsDashing) return;
        targetSteeringDirection = direction.Normalized();
    }
}
