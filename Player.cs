using Godot;

[GlobalClass]
public partial class Player : CharacterBody2D {
    [Export] public float Speed = 300f;
    [Export] SimpleHealth health;
    [Export] GunController gun;
    [Export] InputMovement input;
    [Export] KnockbackComponent knockback;
    [Export] DashComponent dash;
    [Export] VelocityDebugger debugger;
    [Export] public AudioStream HitSFX;
    [Export] public AudioStream DeathSFX;

    public override void _Ready() {
        health.Died += OnDied;
        gun.Shot += OnGunShot;
    }

    public override void _PhysicsProcess(double delta) {
        HandleMovement();

        if (Input.IsActionPressed("attack")) {
            var direction = GetDirectionToMouse();
            gun.DoShot(direction);
        }
        if (Input.IsKeyPressed(Key.R)) {
            gun.StartReload();
        }
    }

    public void EquipGun(GunData data) {
        gun.Equip(data);
    }

    public void TakeDamage(float amount) {
        health.Reduce(amount);
        if (HitSFX != null) {
            Audio.Instance.Play(HitSFX, Audio.BUS_SFX);
        }
    }

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

    private void OnGunShot() {
        var direction = -GetDirectionToMouse();
        knockback.Add(direction, 100f);
    }

    private void OnDied() {
        QueueFree();
        if (DeathSFX != null) {
            Audio.Instance.Play(DeathSFX, Audio.BUS_SFX);
        }
    }
}
