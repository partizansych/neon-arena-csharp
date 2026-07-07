using Godot;

[GlobalClass]
public partial class Player : CharacterBody2D {
    [Export] SimpleHealth health;
    [Export] InputMovement input;
    [Export] KnockbackComponent knockback;
    [Export] DashComponent dash;
    [Export] VelocityDebugger debugger;

    [Export] public float Speed = 300f;
    [Export] public AudioStream HitSFX;
    [Export] public AudioStream DeathSFX;

    public override void _Ready() {
        health.Died += OnDied;
        health.CurrentChanged += OnHpChanged;
    }

    public override void _PhysicsProcess(double delta) {
        HandleMovement();
    }

    // Публичные методы интерфейса

    public void TakeDamage(float amount) {
        health.Reduce(amount);
    }

    // Публичные методы класса

    private void HandleMovement() {
        if (Input.IsActionJustPressed("dash") && input.Direction != Vector2.Zero) {
            dash.Start(input.Direction);
        }

        if (dash.IsDashing) {
            dash.SetSteeringDirection(input.Direction);
        }

        Velocity = dash.IsDashing ? dash.Velocity : (input.Direction * Speed + knockback.Velocity);
        MoveAndSlide();

        if (debugger != null) {
            debugger.CurrentVelocity = Velocity;
        }
    }

    private Vector2 GetDirectionToMouse() {
        var mousePos = GetGlobalMousePosition();
        return GlobalPosition.DirectionTo(mousePos);
    }

    private void OnHpChanged(float oldValue, float newValue) {
        if (newValue >= oldValue || HitSFX == null) return;
        Audio.Instance.PlayJuicySFX(HitSFX);
    }

    private void OnDied() {
        if (DeathSFX != null) {
            Audio.Instance.PlayJuicySFX(DeathSFX);
        }
        QueueFree();
    }
}
