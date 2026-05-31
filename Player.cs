using Godot;

[GlobalClass]
public partial class Player : CharacterBody2D {
    [Export] SimpleHealth health;
    [Export] KnockbackHandler knockback;
    [Export] GunController gunController;
    [Export] public float Speed = 300f;
    [Export] public float MaxHp = 100f;
    [Export] public AudioStream HitSFX;
    [Export] public AudioStream DeathSFX;

    public override void _Ready() {
        health.Died += OnDied;
    }

    public override void _PhysicsProcess(double delta) {
        var direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        var inputVelocity = direction * Speed;

        Velocity = inputVelocity + knockback.Velocity;
        MoveAndSlide();

        if (Input.IsActionPressed("attack")) {
            gunController.DoShot();
        }
        if (Input.IsKeyPressed(Key.R)) {
            gunController.StartReload();
        }
    }

    public void EquipGun(GunData data) {
        gunController.Equip(data);
    }

    public void TakeDamage(float amount) {
        health.Reduce(amount);
        if (HitSFX != null) {
            Audio.Instance.Play(HitSFX, Audio.BUS_SFX);
        }
    }

    private void OnDied() {
        QueueFree();
        if (DeathSFX != null) {
            Audio.Instance.Play(HitSFX, Audio.BUS_SFX);
        }
    }
}
